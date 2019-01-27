using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishHome : MonoBehaviour
{

    public float lifetime = 30f;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, lifetime);   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag + " Collided");
        if (other.CompareTag("Fish"))
        {
            BuildFishTrail.RemoveFish(other.gameObject);
            Destroy(this.gameObject, 3f);
        }

    }
}
