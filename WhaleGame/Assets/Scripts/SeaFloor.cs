using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaFloor : MonoBehaviour
{
    public int trashLevel = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
       //Debug.Log(collision.collider.tag);
        if(collision.collider.CompareTag("Trash"))
        {
            trashLevel++;
        }
    }
}
