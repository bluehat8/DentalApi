using System.Security.Cryptography;
using System.Text;


namespace DentalApi.Cifrado
{
    public static class Encriptado
    {
        public static string EncryptPassword(string _Contraseña)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(_Contraseña));

                StringBuilder stringBuilder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    stringBuilder.Append(bytes[i].ToString("x2"));
                }
                return stringBuilder.ToString();
            }
        }



    }
}
