using System.IO;
using System.Windows.Forms;

namespace EO_Bank
{
    public abstract class Character
    {
        // Require further investigation before putting shared variables here

        /// <summary>Name</summary>
        public abstract char[] Name { get; set; }
        /// <summary>Class</summary>
        public abstract int Class { get; set; }

        /// <summary>Level</summary>
        public abstract int Level { get; set; }

        /// <summary>Experience</summary>
        public abstract uint Exp { get; set; }

        public Character() { }

        /// <summary>Get a string version of the Character's Name</summary>
        public abstract string GetName();
        /// <summary>Get a string version of the Character's Class</summary>
        public abstract string GetClass();
    }

    public class EO1Character : Character
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
        public override uint Exp { get; set; }
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
        /// <summary>Character Base Stats + Skills</summary>
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
            Exp = EndianBitConverter.Little.ToUInt32(input.ReadBytes(4), 0);
            Name = input.ReadChars(18);
            input.ReadBytes(2); // Skip terminator for Name
            BadStatusFg = (BadStatusFlag)EndianBitConverter.Little.ToInt32(input.ReadBytes(4), 0);
            Level = EndianBitConverter.Little.ToInt32(input.ReadBytes(4), 0);
            Boost = EndianBitConverter.Little.ToInt32(input.ReadBytes(4), 0);
            TexNum = EndianBitConverter.Little.ToInt32(input.ReadBytes(4), 0);
            JijikuBit = EndianBitConverter.Little.ToInt32(input.ReadBytes(4), 0);
            SP = EndianBitConverter.Little.ToInt32(input.ReadBytes(4), 0);
            for (int i = 0; i < 21; i++)
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

    public class EO2Character : Character
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

        /// <remarks>0: Landsknecht, 1: Survivalist, 2: Protector, 3: Dark Hunter, 4: Ronin, 5: Medic, 6: Alchemist, 7: Troubadour, 8: Hexer , 9: Gunner, 10: War Magus, 11: Beast, 12: BARD100</remarks>
        public override int Class { get; set; }
        public EQUIP_DATA Equip;
        public ABIL_DATA inherit_param;
        public ABIL_DATA natural_param;
        public ABIL_DATA skill_param;
        public ABIL_DATA fortify_param;
        public int MaxLevel;
        public int Hp;
        public int Tp;
        public override uint Exp { get; set; }
        public override char[] Name { get; set; }
        public BadStatusFlag BadStatusFg;
        public override int Level { get; set; }
        public int Boost;
        public int TexNum;
        public int SkillPoint;
        public int[] SkillLevel;
        public int char_no;
        public int RegisterIndex;
        public bool IsFemale;
        public int TexMainId;


        public EO2Character() { }

        public EO2Character(BinaryReader input)
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

    public class EO3Character : Character
    {
        public override char[] Name { get; set; }
        public override int Class { get; set; }
        public override int Level { get; set; }
        public override uint Exp { get; set; }

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
