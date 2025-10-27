using System.Collections.Generic;
using UnityEngine;

public class Leaderboard
{
   public List<LeaderboardItem> users;
}

public class LeaderboardItem {
    public string userid;
    public int score;

    public string getUsername() { 
        return userid.Substring(userid.IndexOf("|"));
    }
}
