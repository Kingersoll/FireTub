using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    bool faceRight = false;
    bool walking = false;
    bool inRoot = true;
    bool inRoom = false;
    bool flaming = false;

    public GameObject flames;
    private Doorway myDoor;

    private GameObject myFlames;

    private Component enteringDoor;

    private int directionValue;

    Rigidbody2D rb;
    SpriteRenderer sr;
    AudioSource Source;

    Vector3 Flipper;

    void Start()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
        rb = gameObject.GetComponent<Rigidbody2D>();

        Source = gameObject.GetComponent<AudioSource>();

        StartCoroutine(direction());
    }

    void Update()
    {
        //Creates Vector to have the object move left and right.
        Vector2 movement = new Vector2(directionValue * 1, gameObject.GetComponent<Rigidbody2D>().velocity.y);

        //Check to see if he's in a room. If he is, then his movement speed should be 0.
        if (inRoom)
        {
            movement = Vector2.zero;
        }

        if (flaming)
        {
            movement = new Vector2(directionValue * 2, gameObject.GetComponent<Rigidbody2D>().velocity.y);
        }
        
        //Applies the vector.
        gameObject.GetComponent<Rigidbody2D>().velocity = movement;

        //If the NPC is carried, within a room, or not otherwise within the root of the scene, he'll be rendered at a higher layer in order to see them within rooms.
        if (transform.root != transform)
        {
            sr.sortingOrder = 99;
            inRoot = false;
        }
        else
        {
            sr.sortingOrder = 1;
            inRoot = true;
        }

        //If the NPC is moving in a certain direction, he'll be flipped to face it.
        if (rb.velocity.x >= 0)
        {
            sr.flipX = true;
        }
        else
        {
            sr.flipX = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        myDoor = collision.gameObject.GetComponent<Doorway>();

        if (collision.gameObject.CompareTag("Door") && inRoot && !flaming) //When the NPC hits a door and isn't on fire
        {
            if ((Random.Range(0, 5)) == 0) //On a 1/4 chance, they'll enter the room.
            {
                if (myDoor.onFire == true)
                {
                    inRoom = true;
                    sr.enabled = false;
                    StartCoroutine(flamingDoorTimer());
                }
                else
                {
                    inRoom = true;
                    sr.enabled = false;
                    StartCoroutine(timer());
                }
            }
        }
    }

    //Randomly generates a direction to walk in.
    IEnumerator direction()
    {
        while (true)
        {
            if (!flaming)
            {
                yield return new WaitForSeconds(2);
            }

            directionValue = Random.Range(-1, 2);

            yield return new WaitForSeconds(.5f);
        }
    }

    IEnumerator timer()
    {
        transform.gameObject.tag = "Untagged";
        yield return new WaitForSeconds(Random.Range(5, 15));
        inRoom = false;
        sr.enabled = true;
        transform.gameObject.tag = "Item";
    }

    IEnumerator flamingDoorTimer()
    {
        transform.gameObject.tag = "Untagged";
        yield return new WaitForSeconds(Random.Range(1, 3));
        transform.gameObject.tag = "Item";
        inRoom = false;
        sr.enabled = true;
        myFlames = Instantiate(flames);
        myFlames.transform.SetParent(gameObject.transform, false);
        flaming = true;
    }


    public bool isOnFire(){
        return flaming;
    }

    public void setOnfire()
    {
        flaming = true;
    }

    public void putOutFire()
    {
        flaming = false;
        GameObject.Destroy(myFlames);
    }
}
