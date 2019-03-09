using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace gs10.Apps
{
    public class Encryption
    {
        const string encryptionKey = "BVS435Xew567428";
        const string replaceCharacterForwardSlash = "@";
        const string replaceCharacterPlusSign = "zZatT";
        public byte[] salt = { 0x22, 0x66, 0x21, 0x8e, 0x44, 0x9d, 0x15, 0x67, 0x82, 0x11, 0x69, 0x12, 0x59 };

        public Encryption()
        {

        }

        public string Encrypt(string clearText)
        {
            if (clearText != null)
            {
                byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);

                using (Aes encryptor = Aes.Create())
                {
                    Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(encryptionKey, salt);
                    encryptor.Key = pdb.GetBytes(32);
                    encryptor.IV = pdb.GetBytes(16);

                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(clearBytes, 0, clearBytes.Length);
                            cs.Close();
                        }
                        clearText = Convert.ToBase64String(ms.ToArray());
                        //remove forward slash because it causes error in url.
                        clearText = clearText.Replace("/", replaceCharacterForwardSlash);
                        clearText = clearText.Replace("+", replaceCharacterPlusSign);
                    }
                }
            }
            return clearText;
        }

        public string Decrypt(string cipherText)
        {
            if (cipherText != null)
            {
                //reinstall forward slash that was removed in Encrypt function.
                cipherText = cipherText.Replace(replaceCharacterForwardSlash, "/");
                cipherText = cipherText.Replace(replaceCharacterPlusSign, "+");

                byte[] cipherBytes = Convert.FromBase64String(cipherText);

                using (Aes encryptor = Aes.Create())
                {
                    Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(encryptionKey, salt);
                    encryptor.Key = pdb.GetBytes(32);
                    encryptor.IV = pdb.GetBytes(16);

                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(cipherBytes, 0, cipherBytes.Length);
                            cs.Close();
                        }
                        cipherText = Encoding.Unicode.GetString(ms.ToArray());
                        
                    }
                }
            }
            return cipherText;
        } 
    }
}