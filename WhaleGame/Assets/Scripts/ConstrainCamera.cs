using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstrainCamera : MonoBehaviour
{
    // Start is called before the first frame update
    public float LeftBounds;
    public float RightBounds;
    public float BottomBounds;

    private Camera Cam;
    public GameObject TopLeft;
    public GameObject BottomRight;
    private float CameraWidth;
    private float CameraHeight;
    void Start()
    {
        Cam = GetComponent<Camera>();
        CameraWidth = BottomRight.transform.position.x - TopLeft.transform.position.x;
        CameraHeight = TopLeft.transform.position.y - BottomRight.transform.position.y;
    }

    // Update is called once per frame
    void LateUpdate()
    {

        Vector3 CameraPos = transform.position;
        CameraPos.x = Mathf.Clamp(CameraPos.x,LeftBounds+CameraWidth/2,RightBounds-CameraWidth/2);
        CameraPos.y = Mathf.Max(CameraPos.y,BottomBounds+CameraHeight/2);
        transform.position = CameraPos;
    }
}
