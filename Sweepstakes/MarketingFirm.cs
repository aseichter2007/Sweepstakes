using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Sweepstakes
{
    class MarketingFirm
    {
        ISweepstakesManager sweepstakes;
        string name;
        //creates sweepstakes
        public MarketingFirm()
        {
            this.name = NameCompany();
            SetupHandler();
        }

        public void CreateSweepstakes()
        {
            
        }
        private bool ChooseHandler(string handlertype)
        {
            bool output = false;
            switch (handlertype.ToLower())
            {
                case "stack":
                case "s":
                    sweepstakes = new SweepstakesStackManager();
                    output = true;
                        break;
                case "queue":
                case "q":
                    sweepstakes = new SweepstakesQueueManager();
                    output = true;
                    break;

            }
            return output;
        }
        private void SetupHandler()
        {
            bool correctinput = true;
            string message = "What type of sweepstakes management will your company use? stack or queue";
            do
            {
                if (!correctinput)
                {
                    message = "Invalid handler, please try again. Stack or queue, s/q";
                }
                                
                UserInterface.GetInputInline(message);
                correctinput = ChooseHandler(Console.ReadLine());
            } while (!correctinput);
            UserInterface.Clear();
        }
        private string NameCompany()
        {
            return UserInterface.GetinputClear("What is the name of your company?");
        }
        
    }
}
