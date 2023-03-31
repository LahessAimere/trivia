using System;
using System.Collections.Generic;
using System.Text;

namespace Trivia
{
    public class Player
    {
        string name;
        int place = 0;
        int purse = 0;
        bool inPenaltyBox = false;
        private bool isGettingOutOfPenaltyBox = false;
        Player nextPlayer;

        public Player(String name)
        {
            this.name = name;
        }

        //the name of the player
        public string Name
        {
            get { return name; }
            set
            {
                if (value != name)
                {
                    name = value;
                }
            }
        }

        //switch to Next Player
        Player nextPlayer;
        public Player Nextplayer
        {
            get { return Nextplayer}
            set { nextPlayer = value}
        }

        //the number of places in the game
        public int Place
        {
            get { return place; }
            set { place = value; }
        }

        //the number of points
        public int Purse
        {
            get { return purse; }
            set { purse = value; }
        }

        //the number of players in penalty
        public bool InPenaltyBox
        {
            get { return inPenaltyBox; }
            set { inPenaltyBox = value; }
        }

        public bool IsGettingOutOfPenaltyBox
        {
            get { return isGettingOutOfPenaltyBox; }
            set { isGettingOutOfPenaltyBox = value; }
        }
    }
}
