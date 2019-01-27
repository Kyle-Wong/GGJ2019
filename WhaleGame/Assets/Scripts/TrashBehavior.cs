using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    public float WaterBuoyancy;
    public float MaxUnderWaterFallSpeed;
    public float WaterHorizontalDrag;
    public bool InWater = false;
    private Rigidbody rb;
    private float LifeTime;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        LifeTime += Time.deltaTime;
        if(InWater)
        {
            if(Mathf.Abs(rb.velocity.y) > MaxUnderWaterFallSpeed)
                rb.AddForce(Vector3.up*WaterBuoyancy,ForceMode.Force);
            if(Mathf.Abs(rb.velocity.x) < WaterHorizontalDrag*Time.deltaTime){
                rb.velocity = new Vector3(0,rb.velocity.y,0);
            } else {
                if(rb.velocity.x > 0)
                {
                    rb.velocity += Vector3.left*WaterHorizontalDrag*Time.deltaTime;
                } else{
                    rb.velocity += Vector3.right*WaterHorizontalDrag*Time.deltaTime;
                }
            }
            
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.collider.tag);
        if(collision.collider.CompareTag("Seafloor"))
        {
            Destroy(this.gameObject);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Fish"))
        {
            GameObject.FindGameObjectWithTag("UniqueSoundObject").GetComponent<AudioSource>().PlayOneShot(other.GetComponent<FishSounds>().FishDeathSound);
            BuildFishTrail.RemoveFish(other.gameObject);
        }
        if(other.CompareTag("Ocean") && LifeTime > 0.5f)
        {
            InWater = true;
        }
    }
}
