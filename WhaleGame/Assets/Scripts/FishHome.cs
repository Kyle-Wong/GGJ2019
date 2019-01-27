using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FishTypes { Nemo, Dory, Sun};

public class FishHome : MonoBehaviour
{

    public float lifetime = 30f;

    private FishTypes myType;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, lifetime);

        myType = (FishTypes)Random.Range(0, 3);   
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

            if (other.GetComponent<FishType>().myType == myType)
            {
                Debug.Log("Same type");
                BuildFishTrail.RemoveFish(other.gameObject);
                GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().Score++;
                Destroy(this.gameObject, 3f);
            }
        }

    }
}
