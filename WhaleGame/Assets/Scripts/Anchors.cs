using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anchors : MonoBehaviour
{

    private bool occupied;

    // Start is called before the first frame update
    void Start()
    {
        occupied = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool GetOccupied()
    {
        return occupied;
    }

    public void ToggleOccupied()
    {
        occupied = !occupied;
    }
}
