using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameSettings;

public class Movement : MonoBehaviour
{

    Transform curr = null;
    Rigidbody currRb = null;
    float defaultDrag = 0;

    int Rotation = 0;

    public GameObject[] Spawner;
    public int SizeofSpawner = 0;
    public bool DisableInput;

    int active = 0;
    public bool collision = false;
    public bool contact = false;
    public bool Solidify = false;
    public Vector3 prevVtr = new Vector3(0, 0, 0);
    Quaternion prevQuat;
    int prevRot = 0;
    public bool prevCoord = false;

    bool enabled = true;
    float TimeElapsed = 0;

    void OnCollisionStay(Collision obj)
    {
        if ((obj.gameObject.tag == "Base" || obj.gameObject.tag == "GameObj") && Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") <= 0 && GameData.touchX == 0 && !prevCoord)
        {
            collision = true;
        }
        //   print("GameObj: " + obj.gameObject);
        contact = true;
    }

    void OnCollisionExit(Collision obj)
    {
        contact = false;
    }

    // Use this for initialization
    void Start()
    {
        curr = GetComponent<Transform>();
        currRb = GetComponent<Rigidbody>();
        defaultDrag = currRb.drag;
        GameData.inY = 0;
    }

    // Update is called once per frame
    void Update()
    {

        if (!Solidify && Physics.gravity.y != 0)
        {
            /*
            if (GameData.Clearbit && collide == false)
            {
                print("DROP");
                enabled = true;
                //  collision = false;
                contact = false;

                currRb.drag = 0;
                currRb.constraints = GameData.defaultcst;
                collide = true;
            }
            */

            if (currRb.velocity.y > 0)
            {
                currRb.velocity = new Vector3(currRb.velocity.x, 0, currRb.velocity.z);
            }

            if (collision == false)
            {
                if (!DisableInput)
                {
                    if (contact == true)
                    {
                        if (!(prevVtr.x == 0 && prevVtr.y == 0 && prevVtr.z == 0))
                        {
                                curr.position = prevVtr;
                                curr.rotation = prevQuat;
                                Rotation = prevRot;
                        }
                        else
                        {
                            curr.position = GameData.defaultCoords;
                        }
                    }

                    if (TimeElapsed < 0.05f)
                        TimeElapsed += Time.deltaTime;
                    else
                    {
                        TimeElapsed = 0;


                        if (GameData.touchX != 0)
                        {
                            if (!contact)
                            {
                                prevVtr = curr.position;
                                prevQuat = curr.rotation;
                                prevRot = Rotation;

                                curr.position = new Vector3(GameData.touchX, curr.position.y, curr.position.z);

                                if (contact)
                                {
                                    curr.position = prevVtr;
                                    curr.rotation = prevQuat;
                                    Rotation = prevRot;

                                    prevCoord = true;
                                }
                            }
                        }

                        print("ROT: " + Rotation);
                        if (Input.GetAxis("Vertical") != 0 || GameData.inY != 0)
                        {
                            if (GameData.IterationCount > 0)
                                GameData.IterationCount--;

                            if ((Input.GetAxis("Vertical") > 0 || GameData.inY > 0) && active == 0)
                            {
                                if (!contact)
                                {
                                    prevVtr = curr.position;
                                    prevQuat = curr.rotation;
                                    prevRot = Rotation;

                                    curr.Rotate(0.0f, 0.0f, -90.0f, Space.Self);
                                    if (Rotation < 3)
                                        Rotation++;
                                    else
                                        Rotation = 0;

                                    if (contact)
                                    {
                                        curr.position = prevVtr;
                                        curr.rotation = prevQuat;
                                        Rotation = prevRot;

                                        prevCoord = true;
                                    }
                                }
                            }

                            if (Input.GetAxis("Vertical") < 0 || GameData.inY < 0)
                            {
                                currRb.drag = 0;
                                if (Rotation == 0)
                                {
                                    currRb.AddForce(0, -50000, 0);
                                }

                                if (Rotation == 1)
                                {
                                    currRb.AddForce(50000, 0, 0);
                                }
                                if (Rotation == 2)
                                {
                                    currRb.AddForce(0, -50000, 0);
                                }

                                if (Rotation == 3)
                                {
                                    currRb.AddForce(-50000, 0, 0);
                                }
                            }
                            else
                            {
                                currRb.drag = defaultDrag;
                            }

                            active = 1;
                        }
                        else if ((Input.GetAxis("Vertical") <= 0 && GameData.inY <= 0) && active == 1)
                        {
                            print("active1");
                            active = 0;

                            if (Input.GetAxis("Vertical") == 0 || GameData.inY == 0)
                            {
                                currRb.drag = defaultDrag;
                            }
                        }


                        if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") <= 0 && GameData.inX == 0 && GameData.inY <= 0 && GameData.touchX == 0)
                        {
                            if (!contact && prevVtr.x != 0 && prevVtr.y != 0)
                            {
                                prevVtr = curr.position;
                                prevQuat = curr.rotation;
                                prevRot = Rotation;

                                GameData.prevX = curr.position.x;

                                if (GameData.IterationCount > 0)
                                    GameData.IterationCount--;

                                prevCoord = false;
                            }
                        }


                        if (Input.GetAxis("Horizontal") != 0 || GameData.inX != 0)
                        {
                            if (GameData.IterationCount > 0)
                                GameData.IterationCount--;

                            if (Input.GetAxis("Horizontal") > 0 || GameData.inX > 0)
                            {
                                if (!contact)
                                {
                                    prevVtr = curr.position;
                                    prevQuat = curr.rotation;
                                    prevRot = Rotation;

                                    if (Rotation == 0)
                                    {
                                        curr.Translate(1, 0, 0);
                                    }

                                    if (Rotation == 1)
                                    {
                                        curr.Translate(0, 1, 0);
                                    }
                                    if (Rotation == 2)
                                    {
                                        curr.Translate(-1, 0, 0);
                                    }

                                    if (Rotation == 3)
                                    {
                                        curr.Translate(0, -1, 0);
                                    }

                                    if (contact)
                                    {
                                        curr.position = prevVtr;
                                        curr.rotation = prevQuat;
                                        Rotation = prevRot;

                                        prevCoord = true;
                                    }
                                }

                                GameData.inX = 0;
                            }

                            if (Input.GetAxis("Horizontal") < 0 || GameData.inX < 0)
                            {
                                if (!contact)
                                {

                                    prevVtr = curr.position;
                                    prevQuat = curr.rotation;
                                    prevRot = Rotation;

                                    if (Rotation == 0)
                                    {
                                        curr.Translate(-1, 0, 0);
                                    }

                                    if (Rotation == 1)
                                    {
                                        curr.Translate(0, -1, 0);
                                    }
                                    if (Rotation == 2)
                                    {
                                        curr.Translate(1, 0, 0);
                                    }

                                    if (Rotation == 3)
                                    {
                                        curr.Translate(0, 1, 0);
                                    }

                                    if (contact)
                                    {
                                        curr.position = prevVtr;
                                        curr.rotation = prevQuat;
                                        Rotation = prevRot;

                                        prevCoord = true;
                                    }
                                }
                            }
                        }
                        GameData.inX = 0;
                    }
                }

            }

            else
            {
                if (enabled == true && contact == true)
                {
                    enabled = false;
                    //  contact = false;
                    //  collide = false;

                    currRb.drag = 0;
                    currRb.mass = 1000000000;

                    if (SizeofSpawner > 0)
                    {
                        foreach (GameObject obj in Spawner)
                        {
                            obj.AddComponent<Rigidbody>();
                            obj.AddComponent<Movement>();
                            Rigidbody rbobj = obj.GetComponent<Rigidbody>();
                            obj.GetComponent<Movement>().DisableInput = true;
                            rbobj.mass = currRb.mass;
                            rbobj.drag = currRb.drag;
                            rbobj.angularDrag = currRb.angularDrag;

                            if (GameData.PhysicsOn)
                                rbobj.constraints = GameData.defaultcst;
                            else
                                rbobj.constraints = RigidbodyConstraints.FreezeAll;
                            obj.tag = "Base";

                            SizeofSpawner = 0;
                            GameData.IterationCount++;
                        }
                    }
                    if (GameData.PhysicsOn)
                        currRb.constraints = GameData.defaultcst;
                    else
                        currRb.constraints = RigidbodyConstraints.FreezeAll;

                    curr.tag = "Base";
                    if (!DisableInput)
                    {
                        Destroy(currRb);
                        Destroy(GetComponent<Movement>());
                        GameData.SpawnerMode = 2;
                    }
                }
            }

        }
    }
}
