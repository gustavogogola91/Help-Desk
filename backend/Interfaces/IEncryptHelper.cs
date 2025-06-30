namespace backend.Interfaces
{
    public interface IEncryptHelper
    {
        string EncryptPassword(string password);
        bool VerifyPassword(string password, string hash);
    }
}