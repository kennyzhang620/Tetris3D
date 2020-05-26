using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitGame : MonoBehaviour {

    // Use this for initialization

    float defGravity;

    void Start()
    {
        defGravity = Physics.gravity.y;
    }
    public void Pause()
    {
        Physics.gravity = new Vector3(Physics.gravity.x, 0, Physics.gravity.z);

    }
    
    public void Continue()
    {
        print("grav: " + defGravity);
        Physics.gravity = new Vector3(Physics.gravity.x, defGravity, Physics.gravity.z);
    }
	public void Exit()
    {
        Application.Quit();
        return;
    }
}
