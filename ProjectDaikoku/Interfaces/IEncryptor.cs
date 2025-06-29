namespace ProjectDaikoku.Intefaces
{
    public interface IEncryptor
    {
        string Encrypt(string input);
        string Decrypt(string input);
    }

}