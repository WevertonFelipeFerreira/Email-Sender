namespace EmailSender.Application.Services.Interfaces
{
    public interface IPasswordProtector
    {
        string Encrypt(string plainText);
        string Decrypt(string cypher);
    }
}
