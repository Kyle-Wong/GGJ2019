using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatBehaviour : MonoBehaviour
{

    public Vector3 anchorPoint;
    public float patrolDistance;
    public GameObject trashPrefab;
    public float trashDropRate;

    public float boatSpeed;
    //public Transform[] patrolBounds;
   

    enum GameState { InitialSpawn, Swaying };
    GameState myState;

    private Vector3 LeftBound;
    private Vector3 RightBound;

    private bool isSwaying = false;
    private float trashTimer;
    private int currentPoint = 0;
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
            }

            if (transform.position.x <= LeftBound.x)
            {
                goingRight = true;
            }

            if(goingRight)
                newPos = new Vector3(RightBound.x, transform.position.y, transform.position.z);
            else
                newPos = new Vector3(LeftBound.x, transform.position.y, transform.position.z);

            this.transform.position = Vector3.MoveTowards(transform.position, newPos, boatSpeed * Time.deltaTime);


            yield return null;
        }
    }

    public void DropTrash()
    {
        Instantiate(trashPrefab, this.transform.position, Quaternion.identity);
    }

    public void SetAnchor(Vector3 pos)
    {
        anchorPoint = pos;
        SetupBounds();
        myState = GameState.InitialSpawn;
    }
}
