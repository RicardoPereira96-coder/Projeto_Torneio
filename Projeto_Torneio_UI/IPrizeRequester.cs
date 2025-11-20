using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerLibrary;

namespace Projeto_Torneio_UI
{
    public interface IPrizeRequester
    {
        void PrizeComplete(PrizeModel model);

    }
}
