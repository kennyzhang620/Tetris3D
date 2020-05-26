using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour {

    public float TimeE;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (TimeE < 1)
            TimeE += Time.deltaTime;
        else
            TimeE = 0;
	}
}
