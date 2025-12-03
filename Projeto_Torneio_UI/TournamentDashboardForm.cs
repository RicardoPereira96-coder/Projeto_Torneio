using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrackerLibrary;

namespace Projeto_Torneio_UI
{
    public partial class TournamentDashboardForm : Form
    {
        List<TournamentModel> tournaments = GlobalConfig.Connection.GetTournament_All();

        public TournamentDashboardForm()
        {
            InitializeComponent();
            wireLists();
        }

        private void wireLists()
        {
            loadExistingTournamentDropDown.DataSource = tournaments;
            loadExistingTournamentDropDown.DisplayMember = "TournamentName";
        }
        private void createTournamentButton_Click(object sender, EventArgs e)
        {
            CreateTournamentForm frm = new();
            frm.Show();
        }

        private void loadTournamentButton_Click(object sender, EventArgs e)
        {
            
        }
    }
}