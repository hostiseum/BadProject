using ThirdParty;

namespace BetterProject.Providers
{
    public interface IDataStoreProvider
    {

       Advertisement GetAdv(string id);
    }
}