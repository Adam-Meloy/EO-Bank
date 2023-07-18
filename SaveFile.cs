/// Title: SaveFile, EO3SaveFile
/// Author: Rea
/// Date: 2023
/// Code version: 1
/// Availability: ?

using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace EO_Bank
{

    /// <summary>Base class for saves. Includes common encryption/decryption/IO code, as well as values that are consistent across every game.</summary>
    public abstract class SaveFile
    {
        // AES configuration values.
        private static readonly string Key = "Atlus-inc-SQS3SE";
        private static readonly string IV = "Atlus-inc-SQS3Se";
        private static readonly int BlockSize = 128;
        private static readonly int KeySize = 128;

        /// <summary>
        /// The AES object for handling encryption and decryption of saves.
        /// </summary>
        private readonly Aes Aes;

        /// <summary>
        /// The decrypted, unedited bytes of the provided input file.
        /// </summary>
        public byte[] Data;

        /// <summary>
        /// The modified save, used to write the encrypted save.
        /// </summary>
        public byte[] WorkData;

        /// <summary>Character data.</summary>
        public abstract Character[] Characters { get; set; }

        /// <summary>The guild's name.</summary>
        public string GuildName { get; set; } = "";

        public SaveFile() { }

        /// <summary>
        /// For loading encrypted saves, i.e. as the games write them.
        /// </summary>
        /// <param name="path">The path to the save file.</param>
        public SaveFile(string path)
        {
            Aes = InitAes();
            var encrypted = File.ReadAllBytes(path);
            Data = DecryptSave(encrypted);
            WorkData = new byte[Data.Length];
        }

        /// <summary>
        /// Encrypts WorkData, and writes it to the given path.
        /// </summary>
        /// <param name="path">Where to write the encrypted save to.</param>
        public virtual void WriteEncryptedSave(string path)
        {
            var encrypted = EncryptSave(WorkData);
            File.WriteAllBytes(path, encrypted);
        }

        /// <summary>
        /// Writes Data to the given path.
        /// </summary>
        /// <param name="path">Where to write the decrypted save to.</param>
        public virtual void WriteDecryptedSave(string path)
        {
            File.WriteAllBytes(path, Data);
        }

        private byte[] DecryptSave(byte[] encrypted)
        {
            return Aes.DecryptCbc(encrypted, Aes.IV);
        }

        private byte[] EncryptSave(byte[] encrypted)
        {
            return Aes.EncryptCbc(encrypted, Aes.IV);
        }

        private static Aes InitAes()
        {
            var aes = Aes.Create();
            aes.BlockSize = BlockSize;
            aes.KeySize = KeySize;
            aes.Key = Encoding.UTF8.GetBytes(Key);
            aes.IV = Encoding.UTF8.GetBytes(IV);
            // Mode and Padding are CBC and PKCS7, the defaults for AES, so no need to set them.
            return aes;
        }
    }

    public class EO1SaveFile : SaveFile
    {
        /// <summary>The characters from the save file.</summary>
        public override Character[] Characters { get; set; } = new EO1Character[30];

        /// <summary>The guild's name as read from the decrypted save file.</summary>
        public char[] RawGuildName { get; set; } = new char[16];

        public EO1SaveFile() { }

        public EO1SaveFile(string path) : base(path)
        {
            using var input = new BinaryReader(new MemoryStream(Data));
            ReadCharacterData(input);
            ReadGuildName(input);
        }

        private void ReadCharacterData(BinaryReader input)
        {
            for (var i = 0; i < Characters.Length; i += 1)
            {
                input.BaseStream.Position = 0x1C + (i * 0x130);
                Characters[i] = new EO1Character(input);
            }
        }

        private void ReadGuildName(BinaryReader input)
        {
            // There is some distance between last character and guild name
            input.BaseStream.Position = 0x23F8;
            RawGuildName = input.ReadChars(16);
            GuildName = new string(RawGuildName).Replace("\0", "");
        }

        public override void WriteEncryptedSave(string path)
        {
            //UpdateWorkData();
            //WorkData = File.ReadAllBytes("C:/Users/Asteras/Downloads/EO1UhOh.bin");
            base.WriteEncryptedSave(path);
        }

        /// <summary>
        /// Syncs the bytes representing each character in the work buffer with each current character.
        /// </summary>
        private void UpdateWorkData()
        {
            using var reader = new BinaryReader(new MemoryStream(Data));
            using var writer = new BinaryWriter(new MemoryStream(WorkData));
            // Write the header.
            writer.Write(reader.ReadBytes(0x1C));
            // Write characters.
            for (int i = 0; i < Characters.Length; i += 1)
            {
                var character = Characters[i];
                // Write the pre-EXP bytes.
                writer.Write(reader.ReadBytes(0x7C));
                // EXP.
                reader.ReadUInt32();
                writer.Write(character.Exp);
                // Write the post-EXP bytes.
                writer.Write(reader.ReadBytes(0xB0));
            }
            //Write the data between characters and guild name.
            writer.Write(reader.ReadBytes(0x1F));
            //Write the guild name.
            writer.Write(reader.ReadBytes(0x10));
            // "Draw the rest of the owl" I mean write the rest of the file.
            writer.Write(reader.ReadBytes(0x486EE));
            File.WriteAllBytes("C:/Users/Asteras/Downloads/EO1UhOh.bin", WorkData);
        }
    }

    public class EO2SaveFile : SaveFile
    {
        /// <summary>The characters from the save file.</summary>
        public override Character[] Characters { get; set; } = new EO2Character[30];

        /// <summary>The guild's name as read from the decrypted save file.</summary>
        public char[] RawGuildName { get; set; } = new char[16];

        public EO2SaveFile() { }

        public EO2SaveFile(string path) : base(path)
        {
            //using var input = new BinaryReader(new MemoryStream(Data));
            //ReadCharacterData(input);
            //ReadGuildName(input);
        }

        private void ReadCharacterData(BinaryReader input)
        {
            for (var i = 0; i < Characters.Length; i += 1)
            {
                input.BaseStream.Position = 0x1C + (i * 0x130);
                Characters[i] = new EO2Character(input);
            }
        }

        private void ReadGuildName(BinaryReader input)
        {
            // There is some distance between last character and guild name
            input.BaseStream.Position = 0x23F8; // this position is almost certainly wrong, will be changed
            RawGuildName = input.ReadChars(16);
            GuildName = new string(RawGuildName).Replace("\0", "");
        }

        public override void WriteEncryptedSave(string path)
        {
            //UpdateWorkData();
            //WorkData = File.ReadAllBytes("C:/Users/Asteras/Downloads/EO2UhOh.bin");
            base.WriteEncryptedSave(path);
        }

        /// <summary>
        /// Syncs the bytes representing each character in the work buffer with each current character.
        /// </summary>
        private void UpdateWorkData()
        {
            using var reader = new BinaryReader(new MemoryStream(Data));
            using var writer = new BinaryWriter(new MemoryStream(WorkData));
            // Write the header.
            writer.Write(reader.ReadBytes(0x1C)); // this position is almost certainly wrong, will be changed
            // Write characters.
            for (int i = 0; i < Characters.Length; i += 1)
            {
                var character = Characters[i];
                // Write the pre-EXP bytes.
                writer.Write(reader.ReadBytes(0x7C)); // this position is almost certainly wrong, will be changed
                // EXP.
                reader.ReadUInt32();
                writer.Write(character.Exp);
                // Write the post-EXP bytes.
                writer.Write(reader.ReadBytes(0xB0)); // this position is almost certainly wrong, will be changed
            }
            //Write the data between characters and guild name.
            writer.Write(reader.ReadBytes(0x1F)); // this position is almost certainly wrong, will be changed
            //Write the guild name.
            writer.Write(reader.ReadBytes(0x10)); // this position is almost certainly wrong, will be changed
            // "Draw the rest of the owl" I mean write the rest of the file.
            writer.Write(reader.ReadBytes(0x486EE)); // this position is almost certainly wrong, will be changed
            File.WriteAllBytes("C:/Users/Asteras/Downloads/EO2UhOh.bin", WorkData);
        }
    }

    public class EO3SaveFile : SaveFile
    {
        /// <summary>
        /// Where the game can be saved.
        /// </summary>
        public enum SavePointTypes
        {
            /// <summary>
            /// Saved at Aman's Inn.
            /// </summary>
            Inn = 1,
            /// <summary>
            /// Saved at a submagnetic pole.
            /// </summary>
            Dungeon = 2,
            /// <summary>
            /// This save is a quicksave.
            /// </summary>
            Quicksave = 3,
            /// <summary>
            /// Saved at Inver Port, before beginning a sea quest.
            /// </summary>
            Port = 4,
            /// <summary>
            /// Saved at the "save clear data?" prompt after the credits.
            /// </summary>
            Ending = 5
        }

        /// <summary>
        /// When this save file was written. Stored as milliseconds since the Unix epoch.
        /// </summary>
        private long SaveTime { get; set; }

        /// <summary>
        /// How long this save file has been played, store as milliseconds.
        /// </summary>
        public int PlayTime { get; set; } = 0;

        /// <summary>
        /// Whether or not this save is NG+. I don't know what setting an NG+ save to a normal save would do.
        /// </summary>
        public bool NewGamePlus { get; set; }

        /// <summary>
        /// Where this save was created, i.e. at the inn, the port, etc.
        /// </summary>
        private SavePointTypes SavedWhere { get; set; }

        /// <summary>
        /// The deepest floor the player has reached, 1-indexed. Probably shouldn't be set to any value that isn't between
        /// 1 and 25!
        /// </summary>
        public int DeepestFloor { get; set; }

        /// <summary>
        /// Character data.
        /// </summary>
        public override Character[] Characters { get; set; } = new EO3Character[30];

        /// <summary>
        /// The preset parties that you can call up all at once from the Explorers Guild. Arrays of character IDs, sorted
        /// by party position. Letting one of these actually have six characters is a bad idea, and will lead to a softlock
        /// on the next Result screen.
        /// </summary>
        public int[][] PresetParties = new int[6][];

        /// <summary>
        /// How much money the player currently has.
        /// </summary>
        public int Ental { get; set; }

        /// <summary>
        /// How much money the player has accumulated over the entire game.
        /// </summary>
        public int LifetimeEntal { get; set; }

        /// <summary>
        /// The ship's name.
        /// </summary>
        public string ShipName { get; set; } = "";

        /// <summary>
        /// The standard inventory.
        /// </summary>
        //public ItemData[] Inventory { get; } = new ItemData[60];

        /// <summary>
        /// The player's key items.
        /// </summary>
        //public ItemData[] KeyItems { get; } = new ItemData[60];

        /// <summary>
        /// Items stored at the inn.
        /// </summary>
        //public ItemData[] InnStorage { get; } = new ItemData[99];

        /// <summary>
        /// Limit items owned by the party. Determines what limit skills the player can use.
        /// </summary>
        //public short[] LimitItems { get; } = new short[64];

        /// <summary>
        /// Hammers owned by the party. Determines what forges they can apply at Napier's Firm.
        /// </summary>
        //public short[] Hammers { get; } = new short[56];

        //public byte[] ShopMaterialStorage { get; } = new byte[400];

        //public bool[] SeaItemFlags { get; } = new bool[768];

        ///
        public EO3SaveFile() { }

        public EO3SaveFile(string path) : base(path)
        {
            using var input = new BinaryReader(new MemoryStream(Data));
            ReadSaveHeader(input);
            //ReadPartyDataHeader(input);
            ReadCharacterData(input);
            //ReadPresetParties(input);
            //ReadMoneyAndNamePartyData(input);
            //ReadInventories(input);
            //ReadShopData(input);
            //ReadSeaItemBitfields(input);
        }

        private void ReadSaveHeader(BinaryReader input)
        {
            SaveTime = input.ReadInt64();
            input.ReadBytes(8); // Unknown. Metadata says "save version" but I'm not sure how to parse it.
            PlayTime = input.ReadInt32();
            input.ReadInt16(); // "SaveVersion". Like above, I'm not sure how to interpret this. It seems to be 0x00 always.
            SavedWhere = (SavePointTypes)input.ReadInt16();
            input.ReadInt16(); // "SaveBgmNo". Unless I'm mistaken, this should always be 0x1F, SAVELOAD.
            NewGamePlus = input.ReadInt32() == 1;
        }

        /*private void ReadPartyDataHeader(BinaryReader input)
        {
            input.ReadInt32(); // E_DIR_TYPE.
            input.ReadSByte(); // NowFloor. Seems to be set to 30d when I've saved at the inn?
            DeepestFloor = input.ReadSByte();
            input.ReadBytes(4); // "Now" and "Old" values for what seem like coordinates. No need to mess with these.
            input.ReadSByte(); // NowHour.
            input.ReadSByte(); // NowMin.
            input.ReadInt32(); // NowDay.
        }*/

        private void ReadCharacterData(BinaryReader input)
        {
            for (var i = 0; i < Characters.Length; i += 1)
            {
                input.BaseStream.Position = 0x30 + (i * 0x110);
                Characters[i] = new EO3Character(input);
            }
            // TODO: Finish parsing character data, so this isn't necessary.
            input.BaseStream.Position = 0x2010;
        }

        /*private void ReadPresetParties(BinaryReader input)
        {
            for (var presetParty = 0; presetParty < PresetParties.Length; presetParty += 1)
            {
                PresetParties[presetParty] = new int[6];
                var party = PresetParties[presetParty];
                for (var character = 0; character < party.Length; character += 1)
                {
                    party[character] = input.ReadInt32();
                }
            }
        }*/

        /*private void ReadMoneyAndNamePartyData(BinaryReader input)
        {
            Ental = input.ReadInt32();
            LifetimeEntal = input.ReadInt32();
            GuildName = Util.GetUtf8StringFromSaveData(input, 0x18);
            ShipName = Util.GetUtf8StringFromSaveData(input, 0x18);
            input.ReadBytes(4); // ColTypes for Floor and SeaFloor, and coords. Shouldn't mess with these.
            input.ReadInt32(); // Another E_DIR_TYPE.
        }*/

        /*private void ReadInventories(BinaryReader input)
        {
            for (var i = 0; i < Inventory.Length; i += 1)
            {
                Inventory[i] = new ItemData(input);
            }
            for (var i = 0; i < KeyItems.Length; i += 1)
            {
                KeyItems[i] = new ItemData(input);
            }
            for (var i = 0; i < InnStorage.Length; i += 1)
            {
                InnStorage[i] = new ItemData(input);
            }
            for (var i = 0; i < LimitItems.Length; i += 1)
            {
                LimitItems[i] = input.ReadInt16();
            }
            for (var i = 0; i < Hammers.Length; i += 1)
            {
                Hammers[i] = input.ReadInt16();
            }
        }*/

        /*private void ReadShopData(BinaryReader input)
        {
            input.BaseStream.Position = 0x4758;
            for (int i = 0; i < ShopMaterialStorage.Length; i += 1)
            {
                ShopMaterialStorage[i] = input.ReadByte();
            }
        }*/

        /*private void ReadSeaItemBitfields(BinaryReader input)
        {
            input.BaseStream.Position = 0x5678;
            for (int flagByteOffset = 0; flagByteOffset < SeaItemFlags.Length; flagByteOffset += 8)
            {
                var flagByte = input.ReadByte();
                for (int bit = 0; bit < 8; bit += 1)
                {
                    SeaItemFlags[flagByteOffset + bit] = (flagByte & (1 << bit)) != 0;
                }
            }
        }*/

        public override void WriteEncryptedSave(string path)
        {
            //UpdateWorkData();
            WorkData = File.ReadAllBytes("C:/Users/Asteras/Download/EO3SaveFile.bin");
            base.WriteEncryptedSave(path);
        }

        /// <summary>
        /// Syncs the bytes representing each character in the work buffer with each current character.
        /// </summary>
        private void UpdateWorkData()
        {
            using var reader = new BinaryReader(new MemoryStream(Data));
            using var writer = new BinaryWriter(new MemoryStream(WorkData));
            // Write the header, part 1.
            writer.Write(reader.ReadBytes(0x10));
            // Write the play time.
            writer.Write(PlayTime);
            writer.Write((short)0x00); // TODO: is this ever not 0?
            writer.Write((short)SavedWhere);
            // Write the header, part 2.
            reader.BaseStream.Position = 0x18;
            writer.Write(reader.ReadBytes(0x18));
            // Write characters.
            for (int i = 0; i < Characters.Length; i += 1)
            {
                var character = Characters[i];
                // Write the pre-EXP bytes.
                writer.Write(reader.ReadBytes(0xAC));
                // EXP.
                reader.ReadUInt32();
                writer.Write(character.Exp);
                // Write the post-EXP bytes.
                writer.Write(reader.ReadBytes(0x60));
            }
            // "Draw the rest of the owl" I mean write the rest of the file, part 1.
            writer.Write(reader.ReadBytes(0x3668));
            reader.ReadBytes(0x60);
            // "Draw the rest of the owl" I mean write the rest of the file, part 2.
            writer.Write(reader.ReadBytes(0x42618));
            File.WriteAllBytes("C:/Users/Asteras/Downloads/UhOh.bin", WorkData);
        }
    }
}
