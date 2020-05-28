using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameSettings;
using UnityEngine.UI;

public class GFXModule : MonoBehaviour {

    // Use this for initialization
    int optimizer = 0;
    public int interval = 0;
    float TimeElapsed = 0;
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

    void Start()
    {
        if (GameData.PhysicsOn)
            Switch.value = 1;
        else
            Switch.value = 0;
    }
    void Update()
    {


        TimeElapsed += Time.deltaTime;

        if (TimeElapsed < 5)
        {
            optimizer = (int)(1.0f / Time.deltaTime);
        }
        else if (TimeElapsed > 5 && TimeElapsed < 5.15)
        {
            if (optimizer < 15)
            {
                QualitySettings.SetQualityLevel(0, false);
            }
            if (optimizer > 15 && optimizer < 30)
            {
                QualitySettings.SetQualityLevel(2, false);
            }

            if (optimizer > 30 && optimizer < 45)
            {
                QualitySettings.SetQualityLevel(4, false);
            }

            if (optimizer > 45 && optimizer < 60)
            {
                QualitySettings.SetQualityLevel(5);
            }
            if (optimizer > 60)
            {
                QualitySettings.SetQualityLevel(6);
            }
        }
    }
    
}
