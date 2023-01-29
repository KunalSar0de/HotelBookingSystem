using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace HotelManagement.Service.Impl
{
	public class PasswordManagementService : IPasswordManagementService
	{
		private readonly IConfiguration _configuration;

		public PasswordManagementService(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public string GeneratePassword(string passwordString,int custId)
		{
			var salt = _configuration["Password:Salt"]+custId;		
            
			byte[] bytesToBeEncrypted = Encoding.UTF8.GetBytes(passwordString);
            byte[] passwordBytes = Encoding.UTF8.GetBytes(salt);
        
            passwordBytes = SHA256.Create().ComputeHash(passwordBytes);
        
            byte[] bytesEncrypted = AES_Encrypt(bytesToBeEncrypted, passwordBytes);
        
            string encryptedResult = Convert.ToBase64String(bytesEncrypted);    
			return encryptedResult;
		}

		public string DecodePassword(int custId,string encryptedResult)
		{
			var salt = _configuration["Password:Salt"]+custId;	
	
			byte[] bytesToBeDecrypted = Convert.FromBase64String(encryptedResult);
            byte[] passwordBytesdecrypt = Encoding.UTF8.GetBytes(salt);
            passwordBytesdecrypt = SHA256.Create().ComputeHash(passwordBytesdecrypt);
        
            byte[] bytesDecrypted = AES_Decrypt(bytesToBeDecrypted, passwordBytesdecrypt);
        
            string decryptedResult = Encoding.UTF8.GetString(bytesDecrypted);
			return decryptedResult;
		}

		public byte[] AES_Encrypt(byte[] bytesToBeEncrypted, byte[] passwordBytes)
        {
            byte[]? encryptedBytes = null;

            byte[] saltBytes = new byte[] { 2, 1, 7, 3, 6, 4, 8, 5 };
        
            using (MemoryStream ms = new MemoryStream())
            {
                using (var AES = Aes.Create())
                {
                    AES.KeySize = 256;
                    AES.BlockSize = 128;
        
                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);
        
                    AES.Mode = CipherMode.CBC;
        
                    using (var cs = new CryptoStream(ms, AES.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length);
                        cs.Close();
                    }
                    encryptedBytes = ms.ToArray();
                }
            }
            return encryptedBytes;
        }
        
        public byte[] AES_Decrypt(byte[] bytesToBeDecrypted, byte[] passwordBytes)
        {
            byte[]? decryptedBytes = null;
        
            byte[] saltBytes = new byte[] { 2, 1, 7, 3, 6, 4, 8, 5 };
        
            using (MemoryStream ms = new MemoryStream())
            {
                using (var AES = Aes.Create())
                {
                    AES.KeySize = 256;
                    AES.BlockSize = 128;
        
                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);
        
                    AES.Mode = CipherMode.CBC;
        
                    using (var cs = new CryptoStream(ms, AES.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeDecrypted, 0, bytesToBeDecrypted.Length);
                        cs.Close();
                    }
                    decryptedBytes = ms.ToArray();
                }
            }      
            return decryptedBytes;
        }	


		
		// public HashedPassword HashPassword(string password, byte[] salt)
        // {
        //     // Derive a 256-bit subkey (use HMACSHA1 with 10,000 iterations)
        //     var hashed = KeyDerivation.Pbkdf2(
        //         password,
        //         salt,
        //         KeyDerivationPrf.HMACSHA1,
        //         iterationCount: 10000,
        //         numBytesRequested: 256 / 8);
            
        //     return new HashedPassword(hashed, salt);
        // }	
	}
}