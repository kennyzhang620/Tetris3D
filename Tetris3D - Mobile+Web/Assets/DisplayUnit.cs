using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameSettings;
using UnityEngine.UI;

public class DisplayUnit : MonoBehaviour {

    public GameObject Portrait;
    public GameObject Landscape;
    public Text PlayerName;
    public Text ScoreData;
    public GameObject LevelData;

    // Update is called once per frame
    void Update()
    {
        if ((Input.deviceOrientation == DeviceOrientation.LandscapeLeft ||
     Input.deviceOrientation == DeviceOrientation.LandscapeRight) || (Screen.width >= Screen.height))
        {
            Debug.Log("we landscape now.");
            Portrait.SetActive(false);
            Landscape.SetActive(true);
            GameData.ScanLines = true;
        }
        else if (Input.deviceOrientation == DeviceOrientation.Portrait || (Screen.width < Screen.height))
        {
            Debug.Log("we portrait now");
            Portrait.SetActive(true);
            Landscape.SetActive(false);
            GameData.ScanLines = true;
        }

        if (GameData.SpawnerMode == 0 || GameData.ScanLines)
        {
            PlayerName.text = GameData.CurrentUser.GetUser();
            ScoreData.text = "SCORE: " + GameData.CurrentUser.GetScore().ToString();

           if (GameData.Level > 0)
            {
                LevelData.SetActive(true);
                LevelData.GetComponent<Text>().text = "Lv: " + GameData.Level.ToString();
            }
        }
    }
}
