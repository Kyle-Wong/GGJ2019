using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatSpawner : MonoBehaviour
{
    public float spawnRate;
    public GameObject boatPrefab;

    private float timer = 0f;
    private GameObject[] anchors;


    // Start is called before the first frame update
    void Start()
    {
        anchors = GameObject.FindGameObjectsWithTag("Anchor");
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if(timer >= spawnRate)
        {
            foreach (GameObject a in anchors)
            {
                //Debug.Log(a.GetComponent<Anchors>().GetOccupied() == false);
                if (a.GetComponent<Anchors>().GetOccupied() == false)
                {
                    GameObject boat = Instantiate(boatPrefab, transform.position, Quaternion.identity);
                    boat.GetComponent<BoatBehaviour>().SetAnchor(a.transform.position);

                    a.GetComponentInParent<Anchors>().ToggleOccupied();
                    break;
                }
            }
            timer = 0f;
        }
    }
}
