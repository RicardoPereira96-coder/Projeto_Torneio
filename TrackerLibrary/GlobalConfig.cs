using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerLibrary.DataAccess;

namespace TrackerLibrary
{
    public static class GlobalConfig
    {
        public const string PrizesFile = "PrizeModels.csv";
        public const string PeopleFile = "PersonModels.csv";
        public const string TeamsFile = "TeamModels.csv";
        public const string TournamentsFile = "TournamentModels.csv";
        public const string MatchupFile = "MatchupModels.csv";
        public const string MatchupEntryFile = "MatchupEntryModels.csv";
        
        public static IDataConnection Connection { get; private set; }

        public static void InitializeConnections(DataBaseType db)
        {
            Connection = new SqlConnector();
            if (db == DataBaseType.Sql)
            {
                // TODO - Create SQL Connection
                SqlConnector sql = new SqlConnector();
                Connection = sql;
            }
            else if (db == DataBaseType.TextFile)
            {
                // TODO - Create text Connection
                TextConnector text = new TextConnector();
                Connection = text;

            }
        }

        public static string CnnString(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }
        public static string AppkeyLookup(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

    }
}
