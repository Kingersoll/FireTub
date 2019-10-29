using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCNewGuy : MonoBehaviour
{
    public GameObject RoomRequest2nd1;
    public GameObject RoomRequest2nd2;
    public GameObject RoomRequest2nd3;
    public GameObject RoomRequest2nd4;

    public GameObject RoomRequest1st1;
    public GameObject RoomRequest1st2;
    public GameObject RoomRequest1st3;


    private GameObject myRoomRequest;

    private int whichFloor;
    private int whichRoom;

    private int heldBy;

    [SerializeField] float spawnPower;

    public bool inRoot = true;

    // Start is called before the first frame update
    void Start()
    {


        whichFloor = Random.Range(1, 3); //Generate a 1 or 2.


        if (whichFloor == 2) //If a 2:
        {
            whichRoom = Random.Range(1, 5); //Generate a random room, between 1 and 4.
        }
        else
        {
            whichRoom = Random.Range(1, 4); //Else Generate a random room between 1 and 3.
        }




        // Checking for second floor rooms:
        if (whichFloor == 2 && whichRoom == 1)
        {
            myRoomRequest = Instantiate(RoomRequest2nd1);
            print("Instantiated 21");
        }

        if (whichFloor == 2 && whichRoom == 2)
        {
            myRoomRequest = Instantiate(RoomRequest2nd2);
            print("Instantiated 22");
        }

        if (whichFloor == 2 && whichRoom == 3)
        {
            myRoomRequest = Instantiate(RoomRequest2nd3);
            print("Instantiated 23");
        }

        if (whichFloor == 2 && whichRoom == 4)
        {
            myRoomRequest = Instantiate(RoomRequest2nd4);
            print("Instantiated 24");
        }







        // Checking for first floor rooms:
        if (whichFloor == 1 && whichRoom == 1)
        {
            myRoomRequest = Instantiate(RoomRequest1st1);
            print("Instantiated 1");
        }

        if (whichFloor == 1 && whichRoom == 2)
        {
            myRoomRequest = Instantiate(RoomRequest1st2);
            print("Instantiated 2");
        }

        if (whichFloor == 1 && whichRoom == 3)
        {
            myRoomRequest = Instantiate(RoomRequest1st3);
            print("Instantiated 3");
        }




            myRoomRequest.transform.SetParent(gameObject.transform, false);

        GetComponent<Rigidbody2D>().AddForce(Vector2.left* spawnPower, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Doorway1")
        {
            if (whichFloor == 1 && whichRoom == 1)
            {
                DestroyMe();
            }
        }

        if (collision.gameObject.name == "Doorway2")
        {
            if (whichFloor == 1 && whichRoom == 2)
            {
                DestroyMe();
            }
        }

        if (collision.gameObject.name == "Doorway3")
        {
            if (whichFloor == 1 && whichRoom == 3)
            {
                DestroyMe();
            }
        }





        if (collision.gameObject.name == "Doorway21")
        {
            if (whichFloor == 2 && whichRoom == 1)
            {
                DestroyMe();
            }
        }

        if (collision.gameObject.name == "Doorway22")
        {
            if (whichFloor == 2 && whichRoom == 2)
            {
                DestroyMe();
            }
        }

        if (collision.gameObject.name == "Doorway23")
        {
            if (whichFloor == 2 && whichRoom == 3)
            {
                DestroyMe();
            }
        }

        if (collision.gameObject.name == "Doorway24")
        {
            if (whichFloor == 2 && whichRoom == 4)
            {
                DestroyMe();
            }
        }





    }

    void DestroyMe()
    {
        print("DestroyMe called.");

        if (heldBy == 1)
        {
            transform.parent.gameObject.GetComponent<PlayerController1>().itemReset();
        }

        if(heldBy == 2)
        {
            transform.parent.gameObject.GetComponent<PlayerController2>().itemReset();
        }

        if(heldBy == 0)
        {

        }

        Destroy(gameObject);
    }


    private void Update()
    {
        if (transform.root != transform)
        {
            inRoot = false;
        }

        if (transform.root == transform)
        {
            inRoot = true;
        }

        if (!inRoot)
        {
            var parentName = transform.parent.name;

            if (parentName == "player1")
            {
                heldBy = 1;
            }
            else if (parentName == "player2")
            {
                heldBy = 2;
            }     
        }

        if (inRoot)
        {
            heldBy = 0;
        }
    }
}
