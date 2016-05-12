namespace EncryptionAPI
{
    using System;
    using System.IO;
    using System.Text;
    using System.Security.Cryptography;

    public class SessionArchive
    {
        private static readonly byte[] SALT = new byte[] { 0x26, 0xdc, 0xff, 0x00, 0xad, 0xed, 0x7a, 0xee, 0xc5, 0xfe, 0x07, 0xaf, 0x4d, 0x08, 0x22, 0x3c };

        public static byte[] GenerateRecordKey(int sessionId, string masterKey)
        {
            var plain = Encoding.UTF8.GetBytes(sessionId.ToString());
            var pdb = new Rfc2898DeriveBytes(masterKey, SALT);

            using (var rijndael = Rijndael.Create())
            {
                rijndael.Key = pdb.GetBytes(32);
                rijndael.IV = pdb.GetBytes(16);

                using (var memoryStream = new MemoryStream())
                {
                    using (var cryptoStream = new CryptoStream(memoryStream, rijndael.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(plain, 0, plain.Length);
                        cryptoStream.Close();
                    }

                    return memoryStream.ToArray();
                }
            }
         }

        public static int DecryptRecordKey(byte[] recordKey, string masterKey)
        {
            var pdb = new Rfc2898DeriveBytes(masterKey, SALT);

            using (var rijndael = Rijndael.Create())
            {
                rijndael.Key = pdb.GetBytes(32);
                rijndael.IV = pdb.GetBytes(16);

                using (var memoryStream = new MemoryStream())
                {
                    using (var cryptoStream = new CryptoStream(memoryStream, rijndael.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(recordKey, 0, recordKey.Length);
                        cryptoStream.Close();
                    }

                    return Convert.ToInt32(Encoding.UTF8.GetString(memoryStream.ToArray()));
                }
            }
        }
    }
}