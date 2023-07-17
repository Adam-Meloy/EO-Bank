using System.IO;
using System.Text;

namespace EO_Bank
{
    /// <summary>
    /// Base class for character saves. Includes common code, as well as values that are consistent
    /// across every game.
    /// </summary>
    public abstract class CharacterSaveFile
    {
        /// <summary>
        /// The decrypted bytes of the provided input file.
        /// </summary>
        public byte[] Data;

        public CharacterSaveFile() { }

        /// <summary>
        /// For loading character saves, which are decrypted copies of normal character save data
        /// </summary>
        /// <param name="path">The path to the character save file.</param>
        public CharacterSaveFile(string path) { Data = File.ReadAllBytes(path); }

        /// <summary>
        /// Copies the character data, and writes it to the given path.
        /// </summary>
        /// <param name="path">Where to write the character save to.</param>
        public virtual void WriteCharacterSave(string path, Character Character, SaveFile SaveFile) { File.WriteAllBytes(path, Data); }
    }

    public class EO1CharacterSave : CharacterSaveFile
    {
        /// <summary>
        /// Character data.
        /// </summary>
        public EO1Character Character { get; set; } = new EO1Character();

        /// <summary>
        /// The character's guild name.
        /// </summary>
        public string GuildName { get; set; } = "";

        public EO1CharacterSave() { }

        public EO1CharacterSave(string path) : base(path)
        {
            using var input = new BinaryReader(new MemoryStream(Data));
            ReadCharacterData(input);
            ReadGuildName(input);
        }

        private void ReadCharacterData(BinaryReader input)
        {
            input.BaseStream.Position = 0x0;
            Character = new EO1Character(input);
        }

        private void ReadGuildName(BinaryReader input)
        {
            char[] name = input.ReadChars(16);
            GuildName = new string(name).Replace("\0", "");
        }

        public override void WriteCharacterSave(string path, Character Character, SaveFile SaveFile)
        {
            // byte[] byteName = Encoding.UTF8.GetBytes(((EO1SaveFile)SaveFile).RawGuildName);
            // System.Windows.Forms.MessageBox.Show(((char)byteName[0]).ToString());
            // Data = Character.RebuildBytes() + byteName;
            File.WriteAllBytes(path, Data);
        }

        public virtual void CreateCharacterSave()
        {

        }
    }

    public class EO2CharacterSave : CharacterSaveFile
    {
        // W.I.P
    }

    public class EO3CharacterSave : CharacterSaveFile
    {
        // W.I.P
    }
}
