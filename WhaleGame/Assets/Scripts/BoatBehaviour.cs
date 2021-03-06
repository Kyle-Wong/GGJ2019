﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatBehaviour : MonoBehaviour
{

    public Vector3 anchorPoint;
    public float patrolDistance;
    public GameObject trashPrefab;
    public float trashDropRate;
    public float verticalTrashVelocity;
    public float minHorizontalTrashVelocity;
    public float maxHorizontalTrashVelocity;
    

    public float boatSpeed;
    //public Transform[] patrolBounds;
   

    enum GameState { InitialSpawn, Swaying };
    GameState myState;

    private Vector3 LeftBound;
    private Vector3 RightBound;

    private bool isSwaying = false;
    private float trashTimer;
    private Rigidbody rb;
    private bool goingRight = true;
     
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(myState);

        trashTimer += Time.deltaTime;

        if(trashTimer > trashDropRate)
        {
            //Debug.Log("Drop Trash");
            if(myState == GameState.Swaying)
                DropTrash();
            trashTimer = 0f;
        }


        if (myState == GameState.InitialSpawn)
        {
            if (!transform.position.Equals(anchorPoint))
            {
                MoveToAnchor();
            }
            else
                myState = GameState.Swaying;
        }

        if (myState == GameState.Swaying)
            if(!isSwaying)
                StartCoroutine("Sway");
                


    }

    public void MoveToAnchor()
    {
        if (anchorPoint.x < transform.position.x && goingRight == true)
        {
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, -transform.localScale.z);
            goingRight = false;
        }

        transform.position = Vector3.MoveTowards(transform.position, anchorPoint, boatSpeed * Time.deltaTime);
    }

    void SetupBounds()
    {
        LeftBound = new Vector3(
            anchorPoint.x - patrolDistance, 
            transform.position.y, 
            transform.position.z);

        RightBound = new Vector3(
            anchorPoint.x + patrolDistance,
            transform.position.y,
            transform.position.z);
    }


    IEnumerator Sway()
    {
        isSwaying = true;

        while (true)
        {
            //elapsedTime += Time.deltaTime;
            Vector3 newPos;


            if (transform.position.x >= RightBound.x)
            {
                goingRight = false;
                transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, -transform.localScale.z);
            }

            if (transform.position.x <= LeftBound.x)
            {
                goingRight = true;
                transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, Mathf.Abs(transform.localScale.z));
            }

            if (goingRight)
            {
                newPos = new Vector3(RightBound.x, transform.position.y, transform.position.z);
            }
            else
            {
                newPos = new Vector3(LeftBound.x, transform.position.y, transform.position.z);
            }

            this.transform.position = Vector3.MoveTowards(transform.position, newPos, boatSpeed * Time.deltaTime);


            yield return null;
        }
    }

    public void DropTrash()
    {
        GameObject trash = Instantiate(trashPrefab, this.transform.position, trashPrefab.transform.rotation);
        trash.transform.SetParent(this.transform, true);
        int direction = Random.value > 0.5f ? 1 : -1;
        trash.GetComponent<Rigidbody>().velocity = new Vector3(direction*Random.Range(minHorizontalTrashVelocity,maxHorizontalTrashVelocity),verticalTrashVelocity,0);
    }

    public void SetAnchor(Vector3 pos)
    {
        anchorPoint = pos;
        SetupBounds();
        myState = GameState.InitialSpawn;
    }
}
