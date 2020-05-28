using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameSettings;

public class DetectionModule : MonoBehaviour {

    public bool Triggered = false;
    public bool Remove = false;
    public bool Offset = false;
	// Use this for initialization
    
    void OnTriggerStay(Collider obj)
    {
        if (obj.tag == "Base")
        {
            Triggered = true;

                if (Offset)
                {
                    obj.gameObject.transform.position = new Vector3(obj.gameObject.transform.position.x, obj.gameObject.transform.position.y - GameData.ClearLinesA, obj.gameObject.transform.position.z);
                    Offset = false;
                    print("y");
                }

                if (Remove == true)
                {
                    //this.gameObject.GetComponent<MeshRenderer>().enabled = true;
                    Destroy(obj.gameObject);
                    print("DESTR");
                    Triggered = false;
                    Remove = false;
                    Offset = false;
                }
        }

    }

    void OnTriggerExit(Collider obj)
    {
        Triggered = false;
        Offset = false;
    }
    
    public void Disable()
    {
        Triggered = false;
    }
}
