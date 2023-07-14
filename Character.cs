using System.IO;

namespace EO_Bank
{
    public class Character
    {
        public Character() { }

        public static byte[] RebuildBytes()
        {
            return null;
        }
    }

    public class EO1Character : Character
    {
        public struct ABIL_DATA
        {
            /// <summary>
            /// Character Max HP
            /// </summary>
            public int HpMax;
            /// <summary>
            /// Character Max TP
            /// </summary>
            public int TpMax;
            /// <summary>
            /// Character STR
            /// </summary>
            public int Str;
            /// <summary>
            /// Character VIT
            /// </summary>
            public int Vit;
            /// <summary>
            /// Character AGI
            /// </summary>
            public int Agi;
            /// <summary>
            /// Character LUC
            /// </summary>
            public int Luc;
            /// <summary>
            /// Character TEC
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

        /// <summary>
        /// Character Header?
        /// </summary>
        public RESIST_DATA Resist;

        /// <summary>
        /// List of Character Equipment
        /// </summary>
        public int[] Equipment = new int[4];

        /// <summary>
        /// Character Base Stats
        /// </summary>
        public ABIL_DATA Natural_Param;

        /// <summary>
        /// Character Base Stats + Skills + Equipment
        /// </summary>
        public ABIL_DATA Fortify_Param;

        /// <summary>
        /// Character Current HP
        /// </summary>
        public int HP;

        /// <summary>
        /// Character Current TP
        /// </summary>
        public int TP;

        /// <summary>
        /// Character Experience
        /// </summary>
        public uint Exp;

        /// <summary>
        /// Character Name
        /// </summary>
        public char[] Name;

        /// <summary>
        /// Bad Status Effect
        /// </summary>
        public int BadStatusFg;

        /// <summary>
        /// Character Level
        /// </summary>
        public int Level;

        /// <summary>
        /// Boost
        /// </summary>
        public int Boost;

        /// <summary>
        /// Texture Number Used
        /// </summary>
        public int TexNum;

        /// <summary>
        /// JijikaBit (idk)
        /// </summary>
        public int JijikaBit;

        /// <summary>
        /// Character Skill Points
        /// </summary>
        public int SP;

        /// <summary>
        /// Character Skill Levels
        /// </summary>
        public int[] SkillLevel = new int[21];

        /// <summary>
        /// Character Creation Number (shifts up if previous character is deleted)
        /// </summary>
        public int CharacterNumber;

        /// <summary>
        /// Character Base Stats + Skills
        /// </summary>
        public ABIL_DATA Skill_Param;

        /// <summary>
        /// Character Slot Number (0-29)
        /// </summary>
        public int RegisterIndex;

        /// <summary>
        /// Character Female Flag
        /// </summary>
        public int IsFemale;

        /// <summary>
        /// Character Class;
        /// 0: Landsknecht, 1: Survivalist, 2: Protector, 3: Dark Hunter, 4: Ronin, 5: Medic, 6: Alchemist, 7: Troubadour, 8: Hexer
        /// </summary>
        public int Class;

        /// <summary>
        /// Texture Main ID (class id?)
        /// </summary>
        public int TexMainId;

        public EO1Character() { }

        public EO1Character(BinaryReader input)
        {
            Resist = new RESIST_DATA(input);
            for (int i = 0; i < 4; i++)
                Equipment[i] = input.ReadInt32();
            Natural_Param = new ABIL_DATA(input);
            Fortify_Param = new ABIL_DATA(input);
            HP = input.ReadInt32();
            TP = input.ReadInt32();
            Exp = input.ReadUInt32();
            Name = input.ReadChars(18); // Just read every even index to get the name
            input.ReadBytes(2); // Skip terminator for Name
            BadStatusFg = input.ReadInt32();
            Level = input.ReadInt32();
            Boost = input.ReadInt32();
            TexNum = input.ReadInt32();
            JijikaBit = input.ReadInt32();
            SP = input.ReadInt32();
            for (int i = 0; i < 21; i++)
                SkillLevel[i] = input.ReadInt32();
            CharacterNumber = input.ReadInt32();
            Skill_Param = new ABIL_DATA(input);
            RegisterIndex = input.ReadInt32();
            IsFemale = input.ReadInt32();
            Class = input.ReadInt32();
            TexMainId = input.ReadInt32(); // unknown value, but NOT EMPTY like the other gaps that are ignored
        }

        public string PrintName()
        {
            char[] realname = new char[8];
            for (int i = 0; i < 8; i++)
            {
                realname[i] = Name[i * 2];
            }
            return new string(realname).Replace("\0", "");
        }

        public string ClassName()
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
        /// <summary>
        /// eo2 job list here
        /// </summary>
        public int Job;
    }

    public class EO3Character : Character
    {
        /// <summary>
        /// eo3 job list here
        /// </summary>
        public int Job;
        public byte[] list;
        public int Exp;

        public EO3Character() { }

        public EO3Character(BinaryReader input)
        {
            // Name is 18 bytes, with '00' after every character
            //Job = input.ReadInt32();
            list = new byte[218];
            list = input.ReadBytes(218);
        }
    }
}
