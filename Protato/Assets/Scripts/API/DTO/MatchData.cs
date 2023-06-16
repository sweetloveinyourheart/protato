using System;
using System.Collections.Generic;

[Serializable]
public class PlayerData
{
    public string _id { get; set; }
    public string email { get; set; }
    public string username { get; set; }
    public DateTime createdAt { get; set; }
}

[Serializable]
public class ResultData
{
    public PlayerData player { get; set; }
    public int score { get; set; }
    public bool victory { get; set; }
}

[Serializable]
public class MatchData
{
    public string _id { get; set; }
    public bool victory { get; set; }
    public List<PlayerData> players { get; set; }
    public List<ResultData> results { get; set; }
    public DateTime createdAt { get; set; }
}


[Serializable]
public class MatchListResult
{
    public string player { get; set; }
    public int score { get; set; }
    public bool victory { get; set; }
}

[Serializable]
public class MatchListData
{
    public string _id { get; set; }
    public List<string> players { get; set; }
    public List<MatchListResult> results { get; set; }
    public DateTime createdAt { get; set; }
}