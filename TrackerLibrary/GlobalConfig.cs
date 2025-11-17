using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary
{
    public static class GlobalConfig
    {
        public static List<IDataConnection> Connections { get; private set; }

        public static void InitializeConnections(DataBaseType db)
        {
            Connections = new List<IDataConnection>();
            if (db == DataBaseType.Sql)
            {
                // TODO - Create SQL Connection
                SqlConnector sql = new SqlConnector();
                Connections.Add(new SqlConnector());
            }
            else if (db == DataBaseType.TextFile)
            {
                // TODO - Create text Connection
                TextConnector text = new TextConnector();
                Connections.Add(new TextConnector());

            }
        }

        public static string CnnString(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }

    }
}
