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
    private float DashTimer;
    public bool IsDead = false;
    private float CurrentAngle;
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
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Input.GetKeyDown(KeyCode.Space)){
            GetComponent<BuildFishTrail>().AddFish();
        }
        switch(State)
        {
            case WhaleState.MOVE:
                RotateWhale();
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
    }
    private void RotateWhale()
    {
        transform.rotation = Quaternion.RotateTowards(Quaternion.Euler(0,0,CurrentAngle),Quaternion.Euler(0,0,GetAngleToMouse()),RotationSpeed*Time.deltaTime);
        CurrentAngle = transform.rotation.eulerAngles.z;
    }
    private void MoveWhale(float velocity)
    {
        rb.velocity = transform.right*(-1)*velocity;
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
    
}
