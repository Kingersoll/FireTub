using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    //information for the type of Item that it is 
    [SerializeField] string toolName;


    public string getName()
    {
        return toolName;
    }

    public void teleport(Transform trans)
    {
        gameObject.GetComponent<Transform>().position = trans.GetComponent<Transform>().position;
    }

}
