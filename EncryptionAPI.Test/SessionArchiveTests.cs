namespace EncryptionAPI.Test
{
    using System;
    using System.Diagnostics;
    using System.Text;
    using System.Security.Cryptography;
    using System.Linq;

    using NUnit.Framework;

    [TestFixture]
    public class SessionArchiveTests
    {
        private const int _sessionId = 123; 
        private const string _masterKey = "This is a master key";

        private byte[] _recordKey = new byte[] { 202, 238, 170, 0, 207, 43, 67, 91, 61, 204, 74, 247, 43, 98, 47, 23 };

        [Test]
        public void GenerateRecordKeyTest()
        {
            var recordKey = SessionArchive.GenerateRecordKey(_sessionId, _masterKey);

            Assert.IsNotNull(recordKey);
            Assert.AreEqual(16, recordKey.Length);
            Assert.IsTrue(Enumerable.SequenceEqual(_recordKey, recordKey));

            Trace.WriteLine(Encoding.UTF8.GetString(recordKey), "RecordKey");
        }

        [Test]
        public void DecryptRecordKeyTest()
        {
            var decryptedRecordKey = SessionArchive.DecryptRecordKey(_recordKey, _masterKey);

            Assert.AreEqual(_sessionId, decryptedRecordKey);

            Trace.WriteLine(decryptedRecordKey, "Decrypted RecordKey");
        }

        [Test]
        public void DecryptRecordKeyWithInvalidMasterKeyTest()
        {
            const string invalidMasterKey = "This is an INVALID master key";

            Assert.Throws<CryptographicException>(() => SessionArchive.DecryptRecordKey(_recordKey, invalidMasterKey));
        }
    }
}
