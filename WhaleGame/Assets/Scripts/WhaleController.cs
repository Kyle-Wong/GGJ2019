using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhaleController : MonoBehaviour
{
    // Start is called before the first frame update
    protected Camera MainCam;
    public float RotationSpeed;
    public float MoveSpeed;
    protected float MaxSpeed;
    public float DashSpeed;
    public float DashDuration;
    public AnimationCurve DashVelocityCurve;
    protected bool FlipY = false;
    public float FlipSpeed;
    protected float DashTimer;
    protected float StoppedTimer;
    public float LandInWaterParalysisDuration;
    public bool IsDead = false;
    protected float ZRotation;
    protected float XRotation;
    protected Rigidbody rb;
    public bool InWater;
    public enum WhaleState{
        MOVE,DASH,DEAD,AIRBORNE,PREGAME,STOPPED
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
        MaxSpeed = MoveSpeed;
        StoppedTimer = 0;
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
                DashWhale(DashSpeed*DashVelocityCurve.Evaluate(DashTimer/DashDuration));
                if(DashTimer < DashDuration)
                    DashTimer += Time.deltaTime;
                else    
                    State = WhaleState.MOVE;
                break;
            case WhaleState.DEAD:
                break;
            case WhaleState.AIRBORNE:
                RotateWhaleDirection();
                rb.AddForce(Vector3.down*4,ForceMode.Acceleration);
                break;
            case WhaleState.PREGAME:
                break;
            case WhaleState.STOPPED:
                if(StoppedTimer > 0)
                    StoppedTimer -= Time.deltaTime;
                else
                    State = WhaleState.MOVE;
                RotateWhaleDirection();
                break;
        }
        KeepWhaleUpright();
    }
    protected void RotateWhaleDirection()
    {
        
        transform.rotation = Quaternion.RotateTowards(Quaternion.Euler(0,0,ZRotation),Quaternion.Euler(0,0,GetAngleToMouse()),RotationSpeed*Time.deltaTime);
        ZRotation = transform.rotation.eulerAngles.z;
    }
    protected void MoveWhale(float velocity)
    {
        rb.AddForce(transform.right*(-1)*velocity,ForceMode.Force);

    }
    protected void DashWhale(float velocity)
    {
        rb.velocity = transform.right*-1*velocity;
    }
    protected void KeepWhaleUpright()
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
    private void StopWhaleTemporarily(float duration)
    {
        StoppedTimer = duration;
        State = WhaleState.STOPPED;
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
    void OnTriggerEnter(Collider col)
    {
        if(col.CompareTag("Fish"))
        {
            if(BuildFishTrail.FishList.IndexOf(col.gameObject) == -1 && BuildFishTrail.FishList.Count < BuildFishTrail.MaxFishCount)
            {
                BuildFishTrail.AddFish(col.gameObject);
                foreach (Transform t in col.transform) {
                    if(t.gameObject.name.Equals("FishCollectIndicator")){
                        Destroy(t.gameObject);
                    }
                }
            }
        }
    }
    void OnTriggerStay(Collider col)
    {
        if(col.CompareTag("Ocean"))
        {
            InWater = true;
            rb.useGravity = false;
            if(State == WhaleState.AIRBORNE){
                if(transform.rotation.eulerAngles.z > 268 && transform.rotation.eulerAngles.z < 330){
                    StopWhaleTemporarily(LandInWaterParalysisDuration);
                }
                else
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
    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(new Ray(transform.position,transform.right*-1));
    }
}
