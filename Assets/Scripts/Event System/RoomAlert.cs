using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomAlert : MonoBehaviour
{
    [SerializeField] GameObject fire;
    [SerializeField] GameObject water;
    [SerializeField] GameObject electric;

    private bool displayF;
    private bool displayW;
    private bool displayE;

    [SerializeField] GameObject Room;

    private RoomInfo roomInf;

    public int numEventObjects;

    public int numOngoingEvents=0;

    public EventObject[] roomObjects;

    public List<Event> events = new List<Event>();

    public GameObject doorNumber;

    private Doorway myDoor;


    private Event[] Events;
    

    void Start()
    {
        roomInf = Room.GetComponent<RoomInfo>();

        //numEventObjects = roomInf.howManyObjects();

        fire.SetActive(false);
        water.SetActive(false);
        electric.SetActive(false);

        myDoor = doorNumber.GetComponent<Doorway>();


        //print(myDoor.onFire);
                                                                                                    
        
    }

   

   
    void Update()
    {
        //important for the arcade machine to close the program
        if (Input.GetButtonUp("Cancel"))
        {
            Application.Quit();
        }



        checkEvents();
        if (displayF == true)
            fire.SetActive(true);
        else
            fire.SetActive(false);
        if (displayW==true)
            water.SetActive(true);
        else
            water.SetActive(false);
        if (displayE==true)
            electric.SetActive(true);
        else
            electric.SetActive(false);
    }

    public void checkEvents()
    {
        bool foundF= false;
        bool foundW= false;
        bool foundE= false;


        foreach (Event E in events)
        {
            if (E.getTool().Equals("Fire"))
                foundF = true;
            else if (E.getTool().Equals("Water"))
                foundW = true;
            else if (E.getTool().Equals("Electric"))
                foundE = true;
        }

        if (foundF == true)
        {
            displayF = true;
            myDoor.onFire = true; //Tony's hijacking in this piece of code. When the Fire Alert is visible (hence, the room is flamin'), then tell the Doorway itself it's on fire.
        }
        else
        {
            displayF = false;
            myDoor.onFire = false;
        }

        if (foundW == true)
            displayW = true;
        else
            displayW = false;

        if (foundE == true)
            displayE = true;
        else
            displayE = false;

        //change to set bools and displays at the end

    }

    public void addEvent(Event evn)
    {
        numOngoingEvents += 1;

        
            // if (Events[i] == null)
            //    Events[i] = evn;
            events.Add(evn);
        
    }

    //useless
    public void removeEvent(Event E)
    {
        events.Remove(E);

        numOngoingEvents -= 1;
    }

    public void tookEvents()
    {
        events.Clear();
    }

    public List<Event> getEvents()
    {
        return events;
    }

    public bool hasRoom()
    {
        print(numOngoingEvents);
        print(numEventObjects);
        if (numOngoingEvents < numEventObjects)
        {
            return true;
        }
        return false;
    }

    public void setNumObjects(int num)
    {
        numEventObjects = num;
    }

    public void test()
    {
        print("test");
    }
}
