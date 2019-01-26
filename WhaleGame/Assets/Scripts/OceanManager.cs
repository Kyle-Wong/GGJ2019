using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OceanManager : MonoBehaviour
{
    public float width;
    public float height;

    public int fishMax = 5;
    private int fishSpawned = 0;
    public float spawnRate;
    public GameObject fishPrefab;

    private GameObject ground;
    private GameObject boatSpawner;

    private float timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        ground = GameObject.FindGameObjectWithTag("Seafloor");
        ground.transform.position = new Vector3(0, -height/2 - 1, 0);

        boatSpawner = GameObject.FindGameObjectWithTag("BoatSpawner");
        boatSpawner.transform.position = new Vector3(0, height / 2 - 1, 0);

        Debug.Log(width);
        boatSpawner.GetComponent<BoatSpawner>().SetWidth(width);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnRate && fishSpawned < fishMax)
        {
            SpawnFish();
            timer = 0f;
        }

    }

    void SpawnFish()
    {
        float randx = Random.Range(width / 2.0f * -1, width / 2.0f);
        float randy = Random.Range(height / 2.0f * -1, height / 2.0f);

        Instantiate(fishPrefab, new Vector3(randx, randy, 0), Quaternion.identity);
        fishSpawned++;
    }
}
