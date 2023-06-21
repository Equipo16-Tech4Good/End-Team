using System.Security.Cryptography;
using System.Text;

namespace API.Business
{
    public class Encriptacion
    {
        public static string Encrypt(string mensaje)
        {
            string hash = "conding con c"; // --> Frase de encriptación
            byte[] data = UTF8Encoding.UTF8.GetBytes(mensaje); // Array de bytes del mensaje que recibimos por parámetro

            // Protocolos para encriptar
            MD5 md5 = MD5.Create();
            TripleDES tripledes = TripleDES.Create();

            tripledes.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
            tripledes.Mode = CipherMode.CBC;

            ICryptoTransform transform = tripledes.CreateEncryptor();
            byte[] result = transform.TransformFinalBlock(data, 0, data.Length);

            return Convert.ToBase64String(result);
        }

        public static string Decrypt(string mensajeEn)
        {
            string hash = "conding con c";  // --> Frase de encriptación
            byte[] data = Convert.FromBase64String(mensajeEn);

            MD5 md5 = MD5.Create();
            TripleDES tripledes = TripleDES.Create();

            tripledes.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
            tripledes.Mode = CipherMode.CBC;

            ICryptoTransform transform = tripledes.CreateEncryptor();
            byte[] result = transform.TransformFinalBlock(data, 0, data.Length);

            return UTF8Encoding.UTF8.GetString(result);
        }
    }
}
