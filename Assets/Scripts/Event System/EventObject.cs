using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventObject : MonoBehaviour
{
    public bool broken;


    public string toolNeeded;

    [SerializeField] GameObject insideRoom;

    [SerializeField] Sprite fixedSprite;

    [SerializeField] Sprite brokenSprite;

    [SerializeField] GameObject waterP;
    [SerializeField] GameObject fireP;
    [SerializeField] GameObject electricP;

    public GameObject particles;

    public Event onGoingEvent;

    // Start is called before the first frame update
    void Start()
    {
       

      
    }

    // Update is called once per frame
    void Update()
    {
        if(broken == false)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = fixedSprite;

        }
       


    }

    public bool isBroken()
    {
        return broken;
    }

    public string getToolNeeded()
    {
        return toolNeeded;
    }

    public void Fix()
    {

        broken = false;

        gameObject.GetComponent<SpriteRenderer>().sprite = fixedSprite;

        GameObject.Destroy(particles);

        toolNeeded = null;

        //call something that terminates the event and does score things// need to figure out timer system cuz timer wont update while disabled...
        onGoingEvent = null;
    }
    
    //make take an event passed in
    public void breakObject(Event E)
    {
        
        onGoingEvent = E;

        print(E.getTool());
        toolNeeded = E.getTool();

        broken = true;

        //assign the broken version of the sprite
        gameObject.GetComponent<SpriteRenderer>().sprite = brokenSprite;
        

        //assign rotation to the particle effect so it comes in correctly
        Quaternion rot = new Quaternion(0, 0, 0, 0);
        rot.eulerAngles = new Vector3(-90, 0, 0);


        //create particle effect
        if(toolNeeded.Equals("Fire"))
            particles = GameObject.Instantiate(fireP, transform.position, rot, insideRoom.transform);
        else if(toolNeeded.Equals("Water"))
            particles = GameObject.Instantiate(waterP, transform.position, rot, insideRoom.transform);
        else if (toolNeeded.Equals("Electric"))
            particles = GameObject.Instantiate(electricP, transform.position, rot,insideRoom.transform);


    }

}
