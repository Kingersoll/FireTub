using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    bool faceRight = false;
    bool walking = false;
    private int directionValue;

    Rigidbody rb;


    void Start()
    {
        StartCoroutine(direction());
    }

    void Update()
    {
        Vector2 movement = new Vector2(directionValue * 1, gameObject.GetComponent<Rigidbody2D>().velocity.y);

        gameObject.GetComponent<Rigidbody2D>().velocity = movement;

        if(directionValue == -1)
        {
            flip();
        }
        else if(directionValue == 1)
        {
            flip();
        }
    }


    void flip()
    {
        Vector3 Flipper = transform.localScale;
        Flipper.x *= -1;
        transform.localScale = Flipper;
    }


    IEnumerator direction()
    {
        while (true)
        {
            yield return new WaitForSeconds(4);
            directionValue = Random.Range(-1, 2);
            if (directionValue == 1)
            {
                faceRight = true;
            }
            print(directionValue);
        }
    }
}
