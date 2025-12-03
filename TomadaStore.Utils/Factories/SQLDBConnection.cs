using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TomadaStore.Utils.Interfaces;

namespace TomadaStore.Utils.Factories
{
    public class SQLDBConnection : DBConnectionFactory
    {

        public override IDBConnection CreateConnection()
        {
            return new SQLDBConnectionImpl();
        }

    }
}
