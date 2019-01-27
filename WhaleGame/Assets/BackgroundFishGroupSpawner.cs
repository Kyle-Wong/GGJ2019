using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundFishGroupSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    public float MinInterval;
    public float MaxInterval;
    private float TimeToWait;
    public float YRange;
    public Transform FishGroupPrefab;
    public bool FacingRight;
    void Start()
    {
        StartCoroutine(SpawnLoop());
    }

    // Update is called once per frame
    private IEnumerator SpawnLoop()
    {
        while(true){
            SpawnFishGroup();
            yield return new WaitForSeconds(Random.Range(MinInterval,MaxInterval));
        }   
    }
    private void SpawnFishGroup()
    {
        Transform t = Instantiate(FishGroupPrefab,transform.position,Quaternion.identity);
        t.position += Vector3.up*Random.Range(-YRange,YRange);
        t.GetComponent<ConstantMovement>().XVelocity = -12;
        if(FacingRight)
            t.GetComponent<ConstantMovement>().XVelocity *= -1;
        Destroy(t.gameObject,15);
    }
}
