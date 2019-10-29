using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController1 : MonoBehaviour
{

    private float moveInput;
    private bool facingRight;


    private bool holdingItem;
    private GameObject itemHeld;


    public GameObject ItemUnderPlayer;

    private GameObject EventableObject;

    [SerializeField] float throwPower;
    [SerializeField] float moveSpeed;
    [SerializeField] Transform holdLocation;
    [SerializeField] Transform rayRight;
    [SerializeField] Transform rayLeft;

    Animator animator;


    private RoomManager roomMan;
    
    private GameObject door;

    SpriteRenderer sr;

    void Start()
    {
        
        sr = gameObject.GetComponent<SpriteRenderer>();
    
        holdingItem = false;
        facingRight = true;

        animator = GetComponent<Animator>();

        roomMan = GameObject.FindGameObjectWithTag("RoomManager").GetComponent<RoomManager>();

        //player and items ignore physics
        Physics2D.IgnoreLayerCollision(9, 11, true);

        Physics2D.IgnoreLayerCollision(9, 8);
    }

    /*
     * 
     * RECODE INPUTS TO WORK NO BASED ON THE INPUT MANAGER I DONT THINK ITLL WORK WELL WITH THE ARCADE MACHINE
     */



    private void Update()
    {
        //test deeleete later just first line
        
        if (door != null)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                door.GetComponent<Doorway>().enterDoor(gameObject);
                door = null;
            }
        }
        

        //logic for picking the Item up if an item exists behind the player and one is not being held 
        if (holdingItem == false)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                GameObject temp = checkForitem();
                if(temp!= null)
                {

                    itemHeld = temp ;
                    holdingItem = true;
                    itemHeld.transform.SetParent(gameObject.transform);
                   // EventableObject = temp;
                }
            }

        }



        //placing an item and throwing items
        if(holdingItem == true)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                

                holdingItem = false;

                itemHeld.transform.position = new Vector3(itemHeld.transform.position.x,itemHeld.transform.position.y);

                itemHeld = null;
                
            }

            if (Input.GetKeyDown(KeyCode.B))
            {
                throwItem();
            }
        }

        





        //keeps the item above the player stuck to the transform of the hold location that is a child of the player
        if (holdingItem)
        {
            itemHeld.transform.position = holdLocation.position;
        }


        //logic for fixing events on objects, might kind of suck
        if(holdingItem)
        {
            
            if (Input.GetKeyDown(KeyCode.V))
            {
                
                if (EventableObject != null)
                {

                    
                    if (EventableObject.GetComponent<EventObject>().isBroken())
                    {
                       
                        if (itemHeld.GetComponent<Item>().getName().Equals(EventableObject.GetComponent<EventObject>().getToolNeeded()))
                        {
                            //when An object is fixed first remove its reference in the event system, tell the oobject its fixed, add score
                            roomMan.getSecondFloor().GetComponent<RoomInfo>().getAlert().GetComponent<RoomAlert>().removeEvent(EventableObject.GetComponent<EventObject>().onGoingEvent);
                            EventableObject.GetComponent<EventObject>().Fix();
                           
                        }

                       

                    }
                   
                }
                if (itemHeld.GetComponent<Item>() != null)
                {
                    if (itemHeld.GetComponent<Item>().getName().Equals("Fire"))
                    {
                        print("called");
                        fireEx();
                    }
                }
            }
            
        }



    }




    void FixedUpdate()
    {
        moveInput = Input.GetAxis("Horizontal");
        
       
        Vector2 movement = new Vector2(moveInput * moveSpeed, gameObject.GetComponent<Rigidbody2D>().velocity.y);

        gameObject.GetComponent<Rigidbody2D>().velocity = movement;

        //transform.position += movement * Time.deltaTime * moveSpeed;

        if (facingRight == false && moveInput > 0)
        {
            flip();
            sr.flipX = true;
        }

        else if (facingRight == true && moveInput < 0)
        {
            flip();
            sr.flipX = false;
        }


        float xSpeed = GetComponent<Rigidbody2D>().velocity.x;

        if (Mathf.Abs(xSpeed) > 0)
        {
            animator.SetFloat("XSpeed", 1);
        }
        else
            animator.SetFloat("XSpeed", -1);

    }



    void flip()
    {
        facingRight = !facingRight;

        //Below is what was previously used to flip the player. This has been changed to the sr.flipX code seen above.
        //Vector3 Scaler = transform.localScale;
        //Scaler.x *= -1;
        //transform.localScale = Scaler;
    }


    //check for the item infront of the player called when pickup is checked
    public GameObject checkForitem()
    {
        RaycastHit2D hit;
        if (facingRight) {
           
            hit = Physics2D.Raycast(rayRight.position, Vector2.right, .3f);
            print(hit.distance);
        }
        else
        {
           
            hit = Physics2D.Raycast(rayLeft.position, Vector2.left, .3f);
            print(hit.distance);
        }

        if (hit.collider != null)
        {
            if (hit.collider.gameObject.CompareTag("Item"))
            {
                print("found an Item");
                return hit.collider.gameObject;
            }
            else return null;
        }
        else
            return null;
    }

    public void throwItem()
    {
        //player 1 is on the second floor
        roomMan.dropItem(2, itemHeld);

        holdingItem = false;
        itemHeld.GetComponent<Rigidbody2D>().velocity =  Vector3.zero;
        if (facingRight)
            itemHeld.GetComponent<Rigidbody2D>().AddForce(Vector2.right * throwPower,ForceMode2D.Impulse);
        else
            itemHeld.GetComponent<Rigidbody2D>().AddForce(Vector2.left * throwPower, ForceMode2D.Impulse);



        //itemHeld.GetComponent<Rigidbody2D>().AddForce(Vector2.up * throwPower, ForceMode2D.Impulse);
        itemHeld = null;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("EventObject"))
        {
            EventableObject = col.gameObject;
        }
        else if (col.gameObject.CompareTag("Door"))
        {
            door = col.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Door"))
        {
            door = null;
        }
    }


    public void itemReset()
    {
        holdingItem = false;
        itemHeld = null;
    }

   private void fireEx()
    {

        RaycastHit2D[] hits;
        if (facingRight)
        {

            hits = Physics2D.RaycastAll(rayRight.position, Vector2.right, .6f);
           
        }
        else
        {

            hits = Physics2D.RaycastAll(rayLeft.position, Vector2.left, .6f);
            
        }

        foreach( RaycastHit2D H in hits)
        {
            print("runs");
            //14th layer is the layer of Npcs so this detects if its an Npc
            if (H.collider.gameObject.layer == 14)
            {
                print("la");
                NPCController temp = H.collider.gameObject.GetComponent<NPCController>();
                if (temp.isOnFire())
                {
                    //put the fire out
                    temp.putOutFire();
                    print("yeet");
                }

            }
        }

    }

}
