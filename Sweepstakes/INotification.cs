using System;
using System.Collections.Generic;
using System.Text;

namespace Sweepstakes
{
    interface INotification
    {
        public void Notify(string[] company, string body);
    }
}
