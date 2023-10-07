using System.Security.Cryptography;
using System.Text;

namespace Shared.Helper
{
    public class RsaKey
    {
        private static string privateKeyPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "keys/private.xml");
        private static string publicKeyPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "keys/public.xml");
        private static void GeneratePrivateAndPublic()
        {
            var rsa = RSA.Create();
            string privateKeyXml = rsa.ToXmlString(true);
            string publicKeyXml = rsa.ToXmlString(false);

            using var privateFile = File.Create(privateKeyPath);
            using var publicFile = File.Create(publicKeyPath);

            privateFile.Write(Encoding.UTF8.GetBytes(privateKeyXml));
            publicFile.Write(Encoding.UTF8.GetBytes(publicKeyXml));
        }

        public static string GetPrivateKey()
        {
            return File.ReadAllText(privateKeyPath);
        }

        public static string GetPublicKey()
        {
            return File.ReadAllText(publicKeyPath);
        }
    }
}
