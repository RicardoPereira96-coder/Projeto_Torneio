using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary
{
    /// <summary>
    /// Representa um jogo no torneio
    /// </summary>
    public class MatchupModel
    {
        /// <summary>
        /// O conjunto de equipas que estava envolvido no jogo
        /// </summary>
        public List<MatchupEntryModel> Entries { get; set; } = new List<MatchupEntryModel>();
        /// <summary>
        /// O vencedor da partida
        /// </summary>
        public TeamModel Winner { get; set; }

        /// <summary>
        /// Que ronda e que esta partida pertence
        /// </summary>
        public int MatchupRound { get; set; }

    }
}
