using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomInfo : MonoBehaviour
{

    public int numEventObjects=0;
    public int numOngoingEvents;

    public EventObject[] roomObjects;

    public List<EventObject> nonBroken = new List<EventObject>();

    [SerializeField] GameObject Alerts;

    public List<Event> issues = new List<Event>();

    

    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {

            if (transform.GetChild(i).CompareTag("EventObject"))
            {
                //if the objects are not broken then grab them and look at them yay

                if(transform.GetChild(i).gameObject.GetComponent<EventObject>().broken == false)
                    nonBroken.Add(transform.GetChild(i).gameObject.GetComponent<EventObject>());

               // roomObjects[numEventObjects] = transform.GetChild(i).gameObject.GetComponent<EventObject>();

                numEventObjects++;
            }
        }

        Alerts.GetComponent<RoomAlert>().setNumObjects(numEventObjects);

        gameObject.SetActive(false);
    }

    
    private void OnEnable()
    {
        getRoomContents();

        issues = Alerts.GetComponent<RoomAlert>().getEvents();

        foreach(Event E in issues)
        {
            int ran = Random.Range(0, nonBroken.Count);
            // if the Game Object isnt broken apply the event
            if (nonBroken[ran].broken == false)
            {
                nonBroken[ran].breakObject(E);
            }
            else
            {
                
            }
            getRoomContents();
        }

        // this is an offender of the room alerts going bye bye
       
            
    }

    

    public void getRoomContents()
    {
        for (int i = 0; i < transform.childCount; i++)
        {

            if (transform.GetChild(i).CompareTag("EventObject"))
            {
                //if the objects are not broken then grab them and look at them yay

                if (transform.GetChild(i).gameObject.GetComponent<EventObject>().broken == false)
                    nonBroken.Add(transform.GetChild(i).gameObject.GetComponent<EventObject>());

                // roomObjects[numEventObjects] = transform.GetChild(i).gameObject.GetComponent<EventObject>();

                
            }
        }
    }

    //eeeek
    public bool tryAnotherObject(List<Event> E)
    {
        // if every Object in the room has an event
        if (!roomAcceptEvent())
        {
            return false;
        }

        bool condition = false;

        while (condition)
        {

        }

        return true;

    }


    // can this room accept another event
    public bool roomAcceptEvent()
    {
        if (numEventObjects == numOngoingEvents)
        {
            return false;
        }

        else return true;
    }

    public int howManyObjects()
    {
        return numEventObjects;
    }

    public GameObject getAlert()
    {
        return Alerts;
    }

    public void test2()
    {
        print("test2");
    }

}
         