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

    // Start is called before the first frame update
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
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        getRoomContents();
        issues = Alerts.GetComponent<RoomAlert>().getEvents();

        foreach(Event E in issues)
        {
            int ran = Random.Range(0, nonBroken.Count);
            nonBroken[ran].breakObject(E);
            
            getRoomContents();
        }
        issues.Clear();
            
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

}
