using System;
using System.Collections.Generic;
using System.Text;

namespace Sweepstakes
{
    class SweepstakesQueueManager : ISweepstakesManager
    {
        Queue<SweepStakes> sweepStakes;

        public SweepstakesQueueManager()
        {
            sweepStakes = new Queue<SweepStakes>();
        }

        public SweepStakes GetSweepStakes()
        {
            return sweepStakes.Dequeue();
        }

        public void insertSweepstakes(SweepStakes sweepStakes)
        {
            this.sweepStakes.Enqueue(sweepStakes);
        }
    }
}
