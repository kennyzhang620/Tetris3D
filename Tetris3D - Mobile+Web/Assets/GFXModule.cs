using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameSettings;
using UnityEngine.UI;

public class GFXModule : MonoBehaviour {

    // Use this for initialization
    int optimizer = 0;
    public int interval = 0;
    int TimeElapsed = 0;
    public Slider Switch;
	public void PhysicsSettings(float input)
    {
        if ((int) input == 0) {
            GameData.PhysicsOn = false;
        }
        else
        {
            GameData.PhysicsOn = true;
        }
    }
	
	// Update is called once per frame
	void Start () {
        optimizer = (int) (1.0f / Time.deltaTime);

        if (TimeElapsed % interval == 0)
        {
            if (optimizer < 30)
            {
                QualitySettings.DecreaseLevel();
            }
            else
            {
                QualitySettings.IncreaseLevel();
            }
        }

        if (GameData.PhysicsOn)
            Switch.value = 1;
        else
            Switch.value = 0;
    }
}
