using System;

public class Player
{
	string name;

	public Player (String name)
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
}
