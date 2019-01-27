using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OceanManager : MonoBehaviour
{
    public float width;
    public float yMax;
    public float yMin;

    //public int fishMax;
    private int fishSpawned = 0;

    public int fishHomeMax;
    private int fishHomeSpawned = 0;

    public float spawnRate;
    public float homeSpawnRate;

    public GameObject fishPrefab;
    public GameObject fishHomePrefab;

    public GameObject[] fishes;

    //private GameObject ground;
    private GameObject boatSpawner;

    private bool fishSpawning = false;
    private bool homeSpawning = false;

    // Start is called before the first frame update
    void Start()
    {
     

        boatSpawner = GameObject.FindGameObjectWithTag("BoatSpawner");
        boatSpawner.transform.position = new Vector3(0, yMax, 0);

        Debug.Log(width);
        boatSpawner.GetComponent<BoatSpawner>().SetWidth(width);
    }

    // Update is called once per frame
    void Update()
    { 
        if (!fishSpawning)
        {
            StartCoroutine("SpawnFish");
            fishSpawning = true;
        }

        if (!homeSpawning)
        {
            StartCoroutine("SpawnFishHome");
            homeSpawning = true;
        }

    }

    IEnumerator SpawnFish()
    {

        float counter = 0;
        while (true)
        {
            counter += Time.deltaTime;
            if (counter >= homeSpawnRate)
            {
                //fishHomeSpawned = GameObject.FindGameObjectsWithTag("Fish").Length;
                //if (fishHomeSpawned < fishHomeMax)
                //{
                    float randx = Random.Range(width / 2.0f * -1, width / 2.0f);
                    float randy = Random.Range(yMin,yMax);
                    int rand = Random.Range(0, fishes.Length);

                    Instantiate(fishes[rand], new Vector3(randx, randy, 0), Quaternion.identity);

                    fishSpawned++;
                    Debug.Log("Fish spawned");
                //}
                counter = 0f;
            }

            yield return null;
        }

    }

    IEnumerator SpawnFishHome ()
    {

        float counter = 0;
        while (true)
        {
            counter += Time.deltaTime;
            if (counter >= homeSpawnRate)
            {
                fishHomeSpawned = GameObject.FindGameObjectsWithTag("FishHome").Length;
                if (fishHomeSpawned < fishHomeMax)
                {
                    float randx = Random.Range(width / 2.0f * -1, width / 2.0f);
                    float randy = Random.Range(yMin,yMax);

                    Instantiate(fishHomePrefab, new Vector3(randx, randy, 0), fishHomePrefab.transform.rotation);

                    fishHomeSpawned++;
                    Debug.Log("Fish home spawned");
                }
                counter = 0f;
            }

            yield return null;
        }

    }


}
