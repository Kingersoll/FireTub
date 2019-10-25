using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doorway : MonoBehaviour
{
    [SerializeField] bool secondFloor;
    [SerializeField] bool hallwayDoor;
    [SerializeField] GameObject toRoom;
    [SerializeField] Transform spawnLoc;
    private RoomManager RoomMan;

    public bool onFire = false; //This has been added to help Tony make the NPCs burst into flames.
    // Start is called before the first frame update

    void Start()
    {
        RoomMan = GameObject.FindGameObjectWithTag("RoomManager").GetComponent<RoomManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void enterDoor(GameObject player)
    {
        if (hallwayDoor) {

            
            enableRoom();
            //update the roomManager so it player one is on the second floors
            if (secondFloor)
            {
                RoomMan.updateSecondFloor(toRoom);
     
                player.transform.position = toRoom.transform.position;
            }
            else
            {
                RoomMan.updateFirstFloor(toRoom);

                player.transform.position = toRoom.transform.position;
            }

        }
        //disable active room send player home
        else
        {
            if (secondFloor)
            {
                gameObject.transform.parent.gameObject.SetActive(false);
                RoomMan.updateSecondFloor(null);
            }
            else
            {
                gameObject.transform.parent.gameObject.SetActive(false);
                RoomMan.updateFirstFloor(null);

            }
            
        }

        player.transform.position = spawnLoc.transform.position;
    }

    public void enableRoom()
    {
        toRoom.SetActive(true);
    }
}