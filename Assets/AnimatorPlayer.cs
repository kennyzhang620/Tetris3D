using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorPlayer : MonoBehaviour {

    // Use this for initialization
    Animator curr;
    public GameObject obj;
    int active = 0;
    Camera cam;
    
    void Start()
    {
        curr = GetComponent<Animator>();
        cam = GetComponent<Camera>();
    }
	public void Play(string StateName)
    {
        curr.StartPlayback();
        curr.Play(StateName);
    }

    public void Stop()
    {
        curr.Stop();
    }

    public void Reverse()
    {
        curr.speed = -1;
        curr.StartPlayback();
    }
    
    public void EnabledGB()
    {
        obj.SetActive(true);
        
        if (Input.deviceOrientation == DeviceOrientation.Portrait || Screen.width < Screen.height)
        {
            cam.fieldOfView += 20;
        }
        active = 1;
    }

    public void DisabledGB()
    {
        obj.SetActive(false);
    }

    void Update()
    {
        if (active == 1)
        {
            if (Input.deviceOrientation == DeviceOrientation.Portrait || Screen.width < Screen.height)
            {
                cam.fieldOfView = 75;
            }
            else if (Input.deviceOrientation == DeviceOrientation.LandscapeLeft || Input.deviceOrientation == DeviceOrientation.LandscapeRight || Screen.width >= Screen.height)
            {
                cam.fieldOfView = 55;
            }
        }
    }
}
