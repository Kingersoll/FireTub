using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    private static GameObject firstFloorActive;
    private static GameObject secondFloorActive;

    [SerializeField] private GameObject firstFloorHallwayDoors;
    [SerializeField] private GameObject secondFloorHallwayDoors;


    public void updateFirstFloor(GameObject floor)
    {
        firstFloorActive = floor;

        if (firstFloorActive != null)
        {
            firstFloorHallwayDoors.SetActive(false);
        }
        else
        {
            firstFloorHallwayDoors.SetActive(true);
        }
    }

    public void updateSecondFloor(GameObject floor)
    {
        secondFloorActive = floor;

        if (secondFloorActive != null)
        {
            secondFloorHallwayDoors.SetActive(false);
        }
        else
        {
           secondFloorHallwayDoors.SetActive(true);
        }
    }

    public GameObject getFirstFloor()
    {
        return firstFloorActive;
    }

    public GameObject getSecondFloor()
    {
        return secondFloorActive;
    }
    
    public void dropItem(int floor, GameObject item)
    {
        if(floor == 1)
        {
            if (firstFloorActive != null)
                item.transform.SetParent(firstFloorActive.transform);
            else
                item.transform.SetParent(null);
        }
        else
        {
            if (secondFloorActive != null)
                item.transform.SetParent(secondFloorActive.transform);
            else
                item.transform.SetParent(null);
        }
    }
}
