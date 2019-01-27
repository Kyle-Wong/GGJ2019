using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTrail : MonoBehaviour
{
    // Start is called before the first frame update
    public int desiredIndex;
    [HideInInspector]
    public float currentIndex;
    public float acceleration;
    private Vector3 Velocity;
    private Vector3 PosLastFrame;
    private bool SmoothMovement = true;

    void Start()
    {
    }

    // Update is called once per frame
    //

    void Update()
    {
        Velocity = PosLastFrame-transform.position;
        if(BuildFishTrail.Trail.Count > (int)currentIndex)
        {
            Vector2 target = BuildFishTrail.Trail[(int)currentIndex];
            if(SmoothMovement){
                transform.position = Vector3.SmoothDamp(transform.position,BuildFishTrail.Trail[(int)currentIndex],ref Velocity,.1f);
                if(Vector2.Distance(transform.position,target) < 5)
                {
                    SmoothMovement = false;
                }
            } else {
                transform.position = Vector3.Lerp(transform.position,BuildFishTrail.Trail[(int)currentIndex],0.08f);
            }
            
            if((int)currentIndex > desiredIndex)
            {
                currentIndex -= acceleration*Time.deltaTime;
            } else if((int)currentIndex < desiredIndex)
            {
                currentIndex += acceleration*Time.deltaTime;
            }
        }
        PosLastFrame = transform.position;
    }
}
