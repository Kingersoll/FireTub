using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EventManager : MonoBehaviour
{
    private GameObject[] problems;
    private GameObject[] AlertObjects;
    private GameObject[] Npcs;
    public static Event tempEvent;
    bool start = true;

    public float numIssues;

    public float score = 0;

    [SerializeField] Text IssueText;
    [SerializeField] Text scoreText;

    [SerializeField] AnimatorControllerParameter yspeed;
    // public List<GameObject> alertObjects= new List<GameObject>();



    // Start is called before the first frame update
    void Start()
    {

        AlertObjects = GameObject.FindGameObjectsWithTag("RoomAlert");

        //this wil also locate the three tool items and all of the npcs 
        findNpcs();

        setScoreText();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        
        if (start) { 
            StartCoroutine(problem());
            start = false;
        }

        numIssues = findNumIssues();
        setIssueText();
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
            
            yield return new WaitForSeconds(9);
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

    public int findNumIssues()
    {
        int nIssues = 0;
        //issues in rooms
        for (int i = 0; i< AlertObjects.Length; i++)
        {
            nIssues += AlertObjects[i].GetComponent<RoomAlert>().numOngoingEvents;
        }
        //issues attached to people
        for(int i=0; i < Npcs.Length; i++)
        {
            //walking npcs
            if (Npcs[i].GetComponent<NPCController>() != null)
            {
                //if theyre on fire
                if (Npcs[i].GetComponent<NPCController>().isOnFire())
                {
                    nIssues++;
                }
            }
            else if (Npcs[i].GetComponent<NPCNewGuy>() != null)
            {
                nIssues++;
            }


        }


        return nIssues;
    }


    public void addScore(float S)
    {
        score += S;
        setScoreText();
    }

    public void setScoreText()
    {
        scoreText.text = "Score: " + score.ToString();
    }

    public void setIssueText()
    {
        IssueText.text = "issues: " + numIssues.ToString();
    }

    public void findNpcs()
    {
        Npcs = GameObject.FindGameObjectsWithTag("Item");
    }
}
