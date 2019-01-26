using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildFishTrail : MonoBehaviour
{
    // Start is called before the first frame update
    public static List<Vector3> Trail;
    public int MaxLength;

    void Awake()
    {
        Trail = new List<Vector3>();
    }

    // Update is called once per frame
    void Update()
    {
        Trail.Add(transform.position);
        if(Trail.Count >= MaxLength){
            Trail.RemoveAt(0);
        }
    }
    
    void OnDrawGizmos()
    {
        
        Gizmos.color = Color.green;
        for(int i = 0; i < Trail.Count; i++)
        {
            Gizmos.DrawWireSphere(Trail[i],0.5f);
        }
    }
}
