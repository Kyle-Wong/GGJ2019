using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstrainCamera : MonoBehaviour
{
    // Start is called before the first frame update
    private Camera Cam;
    public Vector2 TopLeft;
    public Vector2 BottomRight;
    private float CameraWidth;
    private float CameraHeight;
    void Start()
    {
        Cam = GetComponent<Camera>();
        CameraWidth = Cam.ViewportToWorldPoint(new Vector2(1,0)).x - Cam.ViewportToWorldPoint(new Vector2(0,0)).x;
        CameraHeight = Cam.ViewportToWorldPoint(new Vector2(1,0)).y - Cam.ViewportToWorldPoint(new Vector2(0,0)).y;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 CameraPos = transform.position;
        CameraPos.x = Mathf.Clamp(CameraPos.x,TopLeft.x+CameraWidth/2,BottomRight.x-CameraWidth/2);
        CameraPos.y = Mathf.Clamp(CameraPos.y,BottomRight.y+CameraHeight/2,TopLeft.y-CameraHeight/2);
        transform.position = CameraPos;
    }
}
