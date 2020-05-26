using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using GameSettings;
using UnityEngine.UI;

public class Spawner : MonoBehaviour {

    public bool NewCycle = false;
    public bool AddScore;
    public bool Decor = false;
    int count = 0;
    public int Quantity = 10;

    public GameObject[] TetrisBlocks = new GameObject[6]; // Allocates a block of sizeof(GameObject)*6 in the heap.
    public Slider TouchSlider;
    public Slider PosSlider;
    public GameObject GameOver;
    Transform pivot;
    int[] counter = new int[5];
    int preX;

    private Vector3 fp;   //First touch position
    private Vector3 lp;   //Last touch position
    private float dragDistance;  //minimum distance for a swipe to be registered
	// Use this for initialization
	void Start () {
        pivot = GetComponent<Transform>();
        GameData.prevX = pivot.position.x;
        GameData.defaultCoords = pivot.position;
        dragDistance = Screen.height * 15 / 100;
    }

    public void MoveLR(float increment)
    {
        // 1 - 18
        if (increment < 100 && increment > -100)
        {
            if (increment != 55)
            {
                counter[0] = preX;
                counter[1] = (int) increment;
            }
            else
            { 
                for (int i = 0; i < 1; i++)
                {
                    print(counter[0] + "   " + counter[1]);
                    if (counter[0] > counter[1])
                    {
                        GameData.inX--;
                    }
                    else if (counter[0] < counter[1])
                    {
                        GameData.inX++;
                    }
                }
                PointerUp();
            }
        }
        else
        {
            GameData.inX = (int) increment;
        }
    }

    public void MoveUD(float increment)
    {
        /*
        if (increment < 100 && increment > -100)
        {
            if (preY < 5)
            {
                counter[preY] = (int)increment;
                preY++;
            }
            else
            {
                int dir = 0;
                for (int i = 0; i < 4; i++)
                {
                    if (counter[i] > counter[i + 1])
                    {
                        dir++;
                    }
                    else if (counter[i] < counter[i + 1])
                    {
                        dir--;
                    }
                }
                GameData.inY = dir;
                preY = 0;

                PointerUp();
            }
        }
        else
        {
            GameData.inY = (int) increment;
        }
        */
    }

    public void moveDir(int dir)
    {
        if (dir > 10 || dir < -10)
        {
            GameData.inX = dir;
        }
        else
        {
            GameData.inY = dir;
        }
    }
    public void PointerUp()
    {
        print("AC");
        for (int i = 0; i < 5; i++)
        {
            counter[i] = 0;
        }

        PosSlider.value = GameData.prevX;
        preX = (int) PosSlider.value;
        GameData.inY = 0;
    }
    
    // Update is called once per frame
    void Update()
    {
      //  print("ABCDWEF " + GameData.IterationCount);
        if (!Decor)
        {
            if (NewCycle || GameData.SpawnerMode == 0 && GameData.IterationCount < 45)
            {
                pivot.position = new Vector3(GameData.prevX, pivot.position.y, pivot.position.z);
                var rand = new System.Random();

                int randInt = rand.Next(6);

                TetrisBlocks[randInt].GetComponent<Movement>().Solidify = false;
                TetrisBlocks[randInt].GetComponent<Rigidbody>().constraints = GameData.defaultcst;
                TetrisBlocks[randInt].GetComponent<Rigidbody>().drag = 15;

                Instantiate(TetrisBlocks[randInt], pivot.position, Quaternion.identity);

                GameData.SpawnerMode = 1;
                NewCycle = false;
            }
            else if (GameData.IterationCount >= 45)
            {
                GameOver.SetActive(true);
            }

            if (AddScore)
            {
                GameData.CurrentUser.AddScore();
                AddScore = false;

                if (GameData.CurrentUser.GetScore() % 5 == 0)
                {
                    GameData.Level++;
                    Physics.gravity = new Vector3(Physics.gravity.x, Physics.gravity.y - 1, Physics.gravity.z);
                }
            }


        }
        else
        {
            if (count < Quantity)
            {
                var rand = new System.Random();

                int randInt = rand.Next(6);

                TetrisBlocks[randInt].GetComponent<Movement>().Solidify = true;
                TetrisBlocks[randInt].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                TetrisBlocks[randInt].GetComponent<Rigidbody>().drag = 0;
                Instantiate(TetrisBlocks[randInt], pivot.position, Quaternion.identity);
                count++;
            }
        }

        if (Input.touchCount == 1) // user is touching the screen with a single touch
        {
            print("touched");
            Touch touch = Input.GetTouch(0); // get the touch
            if (touch.phase == TouchPhase.Began) //check for the first touch
            {
                fp = touch.position;
                lp = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved) // update the last position based on where they moved
            {
                lp = touch.position;

                //Check if drag distance is greater than 20% of the screen height
                if (Mathf.Abs(lp.x - fp.x) > dragDistance || Mathf.Abs(lp.y - fp.y) > dragDistance)
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
                    Debug.Log("Tap");
                    GameData.inY = 1;
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
