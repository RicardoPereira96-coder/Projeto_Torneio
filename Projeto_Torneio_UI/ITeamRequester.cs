using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_Torneio_UI
{
    public interface ITeamRequester
    {
        void TeamComplete(TrackerLibrary.TeamModel model);
    }
}
