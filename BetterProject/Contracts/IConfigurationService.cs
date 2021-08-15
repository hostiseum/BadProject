namespace BetterProject
{
    public interface IConfigurationService
    {
        T GetSetting<T>(string key);

    }
}