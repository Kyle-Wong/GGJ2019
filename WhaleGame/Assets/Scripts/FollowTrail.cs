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

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
        if(BuildFishTrail.Trail.Count > (int)currentIndex)
        {
            transform.position = BuildFishTrail.Trail[(int)currentIndex];
            if((int)currentIndex > desiredIndex)
            {
                currentIndex -= acceleration*Time.deltaTime;
            } else if((int)currentIndex < desiredIndex)
            {
                currentIndex += acceleration*Time.deltaTime;
            }
        }
        
    }
}
