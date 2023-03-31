using System;
using System.Collections.Generic;
using System.Text;

namespace Trivia
{
    class Player
    {
        string name;
        int place = 0;
        int purse = 0;
        bool inPenaltyBox = false;
        Player nextPlayer;

        public Player NextPlayer
        {
            get { return nextPlayer; }
            set { nextPlayer = value; }
        }
        public Player(string name)
        {
            this.name = name;
        }
        public string Name
        {
            get { return name; }
            set
            {
                if(value != name)
                {
                    name = value;
                }
            }
        }
        public int Place
        {
            get { return place; }
            set { place = value; }
        }
        public int Purse
        {
            get { return purse; }
            set{ purse = value; }
        }
        public bool InPenaltyBox
        {
            get { return inPenaltyBox; }
            set { inPenaltyBox = value; }
        }
    }
}
