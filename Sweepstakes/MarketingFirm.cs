using MailKit.Net.Smtp;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Transactions;

namespace Sweepstakes 
{
    class MarketingFirm//:INotification //this class needed all different inputs, and while I probably could have fixed it up nice, I decided it was kind of useless here. 
    {
        ISweepstakesManager sweepstakescolleciton;
        string name;
        string[] company;
        bool emailflag = false;
        //creates sweepstakes
        public MarketingFirm()
        {
            company = new string[3];
            this.name = NameCompany();
            emailflag=ConfigEmail();
            SetupHandler();
            Runsweepstakes();
        }
        private bool ConfigEmail()
        {
            bool output = false;
            string input =UserInterface.GetinputClear("Will your company send emails to all contestants?");
            if (input.Trim() == "y" || input.Trim() == "yes")
            {
                string badinput = "";
                do
                {
                    output = true;
                    company[0] = name;
                    company[1] = UserInterface.GetEmail("Enter the address the gmail address your company sends email from. This account must have less secure application acess enabled.");
                    company[2] = UserInterface.GetInputInline("enter the password associated with this acocunt.  Get it right, or you will be mad later.");
                    UserInterface.DisplayInline("");
                    UserInterface.DisplayInline(company[0]);
                    UserInterface.DisplayInline(company[1]);
                    UserInterface.DisplayInline(company[2]);
                    badinput = UserInterface.GetInputInline("are these values correct?");
                  } while (badinput.Trim().ToLower() !="y" && badinput.Trim().ToLower() != "yes");
            }
            return output;
        }
        private void Runsweepstakes()
        {
            do
            {
                bool baduser = true;
                UserInterface.DisplayOnly("Welcome to the " + name + " Sweepstakes management portal");
                UserInterface.DisplayInline("Here you can create and manage sweepstakes.");
                UserInterface.DisplayInline("1.Create a new sweepstakes.");
                if (sweepstakescolleciton.Getcount()>0)
                {
                    UserInterface.DisplayInline("2.Work on a previous sweepstake");
                    UserInterface.DisplayInline("3.Finalize and choose winner of next sweepstake");
                }
           
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
            if (sweepstakescolleciton.Getcount() > 0)
            {
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
            }
            else
            {
                switch (input.Trim())
                {
                    case "1":
                    case "create":
                        CreateSweepstakes();
                        output = false;
                        break;
                }
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
            Contestant winner = working.ChooseAndDisplayWinnerInfo();
            string input = UserInterface.GetInputInline("Would you like to return this sweepstakes to the queue or stack?");
            if (input.Trim().ToLower()=="y"||input.Trim().ToLower()=="yes")
            {
                sweepstakescolleciton.insertSweepstakes(working);
                UserInterface.GetInputInline("Sweepstakes returned, good luck with the duplicate winner.  Enter to continue.");
            }
            else
            {
                UserInterface.GetInputInline("I hope you were done with it, that sweepstakes is gone forever. Enter to continue.");
            }
            MailNotify(working, winner);

        }
        private void MailNotify(SweepStakes sweepstakes,Contestant winner)
        {
            if (emailflag)
            {
                SmtpClient smtpClient = new MailKit.Net.Smtp.SmtpClient();
                smtpClient.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.Auto);
                smtpClient.Authenticate(company[1], company[2]);

                foreach (Contestant loser in sweepstakes)
                {
                    if (loser.email!=winner.email)
                    {
                        loser.Notify(company, winner.firstName + " has won " + sweepstakes.name, smtpClient);
                    }
                }
                winner.Notify(company, "You have won " + sweepstakes.name + "Contact us to claim your prize.",smtpClient);
                smtpClient.Disconnect(true);

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
                                
                UserInterface.DisplayInline(message);
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
