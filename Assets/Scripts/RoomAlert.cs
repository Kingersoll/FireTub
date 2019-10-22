﻿using System.Collections;
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


    public int numEventObjects;

    public int numOngoingEvents;

    public EventObject[] roomObjects;
    public List<Event> events = new List<Event>();


    private Event[] Events;
    

    void Start()
    {
        fire.SetActive(false);
        water.SetActive(false);
        electric.SetActive(false);
    }

   
    void Update()
    {
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
            displayF = true;
        else
            displayF = false;

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
    public void removeEvent(int index)
    {
        Events[index] = null;
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

}
