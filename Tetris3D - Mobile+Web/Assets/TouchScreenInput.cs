using System.Collections;
using System.Collections.Generic;
using GameSettings;
using UnityEngine;
using UnityEngine.UI;


public class TouchScreenInput : MonoBehaviour {

    public Vector2 MaximumTouchBounds;
    public Vector2 MinimumTouchBounds;
    private Vector3 fp;   //First touch position
    private Vector3 lp;   //Last touch position
    private float dragDistance;  //minimum distance for a swipe to be registered
                                 // Use this for initialization

    int[] counter = new int[5];
    int preX;

    int currX = 0;

    public Slider PosSlider;

    public Text debug;
    void Start()
    {
        MinimumTouchBounds.x = Screen.width * 0.35f;
        MaximumTouchBounds.x = Screen.width * 0.65f;

        MinimumTouchBounds.y = Screen.height * 0.20f;
        MaximumTouchBounds.y = Screen.height * 0.80f;

        dragDistance = (MaximumTouchBounds.y - MinimumTouchBounds.y) * 10 / 100;
    }

    public void MoveLR(float increment)
    {
        // 1 - 18
        GameData.touchX = increment + 0.540f;

        /*
        if (increment < 100 && increment > -100)
        {
            if (increment != 55)
            {
                counter[0] = preX;
                counter[1] = (int)Mathf.Floor(increment);
            }
            else
            {
                    print(counter[0] + "   " + counter[1]);
                    if (counter[0] > counter[1])
                    {
                    currX--;
                    }
                    else if (counter[0] < counter[1])
                    {
                    currX++;
                    }

                if (currX < 1)
                {
                    GameData.inX--;
                    currX = 0;
                    PointerUp();
                }

                if (currX > 1)
                {
                    GameData.inX++;
                    currX = 0;
                    PointerUp();
                }
                
            }
        }
        else
        {
            GameData.inX = (int)increment;
        }
        */
    }

    public void PointerUp()
    {
        /*
        print("AC");
        for (int i = 0; i < 5; i++)
        {
            counter[i] = 0;
        }

        PosSlider.value = GameData.prevX;
        preX = (int)PosSlider.value;
        GameData.inY = 0;
        */

        GameData.touchX = 0;
    }

    // Update is called once per frame
    void Update () {

      //  debug.text = "TOUCHPOS: " + Input.mousePosition;
        if (Input.touchCount == 1) // user is touching the screen with a single touch
        {
            print("touched");
            Touch touch = Input.GetTouch(0); // get the touch
            debug.text = "TOUCHPOS: " + touch.position;
            if (touch.phase == TouchPhase.Began) //check for the first touch
            {
                fp = touch.position;
                lp = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved) // update the last position based on where they moved
            {
                lp = touch.position;

                //Check if drag distance is greater than 20% of the screen height
                if ((Mathf.Abs(lp.x - fp.x) > dragDistance || Mathf.Abs(lp.y - fp.y) > dragDistance) && (fp.x < MaximumTouchBounds.x && lp.x < MaximumTouchBounds.x
                    && fp.y < MaximumTouchBounds.y && lp.y < MaximumTouchBounds.y))
                {//It's a drag
                 //check if the drag is vertical or horizontal
                    if (!(Mathf.Abs(lp.x - fp.x) > Mathf.Abs(lp.y - fp.y)))
                    {
                        if (lp.y > fp.y)  //If the movement was up
                        {   //Up swipe
                                Debug.Log("Up Swipe");
                                GameData.inY = 1;
                        }
                        else
                        {   //Down swipe
                            Debug.Log("Down Swipe");
                            GameData.inY = -1;
                        }
                    }
                }
                else
                {   //It's a tap as the drag distance is less than 20% of the screen height
                    
                }
            }
            else if (touch.phase == TouchPhase.Ended) //check if the finger is removed from the screen
            {
                lp = touch.position;  //last touch position. Ommitted if you use list
                GameData.inY = 0;
            }
        }
    }
}
