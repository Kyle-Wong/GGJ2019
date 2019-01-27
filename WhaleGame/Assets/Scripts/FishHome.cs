using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FishTypes { Nemo, Dory, Sun, Hairtail };

public class FishHome : MonoBehaviour
{

    public float lifetime = 30f;

    [SerializeField] public FishTypes myType;

    MeshRenderer meshRender;
    private Material[] mats;
    private Material n_mat;
    private AudioSource Source;
    // Start is called before the first frame update
    void Start()
    {
        Source = GetComponent<AudioSource>();
        meshRender = this.GetComponent<MeshRenderer>();

        mats = meshRender.materials;

        Destroy(this.gameObject, lifetime);
        
        myType = (FishTypes)Random.Range(0, 4);
        /*
        switch(myType)
        {
            case FishTypes.Dory:
                n_mat = mats[0];
                break;

            case FishTypes.Sun:
                n_mat = mats[1];
                break;

            case FishTypes.Nemo:
                n_mat = mats[2];
                break;

            case FishTypes.Hairtail:
                n_mat = mats[3];
                break;
        }
        meshRender.material = n_mat;
        mesh*/

    }

    // Update is called once per frame
    void Update()
    {

    }

    /*
    private void ChangeMaterials(string colorName)
    {
        for(int i = 0; i < mats.Length; i++)
        {
            if (mats[i].name == colorName)
            {
                Material temp = mats[0];
                meshRender.material = mats[i];
                mats[i] = temp;
            }
        }
    }*/

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag + " Collided");
        if (other.CompareTag("Fish"))
        {

            if (other.GetComponent<FishType>().myType == myType)
            {
                Source.PlayOneShot(other.GetComponent<FishSounds>().FishGetsHomeSound);
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
