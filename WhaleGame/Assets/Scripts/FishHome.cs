using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FishTypes { Nemo, Dory, Sun, Hairtail };

public class FishHome : MonoBehaviour
{

    public float lifetime = 30f;
    public Mesh[] meshes;
    public Material[] materials;


    [SerializeField] public FishTypes myType;

    MeshRenderer meshRender;
    MeshFilter filter;

    private AudioSource Source;
    // Start is called before the first frame update
    void Start()
    {
        Source = GetComponent<AudioSource>();
        meshRender = this.GetComponent<MeshRenderer>();
        filter = this.GetComponent<MeshFilter>();

        Destroy(this.gameObject, lifetime);

        myType = (FishTypes)Random.Range(0, 4);
        switch (myType)
        {
            case FishTypes.Dory:
                filter.mesh = meshes[0];
                filter.transform.rotation = Quaternion.identity;
                filter.transform.localScale = new Vector3(50f, 40f);
                meshRender.material = materials[0];
                break;

            case FishTypes.Nemo:
                filter.mesh = meshes[1];
                meshRender.material = materials[1];
                break;

            case FishTypes.Sun:
                filter.mesh = meshes[2];
                meshRender.material = materials[2];
                break;

            case FishTypes.Hairtail:
                filter.mesh = meshes[3];
                meshRender.material = materials[3];
                break;
        }
    }

        // Update is called once per frame
        void Update()
    {

    }
    
    void OnTriggerEnter(Collider other)
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

    void OnDestroy()
    {
        OceanManager.RemoveFishHome(this.gameObject);   
    }
}
