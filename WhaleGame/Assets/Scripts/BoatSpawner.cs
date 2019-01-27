using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatSpawner : MonoBehaviour
{
    public float spawnRate;
    public int AnchorsPerSide;

    public GameObject boatPrefab;
    public GameObject anchorPrefab;

    private float width;

    private float timer = 0f;
    private GameObject[] anchors;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if(timer >= spawnRate)
        {
            if (anchors.Length > 0)
            {
                foreach (GameObject a in anchors)
                {
                    //Debug.Log(a.GetComponent<Anchors>().GetOccupied() == false);
                    if (a.GetComponent<Anchors>().GetOccupied() == false)
                    {
                        GameObject boat = Instantiate(boatPrefab, transform.position, boatPrefab.transform.rotation);
                        boat.transform.SetParent(this.transform, true);

                        boat.GetComponent<BoatBehaviour>().SetAnchor(a.transform.position);

                        a.GetComponentInParent<Anchors>().ToggleOccupied();
                        break;
                    }
                }
            }
            timer = 0f;
        }
    }

    public void SetWidth(float w)
    {
        width = w;
        Debug.Log("set width");
        SpawnAnchors();
    }

    private void SpawnAnchors()
    {
        float mid = width / 2;
        float interval = mid / AnchorsPerSide;

        Vector3 curPos = transform.position;

        for(int i = 1; i <= AnchorsPerSide; i++)
        {
            Vector3 newRightPos = new Vector3(curPos.x + (interval*i), curPos.y, 0f);

            Vector3 newLeftPos = new Vector3(curPos.x - (interval*i), curPos.y, 0f);

            GameObject anchorRight = Instantiate(anchorPrefab, newRightPos, Quaternion.identity);
            anchorRight.transform.SetParent(this.transform, true);

            GameObject anchorLeft = Instantiate(anchorPrefab, newLeftPos, Quaternion.identity);
            anchorLeft.transform.SetParent(this.transform, true);
        }

        anchors = GameObject.FindGameObjectsWithTag("Anchor");


    }

}
