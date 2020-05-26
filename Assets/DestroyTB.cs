using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTB : MonoBehaviour {

    // Use this for initialization
    void OnTriggerStay(Collider obj)
    {

        Destroy(obj.gameObject);
    }
}
