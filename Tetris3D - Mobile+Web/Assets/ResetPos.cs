using GameSettings;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResetPos : MonoBehaviour {

    Slider current;
    public int Dir = 0; // 0 x 1 y
    int snap;
	// Use this for initialization
	void Start () {
        current = GetComponent<Slider>();
	}
	
    public void SnapBack()
    {
        current.value = 0;
        snap = 0;
    }

    public void AutoMoveLR()
    {
        GameData.inX = (int)current.value;
    }

    public void AutoMoveUD()
    {
        GameData.inY = (int)-current.value;
    }

    void Update()
    {
        if (current.value != 0)
        {
            if (Dir ==0)
            {
                GameData.inX = (int)current.value;
            }
            else
            {
                if (current.value > 0 && snap == 0)
                {
                    GameData.inY = (int)-current.value;
                    snap = 1;
                }
                else if (current.value < 0)
                    GameData.inY = (int)-current.value;
            }
        }
    }
}
