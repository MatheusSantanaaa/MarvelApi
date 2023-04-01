namespace MarvelApi.Interfaces
{
    public interface IConsumoApiMarvelClient
    {
        string GerarMd5Hash(string input);
        string AcessarMetodoCharactersApiMarvel(string publicKey, string privateKey);
    }
}
