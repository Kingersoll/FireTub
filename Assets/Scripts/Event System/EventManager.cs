using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    private GameObject[] problems;
    private GameObject[] AlertObjects;
    public static Event tempEvent;
    bool start = true;
    // public List<GameObject> alertObjects= new List<GameObject>();



    // Start is called before the first frame update
    void Start()
    {
        AlertObjects = GameObject.FindGameObjectsWithTag("RoomAlert");
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        
        if (start) { 
            StartCoroutine(problem());
            start = false;
        }
    }



    //Generation of completely random events at the moment!!!
    IEnumerator problem()
    {
        
        while (true)
        {
            
            int sev;
            int tool;
            string tPick="";
            sev = Random.Range(0, 2);
            tool = Random.Range(0, 3);

            if (tool == 0)
                tPick = "Water";
            if (tool == 1)
                tPick = "Fire";
            if (tool == 2)
                tPick = "Electric";

            Event tempEvent = new Event(tPick, sev);
         
            tempEvent.sing();
          
            assignEvent(tempEvent);
            
            yield return new WaitForSeconds(8);
        }
    }


    // the recursion in this function is causing issues I believe
    public void assignEvent(Event E)
    {
        int assignLoc = Random.Range(0, AlertObjects.Length );

        if (openRoom())
        {
            if (AlertObjects[assignLoc].GetComponent<RoomAlert>().hasRoom())
                AlertObjects[assignLoc].GetComponent<RoomAlert>().addEvent(E);
            else
            {
                assignEvent(E);
            }
        }
       
                      

    }

    //checks all the rooms to see if there is an empty spot in any of them
    public bool openRoom()
    {
        int roomsFull=0;
        int assignLoc = Random.Range(0, AlertObjects.Length);

        for(int i =0; i < AlertObjects.Length; i++)
        {
            if (AlertObjects[i].GetComponent<RoomAlert>().hasRoom())
            {
                //if has room do nothing
            }
            else
            {
                roomsFull++;
            }

        }
        if (roomsFull == AlertObjects.Length)
        {
            return false;
        }
        else
            return true;
        

    }
}
