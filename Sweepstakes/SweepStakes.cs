using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Transactions;

namespace Sweepstakes
{
    class SweepStakes
    {
        Dictionary<string, Contestant> contestants;
        private int count;
        public string name;
        Random random;

        public SweepStakes(string name)
        {
            contestants = new Dictionary<string, Contestant>();
            random = new Random();
            Sweepstakes(name);
        }


        public void Sweepstakes(string name)
        {
            this.name = name;
            string takenames = "";
            do
            {
                UserInterface.DisplayOnly("register contestants for "+ name);
                CreateContestant();
                UserInterface.DisplayInline("would you like to add another contestant in "+name + "?");
            } while (takenames!="no");
        }
        public void ContinueAddContestants()
        {
            string takenames = "";
            do
            {
                UserInterface.DisplayOnly("register contestants for " + name);
                CreateContestant();
                UserInterface.DisplayInline("would you like to add another contestant in " + name + "?");
            } while (takenames != "no");
        }
    
        public void CreateContestant()
        {
            string response = "";
            Contestant newContestant;
            do
            {
                newContestant = new Contestant(
                   UserInterface.GetinputClear("enter contestant first name."),
                   UserInterface.GetInputInline("enter contestant last name."),
                   UserInterface.GetEmail("enter contestant email"),
                   count.ToString());

                PrintContestantInfo(newContestant);
                response = UserInterface.GetInputInline("is this information correct?");
            } while (response.ToLower() != "y" && response.ToLower() != "yes");

            RegisterContestant(newContestant);
        }
        private void RegisterContestant(Contestant contestant)
        {
            contestants.Add(count.ToString(), contestant);
            count++;
            UserInterface.Clear();
        }
        
        public Contestant PickWinner()
        {
            Contestant winner = new Contestant(null, null, null, null);
            if (contestants.TryGetValue(random.Next(count + 1).ToString(), out winner))
            {  
            }
            else
            {
                winner = PickWinner();
            }
            return winner;
        }
        public void PrintContestantInfo(Contestant contestant)
        {
            UserInterface.DisplayOnly(contestant.firstName);
            UserInterface.DisplayInline(contestant.lastName);
            UserInterface.DisplayInline(contestant.email);
            UserInterface.DisplayInline(contestant.registrationNumber);           
        }
        public void ChooseAndDisplayWinnerInfo()
        {
            PrintContestantInfo(PickWinner());
        }

    }
}
