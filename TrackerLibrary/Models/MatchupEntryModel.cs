using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary
{
    public class MatchupEntryModel
    {
        /// <summary>
        /// Identifica o MatchupEntryModel
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Representa uma equipa no matchup
        /// </summary>

        public int TeamCompetingId { get; set; }
        public TeamModel TeamCompeting { get; set; }

        /// <summary>
        /// Representa o resultado desta equipa em particular
        /// </summary>
        public double Score { get; set; }

        public int ParentMatchupId { get; set; }

        /// <summary>
        /// Representa o resultado que a equipa teve em relaçao ao vencedor
        /// </summary>
        public MatchupModel ParentMatchup { get; set; }

    }
}
