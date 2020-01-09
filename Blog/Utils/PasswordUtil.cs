using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Security.Cryptography;

namespace Blog.Utils
{
    public class PasswordUtil
    {
        public string PlainPassword { get; set; }
        public string HashedPassword { get; set; }
        public string Salt { get; set; }
        private int KeyLength = 16;

        public PasswordUtil() { }

        public byte[] GenerateRandomByte(int keyLength)
        {
            RNGCryptoServiceProvider rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            byte[] randomBytes = new byte[keyLength];
            rngCryptoServiceProvider.GetBytes(randomBytes);
            return randomBytes;
        }

        public string GenerateRandomKey(int keyLength)
        {
            return Convert.ToBase64String(this.GenerateRandomByte(keyLength));
        }

        public PasswordUtil GetHashedPassword(string password)
        {
            byte[] saltBytes = this.GenerateRandomByte(this.KeyLength);
            byte[] passwordAsBytes = Encoding.UTF8.GetBytes(password);

            List<byte> passwordWithSaltBytes = new List<byte>();
            passwordWithSaltBytes.AddRange(passwordAsBytes);
            passwordWithSaltBytes.AddRange(saltBytes);

            byte[] hashedPasswordBytes = SHA256.Create().ComputeHash(passwordWithSaltBytes.ToArray());

            string salt = Convert.ToBase64String(saltBytes);
            string hashedPassword = Convert.ToBase64String(hashedPasswordBytes);

            PasswordUtil passwordUtil = new PasswordUtil () { PlainPassword = password, HashedPassword = hashedPassword, Salt = salt };

            return passwordUtil;
        }

        public Boolean comparePassword(PasswordUtil passwordUtil)
        {
            byte[] saltBytes = Convert.FromBase64String(passwordUtil.Salt);
            byte[] passwordAsBytes = Encoding.UTF8.GetBytes(passwordUtil.PlainPassword);

            List<byte> passwordWithSaltBytes = new List<byte>();
            passwordWithSaltBytes.AddRange(passwordAsBytes);
            passwordWithSaltBytes.AddRange(saltBytes);

            byte[] hashedPasswordBytes = SHA256.Create().ComputeHash(passwordWithSaltBytes.ToArray());

            string hashedPassword = Convert.ToBase64String(hashedPasswordBytes);
            
            if( passwordUtil.HashedPassword == hashedPassword)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}