using System;
using System.Collections.Generic;
using System.Text;

namespace Sweepstakes
{
    interface ISweepstakesManager
    {
        
       

        public void insertSweepstakes(SweepStakes sweepStakes);
        public SweepStakes GetSweepStakes();
        public int Getcount();
    }
}
