using GameSettings;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearPieces : MonoBehaviour {
    public GameObject[] Sensors = new GameObject[24];
    public bool NewCycle = false;
    public int Level = 0;
    public int MaxLevel = 14;

    int rm = 0;
    Vector3 init;
	// Use this for initialization
	void Start () {
        init = Sensors[0].transform.position;
	}

    // Update is called once per frame
    void Update()
    {
        bool clearLine = true;

        if (!GameData.PhysicsOn)
        {
            if (GameData.SpawnerMode == 1)
            {
            //    print(GameData.SpawnerMode + " SM " + GameData.ClearLevel + "  " + GameData.ClearLinesA + " rmvarLines " + GameData.ClearLines);
                if (GameData.ClearLines >= -14 + GameData.ClearLinesA && GameData.ClearLevel < Level && GameData.ClearLevel != -1 && rm == 0)
                {
                    rm = 1;
                    for (int i = 0; i < 18; i++)
                    {
                        if (Sensors[i].GetComponent<DetectionModule>().Triggered)
                            Sensors[i].GetComponent<DetectionModule>().Offset = true;

                    }
                    GameData.ClearLines--;
                //    print("RM " + Level);
                }
                else if (GameData.ClearLines < 0 && rm != 0)
                {
                    rm = 0;
                //    print("add + " + GameData.ClearLines);
                    GameData.ClearLevel = -1;
                    if (GameData.ClearLines <= 0)
                    {
                        if (GameData.ClearLines < -1)
                            GameData.ClearLines++;
                        else
                        {
                            GameData.ClearLines++;
                            GameData.ClearLinesA = 0;
                        }
                    }

                }
            }
        }

        if (GameData.SpawnerMode == 0 || NewCycle == true)
        {
           // print("NS");
            GameData.ScanLines = false;
            for (int i = 0; i < 18; i++) // Doubles came back to bite me huh? (Neglect errors and they come back to bite me!)
            {
                if (!Sensors[i].GetComponent<DetectionModule>().Triggered)
                {
                    clearLine = false;
                }

            }

        //    print(clearLine + "CL");
            if (clearLine == true)
            {
                for (int i = 0; i < 18; i++)
                {
                    if (Sensors[i].GetComponent<DetectionModule>().Triggered)
                        Sensors[i].GetComponent<DetectionModule>().Remove = true;
                }

                GameData.CurrentUser.AddScore();
                GameData.ClearLinesA++;

                if (GameData.CurrentUser.GetScore() % 5 == 0)
                {
                    GameData.Level++;
                    Physics.gravity = new Vector3(Physics.gravity.x, Physics.gravity.y - 1, Physics.gravity.z);
                }

                GameData.ClearLevel = Level;
            }

            NewCycle = false;

            if (GameData.SpawnerMode != 0)
            {
                GameData.SpawnerMode = 0;
            }
        }
    }
}
