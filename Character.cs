using System.IO;
using System.Security.Cryptography;

namespace EO_Bank
{
    public abstract class Character
    {
        /// <summary>Stats</summary>
        /// <remarks>Max HP, Max TP, STR, VIT, AGI, LUC, TEC</remarks>
        public struct ABIL_DATA
        {
            /// <summary>Max HP</summary>
            public int HpMax;
            /// <summary>Max TP</summary>
            public int TpMax;
            /// <summary>Strength (STR)</summary>
            public int Str;
            /// <summary>Vitality (VIT)</summary>
            public int Vit;
            /// <summary>Agility (AGI)</summary>
            public int Agi;
            /// <summary>Luck (LUC)</summary>
            public int Luc;
            /// <summary>Technique (TEC)</summary>
            public int Tec;

            public ABIL_DATA() { }

            public ABIL_DATA(BinaryReader input)
            {
                HpMax = EndianBitConverter.Little.ToInt32(input.ReadBytes(4), 0);
                TpMax = EndianBitConverter.Little.ToInt32(input.ReadBytes(4), 0);
                Str = EndianBitConverter.Little.ToInt32(input.ReadBytes(4), 0);
                Vit = EndianBitConverter.Little.ToInt32(input.ReadBytes(4), 0);
                Agi = EndianBitConverter.Little.ToInt32(input.ReadBytes(4), 0);
                Luc = EndianBitConverter.Little.ToInt32(input.ReadBytes(4), 0);
                Tec = EndianBitConverter.Little.ToInt32(input.ReadBytes(4), 0);
            }
        }

        /// <summary>Equipment</summary>
        public struct EQUIP_DATA
        {
            /// <summary>Equipped Weapon</summary>
            public int Weapon;
            /// <summary>Equipped Armor in the first slot</summary>
            public int Armor_1;
            /// <summary>Equipped Armor in the second slot</summary>
            public int Armor_2;
            /// <summary>Equipped Armor in the third slot</summary>
            public int Armor_3;

            public EQUIP_DATA() { }

            public EQUIP_DATA(BinaryReader input)
            {
                Weapon = EndianBitConverter.Little.ToInt32(input.ReadBytes(4), 0);
                Armor_1 = EndianBitConverter.Little.ToInt32(input.ReadBytes(4), 0);
                Armor_2 = EndianBitConverter.Little.ToInt32(input.ReadBytes(4), 0);
                Armor_3 = EndianBitConverter.Little.ToInt32(input.ReadBytes(4), 0);
            }
        }



        /// <summary>Name</summary>
        public abstract char[] Name { get; set; }
        /// <summary>Class</summary>
        public abstract int Class { get; set; }

        /// <summary>Level</summary>
        public abstract int Level { get; set; }

        /// <summary>Experience</summary>
        public abstract uint EXP { get; set; }

        public Character() { }

        /// <summary>Get a string version of the Character's Name</summary>
        public abstract string GetName();
        /// <summary>Get a string version of the Character's Class</summary>
        public abstract string GetClass();
    }

    public class EO1Character : Character
    {
        /// <summary>Element Resist Values</summary>
        public struct RESIST_DATA
        {
            public int Slash;
            public int Shock;
            public int Thrust;
            public int Fire;
            public int Ice;
            public int Thunder;
            public int Death;
            public int Mind;
            public int Head;
            public int Arm;
            public int Leg;

            public RESIST_DATA() { }

            public RESIST_DATA(BinaryReader input)
            {
                Slash = EndianBitConverter.Little.ToInt32(input.ReadBytes(4), 0);
                Shock = EndianBitConverter.Little.ToInt32(input.ReadBytes(4), 0);
                Thrust = EndianBitConverter.Little.ToInt32(input.ReadBytes(4), 0);
                Fire = EndianBitConverter.Little.ToInt32(input.ReadBytes(4), 0);
                Ice = EndianBitConverter.Little.ToInt32(input.ReadBytes(4), 0);
                Thunder = EndianBitConverter.Little.ToInt32(input.ReadBytes(4), 0);
                Death = EndianBitConverter.Little.ToInt32(input.ReadBytes(4), 0);
                Mind = EndianBitConverter.Little.ToInt32(input.ReadBytes(4), 0);
                Head = EndianBitConverter.Little.ToInt32(input.ReadBytes(4), 0);
                Arm = EndianBitConverter.Little.ToInt32(input.ReadBytes(4), 0);
                Leg = EndianBitConverter.Little.ToInt32(input.ReadBytes(4), 0);
            }
        }

        /// <summary>List of Bad Statuses</summary>
        public enum BadStatusFlag
        {
            BAD_STATUS_FG_DEAD = 1,
            BAD_STATUS_FG_STONE = 2,
            BAD_STATUS_FG_SLEEP = 4,
            BAD_STATUS_FG_CONFUSION = 8,
            BAD_STATUS_FG_TERROR = 16,
            BAD_STATUS_FG_POISON = 32,
            BAD_STATUS_FG_BLIND = 64,
            BAD_STATUS_FG_CURSE = 128,
            BAD_STATUS_FG_PARALYSIS = 256,
            BAD_STATUS_FG_HEAD = 512,
            BAD_STATUS_FG_ARM = 1024,
            BAD_STATUS_FG_LEG = 2048
        }

        /// <summary>Element Resist Values</summary>
        private RESIST_DATA Resist; // Get/Set might break something, will not be touched
        /// <summary>List of Equipment</summary>
        public EQUIP_DATA Equipment { get; set; }
        /// <summary>Base Stats</summary>
        public ABIL_DATA Natural_Param { get; set; }
        /// <summary>Base Stats + Skills + Equipment</summary>
        private ABIL_DATA Fortify_Param; // Re-calculated by game, will not be touched
        /// <summary>Current HP</summary>
        public int HP { get; set; }
        /// <summary>Current TP</summary>
        public int TP { get; set; }
        public override uint EXP { get; set; }
        public override char[] Name { get; set; }
        /// <summary>Bad Status Effect</summary>
        public BadStatusFlag BadStatusFg { get; set; }
        public override int Level { get; set; }
        /// <summary>Boost</summary>
        private int Boost; // Get/Set might break something, will not be touched
        /// <summary>Portrait Number</summary>
        public int TexNum { get; set; }
        /// <summary>JijikuBit (?)</summary>
        private int JijikuBit; // Get/Set might break something so left alone
        /// <summary>Skill Points</summary>
        public int SP { get; set; }
        /// <summary>Skill Levels</summary>
        public int[] SkillLevel { get; set; } = new int[21];
        /// <summary>Character Number (0-29)</summary>
        public int CharacterNumber { get; set; }
        /// <summary>Base Stats + Skills</summary>
        private ABIL_DATA Skill_Param; // Re-calculated by game, will not be touched
        /// <summary>Slot Number (0-29)</summary>
        public int RegisterIndex { get; set; }
        /// <summary>Female Portrait Flag</summary>
        public int IsFemale { get; set; }
        /// <summary>Class</summary>
        /// <remarks>0: Landsknecht, 1: Survivalist, 2: Protector, 3: Dark Hunter, 4: Ronin, 5: Medic, 6: Alchemist, 7: Troubadour, 8: Hexer</remarks>
        public override int Class { get; set; }
        /// <summary>Portrait Main ID (Texture Class ID)</summary>
        public int TexMainId { get; set; }

        public EO1Character() { }

        public EO1Character(BinaryReader input)
        {
            Resist = new RESIST_DATA(input);
            Equipment = new EQUIP_DATA(input);
            Natural_Param = new ABIL_DATA(input);
            Fortify_Param = new ABIL_DATA(input);
            HP = EndianBitConverter.Little.ToInt32(input.ReadBytes(4), 0);
            TP = EndianBitConverter.Little.ToInt32(input.ReadBytes(4), 0);
            EXP = EndianBitConverter.Little.ToUInt32(input.ReadBytes(4), 0);
            Name = input.ReadChars(18);
            input.ReadBytes(2); // Skip terminator for Name
            BadStatusFg = (BadStatusFlag)EndianBitConverter.Little.ToInt32(input.ReadBytes(4), 0);
            Level = EndianBitConverter.Little.ToInt32(input.ReadBytes(4), 0);
            Boost = EndianBitConverter.Little.ToInt32(input.ReadBytes(4), 0);
            TexNum = EndianBitConverter.Little.ToInt32(input.ReadBytes(4), 0);
            JijikuBit = EndianBitConverter.Little.ToInt32(input.ReadBytes(4), 0);
            SP = EndianBitConverter.Little.ToInt32(input.ReadBytes(4), 0);
            for (int i = 0; i < SkillLevel.Length; i++)
                SkillLevel[i] = EndianBitConverter.Little.ToInt32(input.ReadBytes(4), 0);
            CharacterNumber = EndianBitConverter.Little.ToInt32(input.ReadBytes(4), 0);
            Skill_Param = new ABIL_DATA(input);
            RegisterIndex = EndianBitConverter.Little.ToInt32(input.ReadBytes(4), 0);
            IsFemale = EndianBitConverter.Little.ToInt32(input.ReadBytes(4), 0);
            Class = EndianBitConverter.Little.ToInt32(input.ReadBytes(4), 0);
            TexMainId = EndianBitConverter.Little.ToInt32(input.ReadBytes(4), 0);
        }

        public override string GetName() { return new string(this.Name).Replace("\0", ""); }

        public override string GetClass()
        {
            return Class switch
            {
                0 => "Landsknecht",
                1 => "Survivalist",
                2 => "Protector",
                3 => "Dark Hunter",
                4 => "Ronin",
                5 => "Medic",
                6 => "Alchemist",
                7 => "Troubadour",
                8 => "Hexer",
                _ => "Landsknecht"
            };
        }
    }

    public class EO2Character : Character // PreviousSaveData is EO1Character
    {
        /// <summary>List of Bad Statuses</summary>
        public enum BadStatusFlag
        {
            BAD_STATUS_FG_DEAD = 1,
            BAD_STATUS_FG_STONE = 2,
            BAD_STATUS_FG_SLEEP = 4,
            BAD_STATUS_FG_CONFUSION = 8,
            BAD_STATUS_FG_TERROR = 16,
            BAD_STATUS_FG_POISON = 32,
            BAD_STATUS_FG_BLIND = 64,
            BAD_STATUS_FG_CURSE = 128,
            BAD_STATUS_FG_PARALYSIS = 256,
            BAD_STATUS_FG_HEAD = 512,
            BAD_STATUS_FG_ARM = 1024,
            BAD_STATUS_FG_LEG = 2048
        }

        /// <summary>Class</summary>
        /// <remarks>0: Landsknecht, 1: Survivalist, 2: Protector, 3: Dark Hunter, 4: Ronin, 5: Medic, 6: Alchemist, 7: Troubadour, 8: Hexer , 9: Gunner, 10: War Magus, 11: Beast, 12: BARD100, 14: DLC </remarks>
        public override int Class { get; set; }
        /// <summary>List of Equipment</summary>
        public EQUIP_DATA Equipment;
        /// <summary>Unclear. Either Base Stats + Inherit Bonus, or Inherit Bonus</summary>
        public ABIL_DATA Inherit_Param;
        /// <summary>Base Stats</summary>
        public ABIL_DATA Natural_Param;
        /// <summary>Base Stats + Skills</summary>
        public ABIL_DATA Skill_Param;
        /// <summary>Base Stats + Skills + Equipment</summary>
        public ABIL_DATA Fortify_Param;
        /// <summary>Current Max Level</summary>
        public int MaxLevel;
        /// <summary>Current HP</summary>
        public int HP;
        /// <summary>Current TP</summary>
        public int TP;
        public override uint EXP { get; set; }
        public override char[] Name { get; set; }
        /// <summary>Bad Status Effect</summary>
        public BadStatusFlag BadStatusFg;
        public override int Level { get; set; }
        /// <summary>Boost</summary>
        public int Boost;
        /// <summary>Portrait Number</summary>
        public int TexNum;
        /// <summary>Skill Points</summary>
        public int SP { get; set; }
        /// <summary>Skill Levels</summary>
        public int[] SkillLevel { get; set; } = new int[28];
        /// <summary>Character Number (0-29)</summary>
        public int CharacterNumber;
        /// <summary>Slot Number (0-29)</summary>
        public int RegisterIndex;
        /// <summary>Female Portrait Flag</summary>
        public bool IsFemale;
        /// <summary>Portrait Main ID (Texture Class ID)</summary>
        public int TexMainId;


        public EO2Character() { }

        public EO2Character(BinaryReader input)
        {
            Class = EndianBitConverter.Little.ToInt32(input.ReadBytes(4), 0);
            Equipment = new EQUIP_DATA(input);
            Inherit_Param = new ABIL_DATA(input);
            Natural_Param = new ABIL_DATA(input);
            Skill_Param = new ABIL_DATA(input);
            Fortify_Param = new ABIL_DATA(input);
            MaxLevel = EndianBitConverter.Little.ToInt32(input.ReadBytes(4), 0);
            HP = EndianBitConverter.Little.ToInt32(input.ReadBytes(4), 0);
            TP = EndianBitConverter.Little.ToInt32(input.ReadBytes(4), 0);
            EXP = EndianBitConverter.Little.ToUInt32(input.ReadBytes(4), 0);
            Name = input.ReadChars(18);
            input.ReadBytes(2); // Skip terminator for Name
            BadStatusFg = (BadStatusFlag)EndianBitConverter.Little.ToInt32(input.ReadBytes(4), 0);
            Level = EndianBitConverter.Little.ToInt32(input.ReadBytes(4), 0);
            Boost = EndianBitConverter.Little.ToInt32(input.ReadBytes(4), 0);
            TexNum = EndianBitConverter.Little.ToInt32(input.ReadBytes(4), 0);
            SP = EndianBitConverter.Little.ToInt32(input.ReadBytes(4), 0);
            for (int i = 0; i < SkillLevel.Length; i++)
                SkillLevel[i] = EndianBitConverter.Little.ToInt32(input.ReadBytes(4), 0);
            CharacterNumber = EndianBitConverter.Little.ToInt32(input.ReadBytes(4), 0);
            RegisterIndex = EndianBitConverter.Little.ToInt32(input.ReadBytes(4), 0);
            IsFemale = !EndianBitConverter.ToBoolean(input.ReadBytes(4), 0).Equals(0);
            TexMainId = EndianBitConverter.Little.ToInt32(input.ReadBytes(4), 0);
        }

        public override string GetName() { return new string(this.Name).Replace("\0", ""); }

        public override string GetClass()
        {
            return Class switch
            {
                0 => "Landsknecht",
                1 => "Survivalist",
                2 => "Protector",
                3 => "Dark Hunter",
                4 => "Ronin",
                5 => "Medic",
                6 => "Alchemist",
                7 => "Troubadour",
                8 => "Hexer",
                9 => "Gunner",
                10 => "War Magus",
                11 => "Beast",
                12 => "BARD100", // Unsure what this actually is
                14 => "DLC", // Unsure what this actually is
                _ => "Landsknecht"
            };
        }
    }

    public class EO3Character : Character
    {
        /// <summary>Stats</summary>
        /// <remarks>Max HP, Max TP, STR, VIT, AGI, LUC, TEC, pad</remarks>
        new public struct ABIL_DATA
        {
            /// <summary>Max HP</summary>
            public int HpMax;
            /// <summary>Max TP</summary>
            public int TpMax;
            /// <summary>Strength (STR)</summary>
            public short Str;
            /// <summary>Vitality (VIT)</summary>
            public short Vit;
            /// <summary>Agility (AGI)</summary>
            public short Agi;
            /// <summary>Luck (LUC)</summary>
            public short Luc;
            /// <summary>Technique (TEC)</summary>
            public short Tec;
            /// <summary>Padding</summary>
            public short pad;

            public ABIL_DATA() { }

            public ABIL_DATA(BinaryReader input)
            {
                HpMax = EndianBitConverter.Little.ToInt32(input.ReadBytes(4), 0);
                TpMax = EndianBitConverter.Little.ToInt32(input.ReadBytes(4), 0);
                Str = EndianBitConverter.Little.ToInt16(input.ReadBytes(2), 0);
                Vit = EndianBitConverter.Little.ToInt16(input.ReadBytes(2), 0);
                Agi = EndianBitConverter.Little.ToInt16(input.ReadBytes(2), 0);
                Luc = EndianBitConverter.Little.ToInt16(input.ReadBytes(2), 0);
                Tec = EndianBitConverter.Little.ToInt16(input.ReadBytes(2), 0);
                pad = EndianBitConverter.Little.ToInt16(input.ReadBytes(2), 0);
            }
        }

        /// <summary>Stats</summary>
        /// <remarks>STR, VIT, AGI, LUC, TEC, pad0, pad1, pad2</remarks>
        public struct HOUTEN_COUNT
        {
            /// <summary>Strength (STR)</summary>
            public byte Str;
            /// <summary>Vitality (VIT)</summary>
            public byte Vit;
            /// <summary>Agility (AGI)</summary>
            public byte Agi;
            /// <summary>Luck (LUC)</summary>
            public byte Luc;
            /// <summary>Technique (TEC)</summary>
            public byte Tec;
            /// <summary>Padding</summary>
            public byte pad0;
            /// <summary>Padding</summary>
            public byte pad1;
            /// <summary>Padding</summary>
            public byte pad2;

            public HOUTEN_COUNT() { }

            public HOUTEN_COUNT(BinaryReader input)
            {
                Str = input.ReadByte();
                Vit = input.ReadByte();
                Agi = input.ReadByte();
                Luc = input.ReadByte();
                Tec = input.ReadByte();
                pad0 = input.ReadByte();
                pad1 = input.ReadByte();
                pad2 = input.ReadByte();
            }
        }

        /// <summary>Equipment</summary>
        new public struct  EQUIP_DATA
        {
            
        }

        /// <summary>Items</summary>
        public struct ITEM_DATA
        {

        }


        public byte Flag; // 0x10
        public byte TexNum; // 0x11
        public byte RegisterIndex; // 0x12
        public override int Level { get; set; } // originally byte
        public override int Class { get; set; }
        public int SubClass { get; set; } // originally byte
        public int TexMainId; // 0x1C
        public EQUIP_DATA Equip; // 0x20
        public ABIL_DATA Inherit_Ability; // 0x28
        public ABIL_DATA Natural_Ability; // 0x30
        public ABIL_DATA Skill_Ability; // 0x38
        public ABIL_DATA Fortify_Ability; // 0x40
        public ABIL_DATA Battle_Ability; // 0x48
        public short Hp; // 0x50
        public short Tp; // 0x52
        public override uint EXP { get; set; }
        public override char[] Name { get; set; } // originally string
        public ushort BadStatusFg; // 0x60
        public byte MaxLevel; // 0x62
        public sbyte Boost; // 0x63
        public byte SkillPointLeft; // 0x64
        public byte OriginalCharDataIndex; // 0x65
        public byte[] CommonSkillLevel; // 0x68
        public byte[,] SkillLevel; // 0x70
        public short LimitSkillID; // 0x78
        public byte Order; // 0x7A
        public byte InheritSkillPoint; // 0x7B
        public short NPCNo; // 0x7C
        public byte AIPassNo; // 0x7E
        public byte BkOrder; // 0x7F
        public short BkLimitSkillID; // 0x80
        public HOUTEN_COUNT HoutenCount; // 0x88
        public sbyte CoopMemberIndex; // 0x90
        public byte IsFamale; // 0x91
        public byte[] resv; // 0x98
        public ulong ConstantId; // 0xA0


        public EO3Character() { }

        public EO3Character(BinaryReader input)
        {
            throw new System.NotImplementedException();
        }

        public override string GetName()
        {
            throw new System.NotImplementedException();
        }

        public override string GetClass()
        {
            throw new System.NotImplementedException();
        }
    }
}
