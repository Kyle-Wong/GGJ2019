using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public float Radius;
    public Vector3 Center;
    public float RotationSpeed;
    private float Angle;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Center + new Vector3(Mathf.Cos(Angle),Mathf.Sin(Angle))*Radius;
        Angle += RotationSpeed*Time.deltaTime;
    }
}
