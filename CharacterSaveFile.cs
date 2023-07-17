using System.IO;

namespace EO_Bank
{
    /// <summary>
    /// Base class for character saves. Includes common code, as well as values that are consistent
    /// across every game.
    /// </summary>
    public class CharacterSaveFile
    {
        /// <summary>
        /// The bytes of the provided input file.
        /// </summary>
        public byte[] Data;
        public Character Character;
        public char[] GuildName;

        public CharacterSaveFile() { }

        /// <summary>
        /// For loading character saves, which are decrypted copies of normal character save data
        /// </summary>
        /// <param name="path">The path to the character save file.</param>
        public CharacterSaveFile(string path, int game)
        {
            Data = File.ReadAllBytes(path);
            using var input = new BinaryReader(new MemoryStream(Data));
            Character = game switch
            {
                2 => new EO2Character(input),
                3 => new EO3Character(input),
                _ => new EO1Character(input),
            };
            GuildName = input.ReadChars(16);
        }

        /// <summary>
        /// For creating character saves, which are decrypted copies of normal character save data
        /// </summary
        /// /// <param name="path">Where to write the character save to.</param>
        /// <param name="character">The character to be saved.</param>
        /// /// <param name="guildname">The name of the character's guild.</param>
        public CharacterSaveFile(string path, Character character, char[] guildname)
        {
            
        }
    }

    public class EO1CharacterSave
    {

    }

    public class EO2CharacterSave
    {

    }

    public class EO3CharacterSave
    {

    }
}