using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaFloor : MonoBehaviour
{
    public int trashLevel = 0;

    OceanManager oceanManager;
    private float width;

    // Start is called before the first frame update
    void Start()
    {
        oceanManager = GetComponentInParent<OceanManager>();
        width = oceanManager.width;
        ChangeFloorWidth();
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

    void ChangeFloorWidth()
    {
        transform.localScale += new Vector3(width, 0, width);
    }

}
