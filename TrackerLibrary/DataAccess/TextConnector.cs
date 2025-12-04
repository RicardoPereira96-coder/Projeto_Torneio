using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TrackerLibrary.DataAccess.TextHelpers;


namespace TrackerLibrary.DataAccess
{
    public class TextConnector : IDataConnection
    {
        private const string PrizesFile = "PrizeModels.csv";
        private const string PeopleFile = "PersonModels.csv";
        private const string TeamsFile = "TeamModels.csv";
        private const string TournamentsFile = "TournamentModels.csv";
        private const string MatchupFile = "MatchupModels.csv";
        private const string MatchupEntryFile = "MatchupEntryModels.csv";

        public PersonModel CreatePerson(PersonModel model)
        {
            List<PersonModel> people = PeopleFile.FullFilePath().LoadFile().ConvertToPersonModels();

            int currentId = 1;

            if (people.Count > 0)
            {
                currentId = people.OrderByDescending(x => x.Id).First().Id + 1;
            }
            model.Id = currentId;
            people.Add(model);
            people.SaveToPeopleFile(PeopleFile);
            return model;
        }

        // TODO - Wire up the CreatePrize for text files.
        public PrizeModel CreatePrize(PrizeModel model)
        {
            // Load the text file and convert the text to List<PrizeModel>
            List<PrizeModel> prizes = PrizesFile.FullFilePath().LoadFile().ConvertToPrizeModels();

            // Find the Max Id
            int currentId = 1;

            if (prizes.Count > 0)
            {
                currentId = prizes.OrderByDescending(x => x.Id).First().Id + 1;
            }
            model.Id = currentId;

            //Add the new record with the new ID (max + 1)
            prizes.Add(model);

            //Convert the prizes to list string
            //Save the list string to the text file

            prizes.SaveToPrizeFile(PrizesFile);

            return model;
        }

        public TeamModel CreateTeam(TeamModel model)
        {
            List<TeamModel> teams = TeamsFile.FullFilePath().LoadFile().ConvertToTeamModels();
            int currentId = 1;
            if (teams.Count > 0)
            {
                currentId = teams.OrderByDescending(x => x.Id).First().Id + 1;
            }
            model.Id = currentId;
            teams.Add(model);
            teams.SaveToTeamFile(TeamsFile);
            return model;
        }
        public List<TeamModel> GetTeam_All()
        {
            return TeamsFile.FullFilePath().LoadFile().ConvertToTeamModels();
        }
        
        public List<PersonModel> GetPerson_All()
        {
            return PeopleFile.FullFilePath().LoadFile().ConvertToPersonModels();
        }

        public void CreateTournament(TournamentModel model)
        {
            List<TournamentModel> tournaments = TournamentsFile.FullFilePath().LoadFile().ConvertToTournamentModels();
            int currentId = 1;
            if (tournaments.Count > 0)
            {
                currentId = tournaments.OrderByDescending(x => x.Id).First().Id + 1;
            }
            model.Id = currentId;
            model.SaveRoundsToFile();
            tournaments.Add(model);
            tournaments.SaveToTournamentFile(TournamentsFile);


        }

        public List<TournamentModel> GetTournament_All()
        {
            return TournamentsFile.FullFilePath().LoadFile().ConvertToTournamentModels();
        }

        public void UpdateMatchup(MatchupModel model)
        {
            model.UpdateMatchupToFile();
        }
    }
}
