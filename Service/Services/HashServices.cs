using System.Security.Cryptography;
using System.Text;
using Service.Interfaces;

namespace Service.Services
{
    public class HashService : IHashService
    {
        public async Task<string> CreateHashId(string input)
        {
            using (SHA256 hashAlgo = SHA256.Create())
            {
                string hashId = await GetHashString(hashAlgo, input);
                return hashId;
            }
        }

        public async Task<string> GetHashString(HashAlgorithm algorithm, string input)
        {
            byte[] source = Encoding.ASCII.GetBytes(input);
            using MemoryStream stream = new MemoryStream(source);
            byte[] tempHash = await algorithm.ComputeHashAsync(stream);

            var stringBuilder = new StringBuilder();

            for (int i = 0; i < tempHash.Length; i++)
            {
                stringBuilder.Append(tempHash[i].ToString("x2"));
            }

            return stringBuilder.ToString();
        }

    }
}
