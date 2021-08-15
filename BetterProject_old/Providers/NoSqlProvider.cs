using System;
using System.Collections.Generic;
using System.Text;
using ThirdParty;

namespace BetterProject.Providers
{
    public class NoSqlProvider : INoSqlProvider
    {

        private NoSqlAdvProvider noSqlAdvProvider;

        public NoSqlProvider()
        {
            noSqlAdvProvider = new NoSqlAdvProvider();
        }

        public Advertisement GetAdv(string id)
        {
            return noSqlAdvProvider.GetAdv(id);
        }
    }
}
