using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace EO_Bank
{
    public class DecryptSave
    {
        // AES configuration values.
        private static readonly string Key = "Atlus-inc-SQS3SE";
        private static readonly string IV = "Atlus-inc-SQS3Se";
        private static readonly int BlockSize = 128;
        private static readonly int KeySize = 128;

        /// <summary>
        /// The AES object for handling encryption and decryption of saves.
        /// </summary>
        /// <returns></returns>
        private readonly Aes Aes;

        /// <summary>
        /// The decrypted, unedited bytes of the provided input file.
        /// </summary>
        public byte[] Data;

        public DecryptSave() { }

        /// <summary>
        /// For loading encrypted saves, i.e. as the games write them.
        /// </summary>
        /// <param name="path">The path to the save file.</param>
        public DecryptSave(string path)
        {
            Aes = InitAes();
            var encrypted = File.ReadAllBytes(path);
            Data = Decrypt(encrypted);
        }

        /// <summary>
        /// Writes Data to the given path.
        /// </summary>
        /// <param name="path">Where to write the encrypted save to.</param>
        public virtual void WriteDecryptedSave(string path)
        {
            File.WriteAllBytes(path, Data);
        }

        private byte[] Decrypt(byte[] encrypted)
        {
            return Aes.DecryptCbc(encrypted, Aes.IV);
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
}
