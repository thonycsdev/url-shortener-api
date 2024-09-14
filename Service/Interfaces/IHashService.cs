using System.Security.Cryptography;

namespace Service.Interfaces
{
    public interface IHashService
    {
        Task<string> CreateHashId(string input);
        Task<string> GetHashString(HashAlgorithm algorithm, string input);
    }
}
