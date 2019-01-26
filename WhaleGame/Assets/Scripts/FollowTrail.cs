using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTrail : MonoBehaviour
{
    // Start is called before the first frame update
    public int index;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(BuildFishTrail.Trail.Count > index)
            transform.position = BuildFishTrail.Trail[index];
    }
}
