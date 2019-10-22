using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event  
{

    private float timeSinceStart;

    //fire exting
    //electrical tooollls
    //plunger
    //hammer?
    private string toolNeeded;


    private int severity;
    
    public Event()
    {
        toolNeeded = "Hammer";
        severity = 0;
    }

    public Event(string tool)
    {
        toolNeeded = tool;
    }

    public Event(string tool, int sev)
    {
        toolNeeded = tool;
        severity = sev;
    }

    private void Update()
    {
        timeSinceStart += Time.deltaTime;
    }

    public float getTime()
    {
        return timeSinceStart;
    }

    public string getTool()
    {
        return toolNeeded;
    }

    public int getSeverity()
    {
        return severity;
    }

    public void sing()
    {
        
    }
}
