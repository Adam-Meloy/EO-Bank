/// Title: MuscUtil (EndianBitConverter)
/// Author: Skeet, Jon
/// Date: 2009
/// Code version: r285
/// Availability: http://jonskeet.uk/csharp/miscutil/

namespace EO_Bank
{
    /// <summary>
    /// Endianness of a converter
    /// </summary>
    public enum Endianness
    {
        /// <summary>
        /// Little endian - least significant byte first
        /// </summary>
        LittleEndian,
        /// <summary>
        /// Big endian - most significant byte first
        /// </summary>
        BigEndian
    }
}