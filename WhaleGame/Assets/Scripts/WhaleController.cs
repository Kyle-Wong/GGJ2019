using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhaleController : MonoBehaviour
{
    // Start is called before the first frame update
    private Camera MainCam;
    public float RotationSpeed;
    public float MoveSpeed;
    private float CurrentAngle;
    private Rigidbody rb;
    void Awake()
    {
        MainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.RotateTowards(Quaternion.Euler(0,0,CurrentAngle),Quaternion.Euler(0,0,GetAngleToMouse()),RotationSpeed*Time.deltaTime);
        CurrentAngle = transform.rotation.eulerAngles.z;
        rb.velocity = transform.right*(-1)*MoveSpeed*Time.deltaTime;
    }
    private float GetAngleToMouse()
    {
        Vector2 MouseWorldPoint = MainCam.ScreenToViewportPoint(Input.mousePosition);
        Vector2 MoveVector = new Vector2(0.5f,0.5f)-MouseWorldPoint;
        return Mathf.Rad2Deg*Mathf.Atan2(MoveVector.y,MoveVector.x);
    }
    
}
