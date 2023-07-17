using System.IO;
using System.Windows.Forms;

namespace EO_Bank
{
    public abstract class Character
    {
        // Require further investigation before putting shared variables here

        /// <summary>
        /// Class
        /// </summary>
        public abstract int Class { get; set; }

        /// <summary>
        /// Level
        /// </summary>
        public abstract int Level { get; set; }

        /// <summary>
        /// Experience
        /// </summary>
        public abstract uint Exp { get; set; }

        public Character() { }

        /// <summary>
        /// Get a string version of the Character's Name
        /// </summary>
        public abstract string GetName();
        /// <summary>
        /// Get a string version of the Character's Class
        /// </summary>
        public abstract string GetClass();
    }

    public class EO1Character : Character
    {
        public struct ABIL_DATA
        {
            /// <summary>
            /// Max HP
            /// </summary>
            public int HpMax;
            /// <summary>
            /// Max TP
            /// </summary>
            public int TpMax;
            /// <summary>
            /// Strength (STR)
            /// </summary>
            public int Str;
            /// <summary>
            /// Vitality (VIT)
            /// </summary>
            public int Vit;
            /// <summary>
            /// Agility (AGI)
            /// </summary>
            public int Agi;
            /// <summary>
            /// Luck (LUC)
            /// </summary>
            public int Luc;
            /// <summary>
            /// Technique (TEC)
            /// </summary>
            public int Tec;

            public ABIL_DATA() { }

            public ABIL_DATA(BinaryReader input)
            {
                HpMax = input.ReadInt32();
                TpMax = input.ReadInt32();
                Str = input.ReadInt32();
                Vit = input.ReadInt32();
                Agi = input.ReadInt32();
                Luc = input.ReadInt32();
                Tec = input.ReadInt32();
            }
        }

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
                Slash = input.ReadInt32();
                Shock = input.ReadInt32();
                Thrust = input.ReadInt32();
                Fire = input.ReadInt32();
                Ice = input.ReadInt32();
                Thunder = input.ReadInt32();
                Death = input.ReadInt32();
                Mind = input.ReadInt32();
                Head = input.ReadInt32();
                Arm = input.ReadInt32();
                Leg = input.ReadInt32();
            }
        }

        public struct EQUIP
        {
            /// <summary>
            /// Equipped Weapon
            /// </summary>
            public int Weapon;
            /// <summary>
            /// Equipped Armor in the first slot
            /// </summary>
            public int Armor_1;
            /// <summary>
            /// Equipped Armor in the second slot
            /// </summary>
            public int Armor_2;
            /// <summary>
            /// Equipped Armor in the third slot
            /// </summary>
            public int Armor_3;

            public EQUIP() { }

            public EQUIP(BinaryReader input)
            {
                Weapon = input.ReadInt32();
                Armor_1 = input.ReadInt32();
                Armor_2 = input.ReadInt32();
                Armor_3 = input.ReadInt32();
            }
        }

        /// <summary>
        /// Element Resist Values
        /// </summary>
        public RESIST_DATA Resist; // Get/Set might break something so left alone

        /// <summary>
        /// List of Equipment
        /// </summary>
        public EQUIP Equipment { get; set; }

        /// <summary>
        /// Base Stats
        /// </summary>
        public ABIL_DATA Natural_Param { get; set; }

        /// <summary>
        /// Base Stats + Skills + Equipment
        /// </summary>
        public ABIL_DATA Fortify_Param;

        /// <summary>
        /// Current HP
        /// </summary>
        public int HP { get; set; }

        /// <summary>
        /// Current TP
        /// </summary>
        public int TP { get; set; }

        public override uint Exp { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        public char[] Name { get; set; }

        /// <summary>
        /// Bad Status Effect
        /// </summary>
        public int BadStatusFg { get; set; }

        public override int Level { get; set; }

        /// <summary>
        /// Boost
        /// </summary>
        public int Boost; // Get/Set might break something so left alone

        /// <summary>
        /// Portrait Number
        /// </summary>
        public int TexNum { get; set; }

        /// <summary>
        /// JijikuBit (idk)
        /// </summary>
        public int JijikuBit; // Get/Set might break something so left alone

        /// <summary>
        /// Skill Points
        /// </summary>
        public int SP { get; set; }

        /// <summary>
        /// Skill Levels
        /// </summary>
        public int[] SkillLevel { get; set; } = new int[21];

        /// <summary>
        /// Character Number (shifts up if previous character is deleted)
        /// </summary>
        public int CharacterNumber { get; set; }

        /// <summary>
        /// Character Base Stats + Skills
        /// </summary>
        public ABIL_DATA Skill_Param;

        /// <summary>
        /// Slot Number (0-29)
        /// </summary>
        public int RegisterIndex { get; set; }

        /// <summary>
        /// Female Portrait Flag
        /// </summary>
        public int IsFemale { get; set; }

        public override int Class { get; set; }

        /// <summary>
        /// Portrait Main ID (Texture Class ID?)
        /// </summary>
        public int TexMainId { get; set; }

        public EO1Character() { }

        public EO1Character(BinaryReader input)
        {
            Resist = new RESIST_DATA(input);
            Equipment = new EQUIP(input);
            Natural_Param = new ABIL_DATA(input);
            Fortify_Param = new ABIL_DATA(input);
            HP = input.ReadInt32();
            TP = input.ReadInt32();
            Exp = input.ReadUInt32();
            Name = input.ReadChars(18);
            input.ReadBytes(2); // Skip terminator for Name
            BadStatusFg = input.ReadInt32();
            Level = input.ReadInt32();
            Boost = input.ReadInt32();
            TexNum = input.ReadInt32();
            JijikuBit = input.ReadInt32();
            SP = input.ReadInt32();
            for (int i = 0; i < 21; i++)
                SkillLevel[i] = input.ReadInt32();
            CharacterNumber = input.ReadInt32();
            Skill_Param = new ABIL_DATA(input);
            RegisterIndex = input.ReadInt32();
            IsFemale = input.ReadInt32();
            Class = input.ReadInt32(); // 0: Landsknecht, 1: Survivalist, 2: Protector, 3: Dark Hunter, 4: Ronin, 5: Medic, 6: Alchemist, 7: Troubadour, 8: Hexer
            TexMainId = input.ReadInt32();
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
        public override int Class { get; set; }
        public override int Level { get; set; }
        public override uint Exp { get; set; }

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
