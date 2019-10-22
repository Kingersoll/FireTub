using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTransport : MonoBehaviour
{
    

    [SerializeField] Transform topOfShaft;
    [SerializeField] Transform bottomOfShaft;
    [SerializeField] float speed;
    private bool atBottom = true;
    private bool liftEnabled = false;

    private void Start()
    {
        
        speed = 5;
    }

    private void FixedUpdate()
    {

        if (liftEnabled)
        {
            if (atBottom == true)
            {
                gameObject.GetComponent<Rigidbody2D>().velocity = (Vector2.up * Time.deltaTime * 30);
                if (transform.position.y >= topOfShaft.position.y)
                {
                    gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                    atBottom = false;
                    liftEnabled = false;
                }
            }
            else
            {
                gameObject.GetComponent<Rigidbody2D>().velocity = (Vector2.down * Time.deltaTime * 30);
                if (transform.position.y <= bottomOfShaft.position.y)
                {
                    gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

                    atBottom = true;
                    liftEnabled = false;
                }
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            liftEnabled = true;
        }



    }


    // this coroutine doesnt do anything dont use it its trash
    IEnumerator Move(bool bottom)
    {
        //broken
        if (atBottom)
        {
            int num=0;
            while (atBottom && num<1000)
            {
                gameObject.GetComponent<Rigidbody2D>().velocity = (Vector2.up * Time.deltaTime * 0.001f);
                //gameObject.transform.Translate(Vector2.up*Time.deltaTime*0.001f);
                if (gameObject.transform.position.y >= topOfShaft.position.y)
                {
                    atBottom = false;
                }
                num++;
            }
        }
        yield return new WaitForSeconds(1);
    }





}
