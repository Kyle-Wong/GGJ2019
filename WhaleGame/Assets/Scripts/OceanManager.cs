﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OceanManager : MonoBehaviour
{

    public static OceanManager instance;

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

    public static List<GameObject> allFishHomes;

    private bool fishSpawning = false;
    private bool homeSpawning = false;

    void Awake()
    {
        instance = this;
        allFishHomes = new List<GameObject>(); 

    }

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

        //Debug.Log("Fish Home Count: " + allFishHomes.Count);
    }

    IEnumerator SpawnFish()
    {

        float counter = 0;
        while (!GameController.GameOver)
        {
            counter += Time.deltaTime;
            if (counter >= spawnRate)
            {
                //fishHomeSpawned = GameObject.FindGameObjectsWithTag("Fish").Length;
                //if (fishHomeSpawned < fishHomeMax)
                //{
                    float randx = Random.Range((width-10f) / 2.0f * -1, (width-10f) / 2.0f);
                    float randy = Random.Range(yMin+20,yMax-20);
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
        while (!GameController.GameOver)
        {
            counter += Time.deltaTime;
            if (counter >= homeSpawnRate)
            {
                fishHomeSpawned = GameObject.FindGameObjectsWithTag("FishHome").Length;
                if (fishHomeSpawned < fishHomeMax)
                {
                    float randx = Random.Range(width / 2.0f * -1, width / 2.0f);
                    float randy = yMin+5;

                    GameObject g = Instantiate(fishHomePrefab, new Vector3(randx, randy, 0), fishHomePrefab.transform.rotation);

                    fishHomeSpawned++;
                    allFishHomes.Add(g);
                    Debug.Log("Fish home spawned");
                }
                counter = 0f;
            }

            yield return null;
        }

    }

    public static void RemoveFishHome(GameObject toRemove)
    {
        int HomeIndex = allFishHomes.IndexOf(toRemove);
        allFishHomes.RemoveAt(HomeIndex);
    }


}
