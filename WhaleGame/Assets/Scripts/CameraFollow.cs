using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Start is called before the first frame update
    public float MoveSpeed;
    private Transform Player;

    void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.position += (Vector3)((Vector2)(Player.position-transform.position)*MoveSpeed*Time.deltaTime);
        
    }
}
