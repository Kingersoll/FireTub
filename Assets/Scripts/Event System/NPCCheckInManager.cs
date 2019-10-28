using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCCheckInManager : MonoBehaviour
{
    private GameObject newGuy;

    public GameObject ProblemNPC;

    private EventManager eventMan;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(timer());
        eventMan = GameObject.FindGameObjectWithTag( "EventManager").GetComponent<EventManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    IEnumerator timer()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(15, 20));
            newGuy = Instantiate(ProblemNPC);
            eventMan.findNpcs();
        }
    }

}
