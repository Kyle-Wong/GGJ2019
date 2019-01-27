using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhaleController : MonoBehaviour
{
    // Start is called before the first frame update
    private Camera MainCam;
    public float RotationSpeed;
    public float MoveSpeed;
    public float DashSpeed;
    public float DashDuration;
    public AnimationCurve DashVelocityCurve;
    private bool FlipY = false;
    public float FlipSpeed;
    private float DashTimer;
    public bool IsDead = false;
    private float ZRotation;
    private float XRotation;
    private Rigidbody rb;
    public enum WhaleState{
        MOVE,DASH,DEAD,STOPPED,PREGAME
    }
    [HideInInspector]
    public WhaleState State;
    void Awake()
    {
        MainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        rb = GetComponent<Rigidbody>();
        State = WhaleState.MOVE;
        DashTimer = DashDuration;
        XRotation = transform.rotation.eulerAngles.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        switch(State)
        {
            case WhaleState.MOVE:
                RotateWhaleDirection();
                MoveWhale(MoveSpeed);
                if(Input.GetMouseButtonDown(0)) //left mouse
                {
                    Dash();
                }
                break;
            case WhaleState.DASH:
                MoveWhale(DashSpeed*DashVelocityCurve.Evaluate(DashTimer/DashDuration));
                if(DashTimer < DashDuration)
                    DashTimer += Time.deltaTime;
                else    
                    State = WhaleState.MOVE;
                break;
            case WhaleState.DEAD:
                break;
            case WhaleState.STOPPED:
                break;
            case WhaleState.PREGAME:
                break;
        }
        KeepWhaleUpright();
    }
    private void RotateWhaleDirection()
    {
        
        transform.rotation = Quaternion.RotateTowards(Quaternion.Euler(0,0,ZRotation),Quaternion.Euler(0,0,GetAngleToMouse()),RotationSpeed*Time.deltaTime);
        ZRotation = transform.rotation.eulerAngles.z;
    }
    private void MoveWhale(float velocity)
    {
        rb.velocity = transform.right*(-1)*velocity;
    }
    private void KeepWhaleUpright()
    {
        transform.rotation = Quaternion.Euler(0,0,ZRotation);
        float epsilon = 10;
        FlipY = !(ZRotation > 90 && ZRotation < 270);
        if(FlipY)
        {
            if(XRotation < 0)
            {
                XRotation += FlipSpeed*Time.deltaTime;
            } else{
                XRotation -= FlipSpeed*Time.deltaTime;
            }
            if(Mathf.Abs(XRotation-0)<epsilon)
                XRotation = 0;
        } else {
            if(XRotation < 180)
            {
                XRotation += FlipSpeed*Time.deltaTime;
            } else{
                XRotation -= FlipSpeed*Time.deltaTime;
            }
            if(Mathf.Abs(XRotation-180)<epsilon)
                XRotation = 180;
        }
        transform.RotateAround(transform.position,transform.right,XRotation);

    }
    private void Dash()
    {
        DashTimer = 0;
        State = WhaleState.DASH;
        
    }
    private float GetAngleToMouse()
    {
        Vector2 MouseWorldPoint = MainCam.ScreenToViewportPoint(Input.mousePosition);
        Vector2 MoveVector = new Vector2(0.5f,0.5f)-MouseWorldPoint;
        return Mathf.Rad2Deg*Mathf.Atan2(MoveVector.y,MoveVector.x);
    }
    void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Trash"))
        {
            Destroy(collision.gameObject);
        }
        
    }
    void OnTriggerEnter(Collider collider)
    {
        if(collider.CompareTag("Fish"))
        {
            BuildFishTrail.AddFish(collider.gameObject);
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(new Ray(transform.position,transform.right*-1));
    }
}
