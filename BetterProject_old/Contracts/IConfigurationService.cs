namespace BetterProject
{
    public interface IConfigurationService
    {
        public T GetSetting<T>(string key);

    }
}