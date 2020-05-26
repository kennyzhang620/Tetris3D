using GameSettings;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddtoLeaderboards : MonoBehaviour {

	// Use this for initialization
    public void AppendLeaderboards()
    {
        GameData.Leaderboards.Add(GameData.CurrentUser);
    }

    public void ChangeUser(string Name)
    {
        GameData.CurrentUser.SetUser(Name);
        GameData.CurrentUser.GenerateUID();
        GameData.CurrentUser.ResetScore();
    }
}
