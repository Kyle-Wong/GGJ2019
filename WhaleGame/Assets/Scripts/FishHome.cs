using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FishTypes { Nemo, Dory, Sun};

public class FishHome : MonoBehaviour
{

    public float lifetime = 30f;

    [SerializeField] private FishTypes myType;

    MeshRenderer meshRender;
    private Material[] mats;

    // Start is called before the first frame update
    void Start()
    {
        meshRender = this.GetComponent<MeshRenderer>();

        mats = meshRender.materials;
        Debug.Log(mats[0]+" "+ mats[1] +" " + mats[2]);
        Destroy(this.gameObject, lifetime);

        myType = (FishTypes)Random.Range(0, 3);

        if(myType == FishTypes.Dory)
        {
            meshRender.material = mats[0];
        }

        if(myType == FishTypes.Nemo)
        {
            meshRender.material = mats[1];
        }

        if(myType == FishTypes.Sun)
        {
            meshRender.material = mats[2];
        }




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
