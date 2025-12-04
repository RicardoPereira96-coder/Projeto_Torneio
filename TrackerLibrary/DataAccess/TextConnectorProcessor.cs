using Azure.Core.GeoJson;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TrackerLibrary.DataAccess.TextHelpers
{
    public static class TextConnectorProcessor
    {
        public static string FullFilePath(this string filename) 
        {
            // C:\data\TournamentTracker\PrizeModels.csv
            return $"{ ConfigurationManager.AppSettings["filePath"]}\\{filename}";
        }
        public static List<string> LoadFile(this string file)
        {
            if (!File.Exists(file))
            {
                return new List<string>();
            }
            return File.ReadAllLines(file).ToList();
        }
        public static List<PrizeModel> ConvertToPrizeModels(this  List<string> lines)
        {
            List<PrizeModel> output = new List<PrizeModel>();

            foreach (string line in lines)
            {
                string[] cols = line.Split(',');
                PrizeModel p = new PrizeModel();
                p.Id = int.Parse(cols[0]);
                p.PlaceNumber = int.Parse(cols[1]);
                p.PlaceName = cols[2];
                p.PrizeAmount = decimal.Parse(cols[3]);
                p.PrizePercentage = double.Parse(cols[4]);
                output.Add(p);
            }
            return output ;
        }
        public static List<PersonModel> ConvertToPersonModels(this List<string> lines)
        {
            List<PersonModel> output = new List<PersonModel>();
            
            foreach (string line in lines)
            {
                string[] cols = line.Split(',');
                PersonModel p = new PersonModel();
                p.Id = int.Parse(cols[0]);
                p.FirstName = cols[1];
                p.LastName = cols[2];
                p.EmailAddress = cols[3];
                p.CellphoneNumber = cols[4];
                output.Add(p);
            }
            return output;
        }
        public static void SaveToPrizeFile(this List<PrizeModel> models, string filename)
        {
            List<string> lines = new List<string>();

            foreach (PrizeModel p in models)
            {
                lines.Add($"{p.Id},{p.PlaceNumber},{p.PlaceName},{p.PrizeAmount},{p.PrizePercentage}");
            }

            File.WriteAllLines(filename.FullFilePath(), lines);
        }
        public static void SaveToPeopleFile(this List<PersonModel> models, string filename)
        {
            List<string> lines = new List<string>();
            foreach (PersonModel p in models)
            {
                lines.Add($"{p.Id},{p.FirstName},{p.LastName},{p.EmailAddress},{p.CellphoneNumber}");
            }
            File.WriteAllLines(filename.FullFilePath(), lines);
        }
        public static void SaveToTournamentFile(this List<TournamentModel> models, string fileName)
        {
            List<string> lines = new List<string>();
            foreach (TournamentModel tm in models)
            {
                lines.Add($"{tm.Id},{tm.TournamentName},{tm.EntryFee},{ConvertTeamListToString(tm.EnteredTeams)},{ConvertPrizeListToString(tm.Pries)}, {ConvertRoundListToString(tm.Rounds)}");
            }
            File.WriteAllLines(fileName.FullFilePath(), lines);
        }
        private static string ConvertPrizeListToString(List<PrizeModel> prizes)
        {
            string output = "";
            if (prizes.Count == 0)
            {
                return output;
            }
            foreach (PrizeModel p in prizes)
            {
                output += $"{p.Id}|";
            }
            output = output.TrimEnd('|');
            return output;
        }
        private static string ConvertTeamListToString(List<TeamModel> teams)
        {
            string output = "";
            if (teams.Count == 0)
            {
                return output;
            }
            foreach (TeamModel t in teams)
            {
                output += $"{t.Id}|";
            }
            output = output.TrimEnd('|');
            return output;
        }
        private static string ConvertRoundListToString(List<List<MatchupModel>> rounds)
        {
            // (Rounds id^id^id|id^id^id|id^id^id)
            string output = "";
            if (rounds.Count == 0)
            {
                return output;
            }
            foreach (List<MatchupModel> m in rounds)
            {
                output += $"{ConvertMatchupListToString(m)}|";
            }
            output = output.TrimEnd('|');
            return output;
        }
        private static string ConvertMatchupListToString(List<MatchupModel> matchups)
        {
            string output = "";
            if (matchups.Count == 0)
            {
                return output;
            }
            foreach (MatchupModel m in matchups)
            {
                output += $"{m.Id}^";
            }
            output = output.TrimEnd('^');
            return output;
        }
        public static List<TeamModel> ConvertToTeamModels(this List<string> lines)
        {
            List<TeamModel> output = new();
            List<PersonModel> people = GlobalConfig.PeopleFile.FullFilePath().LoadFile().ConvertToPersonModels();
            foreach (string line in lines)
            {
                string[] cols = line.Split(',');
                TeamModel t = new();
                t.Id = int.Parse(cols[0]);
                t.TeamName = cols[1];

                string[] personIds = cols[2].Split('|');

                foreach (string id in personIds)
                {
                    t.TeamMembers.Add(people.Where(x => x.Id == int.Parse(id)).First());

                }
                output.Add(t);

            }
            return output;
        }
        public static List<TournamentModel> ConvertToTournamentModels(this List<string> lines)
        {

            //id,TournamentName,EntryFee,(id|id|id - entered teams),(id|id|id - prizes),(Rounds - id^id^id|id^id^id|id^id^id)
            List<TournamentModel> output = new List<TournamentModel>();
            List<TeamModel> teams = GlobalConfig.TeamsFile.FullFilePath().LoadFile().ConvertToTeamModels();
            List<PrizeModel> prizes = GlobalConfig.PrizesFile.FullFilePath().LoadFile().ConvertToPrizeModels();
            List<MatchupModel> matchups = GlobalConfig.MatchupFile.FullFilePath().LoadFile().ConvertToMatchupModels();
            foreach (string line in lines)
            {
                string[] cols = line.Split(',');
                TournamentModel tm = new TournamentModel();
                tm.Id = int.Parse(cols[0]);
                tm.TournamentName = cols[1];
                tm.EntryFee = decimal.Parse(cols[2]);
                string[] teamIds = cols[3].Split('|');
                foreach (string id in teamIds)
                {
                    tm.EnteredTeams.Add(teams.Where(x => x.Id == int.Parse(id)).First());
                }
                if (cols[4].Length > 0)
                {
                    string[] prizeIds = cols[4].Split('|');
                    foreach (string id in prizeIds)
                    {
                        tm.Pries.Add(prizes.Where(x => x.Id == int.Parse(id)).First());
                    }
                }
                string [] rounds = cols[5].Split('|');
                List<MatchupModel> ms = new List<MatchupModel>();
                foreach (string round in rounds)
                {
                    string[] msText = round.Split('^');
                    foreach (string matchupModelTextId in msText)
                    {
                       ms.Add(matchups.Where(x => x.Id == int.Parse(matchupModelTextId)).First());
                    }
                    tm.Rounds.Add(ms);
                }
                output.Add(tm);
            }
            return output;
        }
        public static void SaveToTournamentFile(this List<TournamentModel> models)
        {
            List<string> lines = new();
            foreach (TournamentModel tm in models)
            {
                lines.Add($@"{tm.Id},{tm.TournamentName},{tm.EntryFee},{ConvertTeamListToString(tm.EnteredTeams)},{ConvertPrizeListToString(tm.Pries)},{ConvertRoundListToString(tm.Rounds)}");
            }
            ;
            File.WriteAllLines(GlobalConfig.TournamentsFile.FullFilePath(), lines);
        }
        public static void SaveToTeamFile(this List<TeamModel> models, string filename)
        {
            //id,TeamName,PersonId|PersonId|PersonId - PersonId is from the People file
            List<string> lines = new List<string>();
            foreach (TeamModel t in models)
            {
                lines.Add($"{t.Id},{t.TeamName},{ConvertPeopleListToString(t.TeamMembers)}");
            }
            File.WriteAllLines(filename.FullFilePath(), lines);
        }
        public static void SaveRoundsToFile(this TournamentModel model)
        {
            foreach (List<MatchupModel> round in model.Rounds)
            {
                foreach (MatchupModel matchup in round)
                {
                    //load all of the matchups from file
                    //get top id and add one
                    //store the id
                    //save the matchup record
                    matchup.SaveMatchupToFile();
                }
            }
        }
        public static void SaveMatchupToFile(this MatchupModel matchup)
        {
            List<MatchupModel> matchups = GlobalConfig.MatchupFile.FullFilePath().LoadFile().ConvertToMatchupModels();

            int nextId = 1;
            if (matchups.Count > 0)
            {
                nextId = matchups.OrderByDescending(x => x.Id).First().Id + 1;
            }
            matchup.Id = nextId;
            matchups.Add(matchup);

            foreach (MatchupEntryModel entry in matchup.Entries)
            {
                entry.SaveEntryToFile();
            }
            // save to file
            List<string> lines = new();
            foreach (MatchupModel m in matchups)
            {
                string winner = "";
                if (m.Winner != null)
                {
                    winner = m.Winner.Id.ToString();
                }
                lines.Add($"{m.Id},{ConvertMatchupEntryListToString(m.Entries)},{winner},{m.MatchupRound}");

            }
            

            File.WriteAllLines(GlobalConfig.MatchupFile.FullFilePath(), lines);


        }
        public static void UpdateMatchupToFile(this MatchupModel matchup)
        {
            List<MatchupModel> matchups = GlobalConfig.MatchupFile.FullFilePath().LoadFile().ConvertToMatchupModels();

            MatchupModel oldMatchup = new();

            foreach (MatchupModel m in matchups)
            {
                if (m.Id == matchup.Id)
                {
                    oldMatchup = m;
                }
            }

            matchups.Remove(oldMatchup);
            matchups.Add(matchup);
            foreach (MatchupEntryModel entry in matchup.Entries)
            {
                entry.UpdateEntryToFile();
            }
            // save to file
            List<string> lines = new();
            foreach (MatchupModel m in matchups)
            {
                string winner = "";
                if (m.Winner != null)
                {
                    winner = m.Winner.Id.ToString();
                }
                lines.Add($"{m.Id},{ConvertMatchupEntryListToString(m.Entries)},{winner},{m.MatchupRound}");

            }
            ;

            File.WriteAllLines(GlobalConfig.MatchupFile.FullFilePath(), lines);
        }
        public static void UpdateEntryToFile(this MatchupEntryModel entry)
        {
            List<MatchupEntryModel> entries = GlobalConfig.MatchupEntryFile.FullFilePath().LoadFile().ConvertToMatchupEntryModels();
            MatchupEntryModel oldEntry = new();
            foreach (MatchupEntryModel e in entries)
            {
                if (e.Id == entry.Id)
                {
                    oldEntry = e;
                }
            }

            entries.Remove(oldEntry);
            entries.Add(entry);

            List<string> lines = new();

            foreach (MatchupEntryModel e in entries)
            {
                string parent = "";
                if (e.ParentMatchup != null)
                {
                    parent = e.ParentMatchup.Id.ToString();
                }
                string teamCompeting = "";
                if (e.TeamCompeting != null)
                {
                    teamCompeting = e.TeamCompeting.Id.ToString();
                }
                lines.Add($"{e.Id},{teamCompeting},{e.Score},{parent}");
            }
            ;

            File.WriteAllLines(GlobalConfig.MatchupEntryFile.FullFilePath(), lines);
        }
        private static string ConvertMatchupEntryListToString(List<MatchupEntryModel> entries)
        {
            string output = "";
            if (entries.Count == 0)
            {
                return output;
            }
            foreach (MatchupEntryModel me in entries)
            {
                output += $"{me.Id}|";
            }
            output = output.Substring(0, output.Length - 1);
            return output;
        }
        public static void SaveEntryToFile(this MatchupEntryModel entry)
        {

            List<MatchupEntryModel> entries = GlobalConfig.MatchupEntryFile.FullFilePath().LoadFile().ConvertToMatchupEntryModels();
            int nextId = 1;
            if (entries.Count > 0)
            {
                nextId = entries.OrderByDescending(x => x.Id).First().Id + 1;
            }
            entry.Id = nextId;
            // save here
            entries.Add(entry);

            List<string> lines = new();
            foreach (MatchupEntryModel e in entries)
            {
                string parent = "";
                if (e.ParentMatchup != null)
                {
                    parent = e.ParentMatchup.Id.ToString();
                }
                string teamCompeting = "";
                if (e.TeamCompeting != null)
                {
                    teamCompeting = e.TeamCompeting.Id.ToString();
                }
                lines.Add($"{e.Id},{teamCompeting},{e.Score},{parent}");
            }
            ;

            File.WriteAllLines(GlobalConfig.MatchupEntryFile.FullFilePath(), lines);
        }
        public static List<MatchupEntryModel> ConvertToMatchupEntryModels(this List<string> lines)
        {
            List<MatchupEntryModel> output = new();
            foreach (string line in lines)
            {
                string[] cols = line.Split(',');
                MatchupEntryModel me = new();
                me.Id = int.Parse(cols[0]);
                if (cols[1].Length == 0)
                {
                    me.TeamCompeting = null;
                }
                else
                {
                    me.TeamCompeting = LookupTeamById(int.Parse(cols[1]));
                }
                me.Score = double.Parse(cols[2]);
                if (int.TryParse(cols[3], out int parentId))
                {
                    me.ParentMatchup = LookupMatchupById(parentId);
                }
                else
                {
                    me.ParentMatchup = null;
                }
                output.Add(me);
            }
            return output;
        }
        private static MatchupModel LookupMatchupById(int id)
        {
            List<string> matchups = GlobalConfig.MatchupFile.FullFilePath().LoadFile();
            foreach (string matchup in matchups)
            {
                string[] cols = matchup.Split(',');
                if (cols[0] == id.ToString())
                {
                    List<string> matchingMatchups = new();
                    matchingMatchups.Add(matchup);
                    return matchingMatchups.ConvertToMatchupModels().First();
                }
            }
            return null;
        }
        private static List<MatchupEntryModel> ConvertStringToMatchupEntryModels(string input)
        {
            string[] ids = input.Split('|');
            List<MatchupEntryModel> output = new();
            List<string> entries = GlobalConfig.MatchupEntryFile.FullFilePath().LoadFile();
            List<string> matchingEntries = new();
            foreach (string id in ids)
            {
                foreach (string entry in entries)
                {
                    string[] cols = entry.Split(',');
                    if (cols[0] == id)
                    {
                        matchingEntries.Add(entry);
                    }
                }

            }
            output = matchingEntries.ConvertToMatchupEntryModels();

            return output;
        }
        private static TeamModel LookupTeamById(int id)
        {

            List<string> teams = GlobalConfig.TeamsFile.FullFilePath().LoadFile();
            foreach (string team in teams)
            {
                string[] cols = team.Split(',');
                if (cols[0] == id.ToString())
                {
                    List<string> matchingTeams = new();
                    matchingTeams.Add(team);
                    return matchingTeams.ConvertToTeamModels().First();
                }
            }
            return null;
        }
        public static List<MatchupModel> ConvertToMatchupModels(this List<string> lines)
        {
            List<MatchupModel> output = new List<MatchupModel>();
            foreach (string line in lines)
            {
                string[] cols = line.Split(',');
                MatchupModel m = new MatchupModel();
                m.Id = int.Parse(cols[0]);
                
                m.Entries = ConvertStringToMatchupEntryModels(cols[1]);
                if (cols[2].Length == 0)
                {
                    m.Winner = null;
                }
                else
                {
                    m.Winner = LookupTeamById(int.Parse(cols[2]));
                }

                m.MatchupRound = int.Parse(cols[3]);
                output.Add(m);
            }
            return output;
        }
        private static string ConvertPeopleListToString(List<PersonModel> people)
        {
            string output = "";
            if (people.Count == 0)
            {
                return output;
            }
            foreach (PersonModel p in people)
            {
                output += $"{p.Id}|";
            }
            output = output.TrimEnd('|');
            return output;
        }


    }
}
