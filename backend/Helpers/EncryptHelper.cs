using static BCrypt.Net.BCrypt;
using backend.Interfaces;

namespace backend.Helpers
{
    public class EncryptHelper : IEncryptHelper
    {
        public string EncryptPassword(string password)
        {
            return HashPassword(password);
        }

        public bool VerifyPassword(string password, string hash)
        {
            return Verify(password, hash);
        }
    }
}