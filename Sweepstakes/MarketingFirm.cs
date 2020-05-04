using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;

namespace Sweepstakes
{
    class MarketingFirm
    {
        ISweepstakesManager sweepstakescolleciton;
        string name;
        //creates sweepstakes
        public MarketingFirm()
        {
            this.name = NameCompany();
            SetupHandler();
            Runsweepstakes();
        }
        private void Runsweepstakes()
        {
            do
            {
                bool baduser = true;
                UserInterface.DisplayOnly("Welcome to the " + name + "Sweepstakes management portal");
                UserInterface.DisplayInline("Here you can create and manage sweepstakes.");
                UserInterface.DisplayInline("1.Create a new sweepstakes.");
                UserInterface.DisplayInline("2.Work on a previous sweepstake");
                UserInterface.DisplayInline("3.Finalize and choose winner of next sweepstake");
                do
                {
                    string input = UserInterface.GetInputInline("Enter choice.");
                    baduser = RunSweepstakesInput(input);
                } while (baduser);
            }
            while (true);
            
        }
        private bool RunSweepstakesInput(string input)
        {
            bool output = true;
            switch (input.Trim())
            {
                case "1":
                case "create":
                    CreateSweepstakes();
                    output = false;
                    break;
                case "2":
                case "work:":
                    output = false;
                    WorkOnColleciton();
                    break;
                case "3":
                case "finalize":
                    ChooseWinner();
                    output = false;
                    break;
            }
            return output;
        }
        private void WorkOnColleciton()
        {
            SweepStakes working = sweepstakescolleciton.GetSweepStakes();
            UserInterface.DisplayOnly("you are now working on " + working.name);
            UserInterface.DisplayInline("what would you like to do?");
            UserInterface.DisplayInline("1.Continue to add contestants.");
            string input =UserInterface.GetInputInline("Choose your selection, invalid input returns to previous menu.");

            if (input.Trim()=="1")
            {
                working.ContinueAddContestants();
            }
            sweepstakescolleciton.insertSweepstakes(working);
        }
        private void ChooseWinner()
        {
            SweepStakes working = sweepstakescolleciton.GetSweepStakes();
            UserInterface.DisplayOnly("the winner of " + working.name + "is:");
            working.ChooseAndDisplayWinnerInfo();
            string input = UserInterface.GetInputInline("Would you like to return this sweepstakes to the queue or stack?");
            if (input.Trim().ToLower()=="y"||input.Trim().ToLower()=="yes")
            {
                sweepstakescolleciton.insertSweepstakes(working);
                UserInterface.DisplayInline("Sweepstakes returned, good luck with the duplicate winner");
            }
            else
            {
                UserInterface.DisplayInline("I hope you were done with it, that sweepstakes is gone forever.");
            }

        }

        private void CreateSweepstakes()
        {
            string name= UserInterface.GetinputClear("what is the name of the new sweepstakes?");
            SweepStakes thissweepStakes = new SweepStakes(name);
            sweepstakescolleciton.insertSweepstakes(thissweepStakes);
        }
        private bool ChooseHandler(string handlertype)
        {
            bool output = false;
            switch (handlertype.ToLower())
            {
                case "stack":
                case "s":
                    sweepstakescolleciton = new SweepstakesStackManager();
                    output = true;
                        break;
                case "queue":
                case "q":
                    sweepstakescolleciton = new SweepstakesQueueManager();
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
