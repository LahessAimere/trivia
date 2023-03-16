using System;

public class Player
{
    string name;
    int place = 0;
    int purse = 0;

    public Player(String name)
    {
        this.name = name;
    }

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

    Player nextPlayer;
    public Player Nextplayer
    {
        get { return Nextplayer}
        set { nextPlayer = value}
    }

    public int Place
    {
        get { return place; }
        set { place = value; }
    }

    public int Purse
    {
        get{ return purse; }
        set { purse = value; }
    }
}
