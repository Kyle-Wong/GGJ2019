using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatSpawner : MonoBehaviour
{
    public float spawnRate;
    public GameObject boatPrefab;

    private float timer = 0f;
    private Transform[] anchors;
    

    // Start is called before the first frame update
    void Start()
    {
        anchors = this.GetComponentsInChildren<Transform>();
        foreach(Transform a in anchors)
        {
            Debug.Log(a.transform.position);

            Anchors anc = a.GetComponentInParent<Anchors>();

            Debug.Log(anc.GetOccupied());
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if(timer >= spawnRate)
        {
            /*foreach (Transform a in anchors)
            {
                Debug.Log(a.GetComponentInParent<Anchors>().getOccupied() == false);
                if (a.GetComponentInParent<Anchors>().getOccupied() == false)
                {
                    GameObject boat = Instantiate(boatPrefab, transform.position, Quaternion.identity);
                    boat.GetComponent<BoatBehaviour>().SetAnchor(a.position);

                    a.GetComponentInParent<Anchors>().toggleOccupied();
                    break;
                }
            }*/
            timer = 0f;
        }
    }
}
