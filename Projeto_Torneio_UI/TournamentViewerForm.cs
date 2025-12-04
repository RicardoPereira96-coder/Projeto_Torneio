using System.ComponentModel;
using TrackerLibrary;

namespace Projeto_Torneio_UI
{
    public partial class TournamentViewerForm : Form
    {
        private TournamentModel tournament;
        BindingList<int> rounds = new BindingList<int>();
        BindingList<MatchupModel> selectedMatchups = new BindingList<MatchupModel>();

        //BindingSource roundsBinding = new BindingSource();
        //BindingSource matchupsBinding = new BindingSource();

        public TournamentViewerForm(TournamentModel tournamentModel)
        {
            InitializeComponent();

            tournament = tournamentModel;

            wireUpLists();
            LoadFormData();
            LoadRounds();
        }


        private void LoadFormData()
        {
            tournamentName.Text = tournament.TournamentName;
        }

        private void LoadRounds()
        {
            //rounds = new BindingList<int>();
            rounds.Clear();
            rounds.Add(1);
            int currRound = 1;
            foreach (List<MatchupModel> matchups in tournament.Rounds)
            {
                if (matchups.First().MatchupRound > currRound)
                {
                    currRound = matchups.First().MatchupRound;
                    rounds.Add(currRound);

                }
            }
            LoadMatchups(1);

        }
        private void wireUpLists()
        {
            //roundDropDown.DataSource = null;

            roundDropDown.DataSource = rounds;
            matchupListBox.DataSource = selectedMatchups;
            matchupListBox.DisplayMember = "DisplayName";

        }

        private void LoadMatchups(int round)
        {

            foreach (List<MatchupModel> matchups in tournament.Rounds)
            {
                if (matchups.First().MatchupRound == round)
                {
                    selectedMatchups.Clear();
                    foreach (MatchupModel m in matchups)
                    {
                        if (m.Winner == null || !unplayedOnlyCheckBox.Checked)
                        {
                            selectedMatchups.Add(m);
                        }
                    }
                }
            }
            if (selectedMatchups.Count > 0)
            { 
                loadMatchup(selectedMatchups.First());
            }
            DisplayMatchupInfo();

        }
        private void DisplayMatchupInfo()
        {
            bool isVisible = (selectedMatchups.Count > 0);
            teamOneName.Visible = isVisible;
            teamOneScoreValue.Visible = isVisible;
            teamTwoName.Visible = isVisible;
            teamTwoScoreValue.Visible = isVisible;
            versusLabel.Visible = isVisible;
            scoreButton.Visible = isVisible;
        }

        private void teamOneScoreValue_TextChanged(object sender, EventArgs e)
        {

        }
        private void loadMatchup(MatchupModel m)
        {
            if (m == null)
            {
                return;
            }
            for (int i = 0; i < m.Entries.Count; i++)
            {
                if (i == 0)
                {
                    if (m.Entries[0].TeamCompeting != null)
                    {
                        teamOneName.Text = m.Entries[0].TeamCompeting.TeamName;
                        teamOneScoreValue.Text = m.Entries[0].Score.ToString();

                        teamTwoName.Text = "<Bye>";
                        teamTwoScoreValue.Text = "0";
                    }
                    else
                    {
                        teamOneName.Text = "Not set";
                        teamOneScoreValue.Text = "";

                    }
                }

                if (i == 1)
                {
                    if (m.Entries[1].TeamCompeting != null)
                    {

                        teamTwoName.Text = m.Entries[1].TeamCompeting.TeamName;
                        teamTwoScoreValue.Text = m.Entries[1].Score.ToString();
                    }
                    else
                    {
                        teamTwoName.Text = "Not set";
                        teamTwoScoreValue.Text = "";
                    }
                }
            }
        }

        private void matchupListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadMatchup((MatchupModel)matchupListBox.SelectedItem);
        }

        private void roundDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadMatchups((int)roundDropDown.SelectedItem);
        }

        private void TournamentViewerForm_Load(object sender, EventArgs e)
        {

        }

        private void unplayedOnlyCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            LoadMatchups((int)roundDropDown.SelectedItem);
        }

        private void scoreButton_Click(object sender, EventArgs e)
        {
            MatchupModel m = (MatchupModel)matchupListBox.SelectedItem;
            double teamOneScore = 0;
            double teamTwoScore = 0;
            for (int i = 0; i < m.Entries.Count; i++)
            {
                if (i == 0)
                {
                    if (m.Entries[0].TeamCompeting != null)
                    {
                        teamOneName.Text = m.Entries[0].TeamCompeting.TeamName;
                        double scoreVal = 0;
                        bool scoreValid = double.TryParse(teamOneScoreValue.Text, out teamOneScore);
                        if (scoreValid)
                        {
                            m.Entries[0].Score = double.Parse(teamOneScoreValue.Text);
                        }
                    }
                    else
                    {
                        MessageBox.Show("You cannot score a bye team.");
                        return;
                    }
                }
                if (i == 1)
                {
                    if (m.Entries[1].TeamCompeting != null)
                    {
                        
                        double scoreVal = 0;
                        bool scoreValid = double.TryParse(teamTwoScoreValue.Text, out teamTwoScore);
                        if (scoreValid)
                        {
                            m.Entries[1].Score = double.Parse(teamTwoScoreValue.Text);
                        }
                    }
                    else
                    {
                        MessageBox.Show("You cannot score a bye team.");
                        return;
                    }
                   
                }
            }
            if (teamOneScore > teamTwoScore)
            {
                m.Winner = m.Entries[0].TeamCompeting;
            }
            else if (teamTwoScore > teamOneScore)
            {
                m.Winner = m.Entries[1].TeamCompeting;
            }
            else
            {
                MessageBox.Show("Ties are not allowed.");
                return;
            }

            foreach (List<MatchupModel> matchups in tournament.Rounds)
            {
                foreach (MatchupModel rm in matchups)
                {
                    foreach (MatchupEntryModel me in rm.Entries)
                    {
                        if (me.ParentMatchup != null)
                        {
                            if (me.ParentMatchup.Id != m.Id)
                            {
                                me.TeamCompeting = m.Winner;
                                GlobalConfig.Connection.UpdateMatchup(rm);
                            }
                        }
                    }
                }
            }

            LoadMatchups((int)roundDropDown.SelectedItem);


            GlobalConfig.Connection.UpdateMatchup(m);
        }
        
    }
}
