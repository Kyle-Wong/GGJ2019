using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FishTypes { Nemo, Dory, Sun, Hairtail};

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

        Destroy(this.gameObject, lifetime);

        myType = (FishTypes)Random.Range(0, 4);

        if(myType == FishTypes.Dory)
        {
            meshRender.material = mats[0];
            Debug.Log("Dory home");
            foreach (Material m in mats)
            {
                Debug.Log(m.name);
            }
        }

        if(myType == FishTypes.Nemo)
        {

            meshRender.material = mats[1];
            Debug.Log("Nemo home");
            foreach (Material m in mats)
            {
                Debug.Log(m.name);
            }
        }

        if(myType == FishTypes.Sun)
        {
            meshRender.material = mats[2];
            Debug.Log("Sun home");
            foreach (Material m in mats)
            {
                Debug.Log(m.name);
            }
        }

        if (myType == FishTypes.Hairtail)
        {
            meshRender.material = mats[3];
            Debug.Log("Sun home");
            foreach (Material m in mats)
            {
                Debug.Log(m.name);
            }
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

    private void OnDestroy()
    {
        OceanManager.RemoveFishHome(this.gameObject);   
    }
}
