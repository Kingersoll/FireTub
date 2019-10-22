using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    private GameObject[] problems;
    private GameObject[] AlertObjects;
    public static Event tempEvent;
   // public List<GameObject> alertObjects= new List<GameObject>();



    // Start is called before the first frame update
    void Start()
    {
        AlertObjects = GameObject.FindGameObjectsWithTag("RoomAlert");
        StartCoroutine(problem());
    }

    // Update is called once per frame
    void Update()
    {
        
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
            print("Hi");
            tempEvent.sing();
            print("Ho");
            assignEvent(tempEvent);
            
            yield return new WaitForSeconds(8);
        }
    }

    public void assignEvent(Event E)
    {
        int assignLoc = Random.Range(0, AlertObjects.Length - 1);
        AlertObjects[assignLoc].GetComponent<RoomAlert>().addEvent(E);


    }

}
