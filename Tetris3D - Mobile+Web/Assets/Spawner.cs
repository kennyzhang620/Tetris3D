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
    public GameObject GameOver;
    Transform pivot;

	// Use this for initialization
	void Start () {
        pivot = GetComponent<Transform>();
        GameData.prevX = pivot.position.x;
        GameData.defaultCoords = pivot.position;
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
    
    // Update is called once per frame
    void Update()
    {
        print("ABCDWEF " + GameData.SpawnerMode);
        if (!Decor)
        {
            if ((NewCycle || GameData.SpawnerMode == 0) && GameData.IterationCount < 45)
            {
                GameData.ScanLines = true;
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

    }
}
