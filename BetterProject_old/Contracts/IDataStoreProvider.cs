using ThirdParty;

namespace BetterProject.Providers
{
    public interface IDataStoreProvider
    {

        public Advertisement GetAdv(string id);
    }
}