using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public float XVelocity;
    public float VerticalPeriodSpeed;
    public float VerticalAmplitude;
    private float Timer;

    void Start()
    {
        Timer = 0;   
    }

    // Update is called once per frame
    void Update()
    {
        Timer += Time.deltaTime*VerticalPeriodSpeed;
        transform.position += Vector3.right*XVelocity*Time.deltaTime;
        transform.position += Vector3.up*Mathf.Sin(Timer)*VerticalAmplitude*Time.deltaTime;
    }
}
