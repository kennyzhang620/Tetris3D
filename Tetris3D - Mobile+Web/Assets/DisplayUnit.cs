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
    public GameObject Pause;
    int counter = 0;

    public bool LimitedDisplay;

    float defaultGravity = 0;

    // Update is called once per frame

    void Start()
    {
        defaultGravity = Physics.gravity.y;
    }
    void Update()
    {
        if (!LimitedDisplay)
        {
            if ((Input.deviceOrientation == DeviceOrientation.LandscapeLeft ||
         Input.deviceOrientation == DeviceOrientation.LandscapeRight) || (Screen.width >= Screen.height))
            {
                //     Debug.Log("we landscape now.");
                Portrait.SetActive(false);
                Landscape.SetActive(true);

                if (counter == 0)
                {
                    GameData.ScanLines = true;
                    counter = 1;
                }
            }
            else if (Input.deviceOrientation == DeviceOrientation.Portrait || (Screen.width < Screen.height))
            {
                //     Debug.Log("we portrait now");
                Portrait.SetActive(true);
                Landscape.SetActive(false);

                if (counter == 1)
                {
                    GameData.ScanLines = true;
                    counter = 0;
                }
            }

            if (Input.GetAxis("Cancel") != 0 && !Pause.activeSelf)
            {
                Pause.SetActive(true);
                Physics.gravity = new Vector3(0, 0, 0);
            }
        }
        
        if (GameData.SpawnerMode == 0 || GameData.SpawnerMode == 2 || GameData.ScanLines)
        {
            PlayerName.text = GameData.CurrentUser.GetUser();
            ScoreData.text = "SCORE: " + GameData.CurrentUser.GetScore().ToString();

           if (GameData.Level > 0 && !LimitedDisplay)
            {
                LevelData.SetActive(true);
                LevelData.GetComponent<Text>().text = "Lv: " + GameData.Level.ToString();
            }

            GameData.ScanLines = false;
        }
    }
}
