using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;


namespace PEC.Classes
{
    public class Cryptografia
    {
        private static readonly TripleDESCryptoServiceProvider Des = new TripleDESCryptoServiceProvider();
        private static readonly MD5CryptoServiceProvider HasHmd5 = new MD5CryptoServiceProvider();

        public enum Tipo_Operacao
        {
            Cifra = 0,
            Decifra = 1
        }

        public static string Criptografia(string Texto, Tipo_Operacao Operacao)
        {
            try
            {
                switch (Operacao)
                {
                    case Tipo_Operacao.Cifra:
                        return Cifra(Texto);
                    case Tipo_Operacao.Decifra:
                        return Decifra(Texto);
                    default:
                        return string.Empty;
                }
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }

        private static string MyKey()
        {
            return "123456";
        }

        private static string Decifra(string Texto)
        {
            Des.Key = HasHmd5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(MyKey()));
            Des.Mode = CipherMode.ECB;
            ICryptoTransform desdencrypt = Des.CreateDecryptor();
            byte[] buff = Convert.FromBase64String(Texto);
            return ASCIIEncoding.ASCII.GetString(desdencrypt.TransformFinalBlock(buff, 0, buff.Length));
        }

        private static string Cifra(string Texto)
        {
            Des.Key = HasHmd5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(MyKey()));
            Des.Mode = CipherMode.ECB;
            ICryptoTransform desdencrypt = Des.CreateEncryptor();
            dynamic MyASCIIEncoding = new ASCIIEncoding();
            byte[] buff = ASCIIEncoding.ASCII.GetBytes(Texto);
            return Convert.ToBase64String(desdencrypt.TransformFinalBlock(buff, 0, buff.Length));
        }
    }
}
