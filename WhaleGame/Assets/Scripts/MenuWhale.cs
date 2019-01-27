using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuWhale : WhaleController
{
    // Start is called before the first frame update
    public float ANGLEUP;
    public float ANGLEDOWN;
    public Transform[] FishType;
    public float LoopDuration;
    private float LoopTimer;
    private Vector3 StartingPosition;
    void Start()
    {
        StartingPosition = transform.position;
        LoopTimer = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(LoopTimer < LoopDuration)
        {
            LoopTimer += Time.deltaTime;
        } else {
            LoopTimer = 0;
            transform.position = StartingPosition;
            SpawnFish();
            rb.velocity = Vector3.zero;
            transform.rotation = Quaternion.Euler(0,0,0);
            ZRotation = transform.rotation.eulerAngles.z;
        }
        if(State == WhaleState.MOVE){
            RotateWhaleDirection(ANGLEUP);
            MoveWhale(MoveSpeed);
        } else if(State == WhaleState.AIRBORNE)
        {
            RotateWhaleDirection(ANGLEDOWN);

        }
        KeepWhaleUpright();
    }
    private void RotateWhaleDirection(float angle)
    {
        
        transform.rotation = Quaternion.RotateTowards(Quaternion.Euler(0,0,ZRotation),Quaternion.Euler(0,0,angle),RotationSpeed*Time.deltaTime);
        ZRotation = transform.rotation.eulerAngles.z;
    }
    void OnTriggerStay(Collider col)
    {
        if(col.CompareTag("Ocean"))
        {
            InWater = true;
            rb.useGravity = false;
            if(State == WhaleState.AIRBORNE){
                StartCoroutine(SpawnSplashPrefab(.15f));
                State = WhaleState.MOVE;
            }
        }
    }
    void OnTriggerExit(Collider col)
    {
        if(col.CompareTag("Ocean"))
        {
            InWater = false;
            State = WhaleState.AIRBORNE;
            rb.useGravity = true;
        }
    }
    private void SpawnFish()
    {
        int length = BuildFishTrail.FishList.Count;
        for(int i = 0; i < length; i++)
        {
            BuildFishTrail.RemoveFish(0);
        }
        for(int i = 0; i < BuildFishTrail.Trail.Count; i++)
        {
            BuildFishTrail.Trail[i] = transform.position;
        }
        for(int i = 0; i < 3; i++)
        {
            Transform g = Instantiate(FishType[i],transform.position,Quaternion.identity);
        }
    }
}
