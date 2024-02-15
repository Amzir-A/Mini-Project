public class Location
{
    public int ID;
    public string Name;
    public string Description;
    public Quest? QuestAvailableHere;
    public Monster? MonsterLivingHere;

    public Location? LocationToNorth;
    public Location? LocationToEast;
    public Location? LocationToSouth;
    public Location? LocationToWest;

    public Location(int id, string name, string description, Quest? questAvailableHere = null, Monster? monsterLivingHere = null)
    {
        ID = id;
        Name = name;
        Description = description;
        QuestAvailableHere = questAvailableHere;
        MonsterLivingHere = monsterLivingHere;
    }

    public string Compass()
    {
        string s = "From here you can go:\n";
        if (LocationToNorth != null)
        {
            s += "    N\n    |\n";
        }
        if (LocationToWest != null)
        {
            s += "W---C";
        }
        else
        {
            s += "    C";
        }
        if (LocationToEast != null)
        {
            s += "---C";
        }
        s += "\n";
        if (LocationToSouth != null)
        {
            s += "    C\n    S\n";
        }
        return s;
    }

    public string NextLocationOptions()
    {
        string s = "";
        if (LocationToNorth != null)
            s += "N";
        if (LocationToEast != null)
            s += "E";
        if (LocationToSouth != null)
            s += "S";
        if (LocationToWest != null)
            s += "W";
        return s;
    }

    public Location? GetLocationAt(string location)
    {
        location = location.ToUpper();
        if (location == "N") return LocationToNorth;
        if (location == "E") return LocationToEast;
        if (location == "S") return LocationToSouth;
        if (location == "W") return LocationToWest;
        return null;
    }
}