using GameSettings;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour {

	// Use this for initialization
    public void LoadLevel(string Scene)
    {
        GameData.SpawnerMode = 0;
        Physics.gravity = new Vector3(0,-9.80665f,0);
        GameData.IterationCount = 0;
        GameData.Level = 0;
        GameData.CurrentUser.ResetScore();
        SceneManager.LoadScene(Scene);
    }
}
