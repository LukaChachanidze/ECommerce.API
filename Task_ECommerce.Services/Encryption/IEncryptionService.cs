namespace Task_ECommerce.Services.Encryption
{
    /// <summary>
    /// Interface for encryption service
    /// </summary>
    public interface IEncryptionService
    {
        string CreatePasswordHash(string password);
        bool VerifyPasswordHash(string password, string hash);
    }
}
