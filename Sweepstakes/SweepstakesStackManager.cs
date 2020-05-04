using System;
using System.Collections.Generic;
using System.Text;

namespace Sweepstakes
{
    class SweepstakesStackManager:ISweepstakesManager
    {
        Stack<SweepStakes> sweepStakes;
        public SweepstakesStackManager()
        {
            sweepStakes = new Stack<SweepStakes>();
        }

        public int Getcount()
        {
            return sweepStakes.Count;
        }

        public SweepStakes GetSweepStakes()
        {
            return sweepStakes.Pop();
        }

        public void insertSweepstakes(SweepStakes sweepStakes)
        {
            this.sweepStakes.Push(sweepStakes);
        }
    }
}
