using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;

namespace TrackerLibrary
{
    public class SqlConnector : IDataConnection
    {
        private const string db = "Tournaments";
        public PersonModel CreatePerson(PersonModel model)
        {
            using (IDbConnection connection = new SqlConnection(GlobalConfig.CnnString(db)))
            {
                var p = new DynamicParameters();
                p.Add("FirstName", model.FirstName);
                p.Add("LastName", model.LastName);
                p.Add("EmailAddress", model.EmailAddress);
                p.Add("CellphoneNumber", model.CellphoneNumber);
                p.Add("id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);

                connection.Execute("dbo.spPeople_Insert", p, commandType: CommandType.StoredProcedure);

                model.Id = p.Get<int>("@id");

                return model;
            }
        }

        public PrizeModel CreatePrize(PrizeModel model)
        {
            using (IDbConnection connection = new SqlConnection(GlobalConfig.CnnString(db)))
            {
                var p = new DynamicParameters();
                p.Add("PlaceNumber", model.PlaceNumber);
                p.Add("PlaceName", model.PlaceName);
                p.Add("PrizeAmount", model.PrizeAmount);
                p.Add("PrizePercentage", model.PrizePercentage);
                p.Add("id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);

                connection.Execute("dbo.spPrizes_Insert", p, commandType: CommandType.StoredProcedure);

                model.Id = p.Get<int>("@id");

                return model;
            }
        }

        public TeamModel CreateTeam(TeamModel model)
        {
            using (IDbConnection connection = new SqlConnection(GlobalConfig.CnnString("Tournaments")))
            {
                var p = new DynamicParameters();
                p.Add("TeamName", model.TeamName);
                p.Add("id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);
                connection.Execute("dbo.spTeams_Insert", p, commandType: CommandType.StoredProcedure);
                model.Id = p.Get<int>("@id");
                foreach (PersonModel tm in model.TeamMembers)
                {
                    var tmParams = new DynamicParameters();
                    tmParams.Add("TeamId", model.Id);
                    tmParams.Add("PersonId", tm.Id);
                    connection.Execute("dbo.spTeamMembers_Insert", tmParams, commandType: CommandType.StoredProcedure);
                }
                return model;
            }
        }

        public void CreateTournament(TournamentModel model)
        {
            using (IDbConnection connection = new SqlConnection(GlobalConfig.CnnString("Tournaments")))
            {
                SaveTournament(connection,model);
                SaveTournamentPrizes(connection, model);
                SaveTournamentEntries(connection, model);
                SaveTournamentRounds(connection, model);
            }
  
        }
        
        private void SaveTournamentPrizes(IDbConnection connection, TournamentModel model)
        {
            foreach (PrizeModel pz in model.Pries)
            {
                var p = new DynamicParameters();
                p.Add("TournamentId", model.Id);
                p.Add("PrizeId", pz.Id);
                connection.Execute("dbo.spTournamentPrizes_Insert", p, commandType: CommandType.StoredProcedure);
            }
        }
        private void SaveTournament(IDbConnection connection, TournamentModel model)
        {
            var p = new DynamicParameters();
            p.Add("TournamentName", model.TournamentName);
            p.Add("EntryFee", model.EntryFee);
            p.Add("id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);
            connection.Execute("dbo.spTournaments_Insert", p, commandType: CommandType.StoredProcedure);
            model.Id = p.Get<int>("@id");

        }
        private void SaveTournamentEntries(IDbConnection connection, TournamentModel model)
        {
            foreach (TeamModel tm in model.EnteredTeams)
            {
                var p = new DynamicParameters();
                p.Add("TournamentId", model.Id);
                p.Add("TeamId", tm.Id);
                connection.Execute("dbo.spTournamentEntries_Insert", p, commandType: CommandType.StoredProcedure);
            }
        }
        private void SaveTournamentRounds(IDbConnection connection, TournamentModel model)
        {
            // TODO - Save Rounds
            // Loop through the Rounds
            // Loop through the Matchups
            // Save the Matchup
            // Loop through the Entries
            // Save the Entries
            var p = new DynamicParameters();
            foreach (List<MatchupModel> round in model.Rounds)
            {
                foreach (MatchupModel matchup in round)
                {
                    p = new DynamicParameters();
                    p.Add("TournamentId", model.Id);
                    p.Add("MatchupRound", matchup.MatchupRound);
                    p.Add("id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);
                    connection.Execute("dbo.spMatchups_Insert", p, commandType: CommandType.StoredProcedure);
                    matchup.Id = p.Get<int>("@id");
                    foreach (MatchupEntryModel entry in matchup.Entries)
                    {
                        p = new DynamicParameters();
                        p.Add("MatchupId", matchup.Id);
                        if (entry.ParentMatchup == null)
                        {
                            p.Add("ParentMatchupId", null);
                        }
                        else
                        {
                            p.Add("ParentMatchupId", entry.ParentMatchup.Id);
                        }
                        if (entry.TeamCompeting == null)
                        {
                            p.Add("TeamCompetingId", null);
                        }
                        else
                        {
                            p.Add("TeamCompetingId", entry.TeamCompeting.Id);
                        }

                        p.Add("id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);
                        connection.Execute("dbo.spMatchupEntries_Insert", p, commandType: CommandType.StoredProcedure);

                    }
                }
            }
        }

        public List<PersonModel> GetPerson_All()
        {
            
            using (IDbConnection connection = new SqlConnection(GlobalConfig.CnnString("Tournaments")))
            {
                return connection.Query<PersonModel>("dbo.spPeople_GetAll").ToList();
            }
        }

        public List<TeamModel> GetTeam_All()
        {
            List<TeamModel> output;
            using (IDbConnection connection = new SqlConnection(GlobalConfig.CnnString("Tournaments")))
            {
                output = connection.Query<TeamModel>("dbo.spTeam_GetAll").ToList();
                foreach (TeamModel team in output)
                {
                    // TODO
                    var p = new DynamicParameters();
                    p.Add("TeamId", team.Id);
                    team.TeamMembers = connection.Query<PersonModel>("dbo.spTeamMembers_GetByTeam", p, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            return output;
        }

        public List<TournamentModel> GetTournament_All()
        {
            List<TournamentModel> output = new();
            using IDbConnection connection = new SqlConnection(GlobalConfig.CnnString(db));
            output = connection.Query<TournamentModel>("spTournaments_GetAll").ToList();
            foreach (TournamentModel tm in output)
            {
                // populate prizes
                DynamicParameters p = new();
                p.Add("@TournamentId", tm.Id);
                tm.Pries = connection.Query<PrizeModel>("dbo.spPrizes_GetByTournament", p, commandType: CommandType.StoredProcedure).ToList();

                // populate teams

                tm.EnteredTeams = connection.Query<TeamModel>("spTeam_GetByTournament", p, commandType: CommandType.StoredProcedure).ToList();
                foreach (TeamModel team in tm.EnteredTeams)
                {
                    DynamicParameters p1 = new();
                    p1.Add("@TeamId", team.Id);
                    team.TeamMembers = connection.Query<PersonModel>("dbo.spTeamMembers_GetByTeam", p1, commandType: CommandType.StoredProcedure).ToList();

                }
                DynamicParameters p2 = new();
                p2.Add("@TournamentId", tm.Id);

                // populate rounds // dbo.spMatchups_GetByTournament;
                List<MatchupModel> matchups = connection.Query<MatchupModel>("dbo.spMatchups_GetByTournament", p2, commandType: CommandType.StoredProcedure).ToList();

                foreach (MatchupModel m in matchups)
                {
                    DynamicParameters p3 = new();
                    p3.Add("@MatchupId", m.Id);

                    m.Entries = connection.Query<MatchupEntryModel>("dbo.spMatchupEntries_GetByMatchup", p3, commandType: CommandType.StoredProcedure).ToList();
                    List<TeamModel> allTeams = GetTeam_All();

                    if (m.WinnerId > 0)
                    {
                        m.Winner = allTeams.Where(x => x.Id == m.WinnerId).First();
                    }

                    foreach (MatchupEntryModel entry in m.Entries)
                    {
                        if (entry.TeamCompetingId > 0)
                        {
                            entry.TeamCompeting = allTeams.Where(x => x.Id == entry.TeamCompetingId).First();
                        }

                        if (entry.ParentMatchupId > 0)
                        {
                            entry.ParentMatchup = matchups.Where(x => x.Id == entry.ParentMatchupId).First();
                        }
                    }
                }

                List<MatchupModel> currRow = new();
                int currRound = 1;
                foreach (MatchupModel m in matchups)
                {
                    if (m.MatchupRound > currRound)
                    {
                        tm.Rounds.Add(currRow);
                        currRow = new();
                        currRound += 1;
                    }
                    currRow.Add(m);
                }
                tm.Rounds.Add(currRow);
            }


            return output;

        }
    
    }
}
