using System;
using System.Collections.Generic;
using System.Text;
using ThirdParty;

namespace BetterProject.Providers
{
    public class SqlProvider : ISqlProvider, IDataStoreProvider
    {
        public SqlProvider() { }

        public Advertisement GetAdv(string id)
        {
            return SQLAdvProvider.GetAdv(id);
        }
    }
}
