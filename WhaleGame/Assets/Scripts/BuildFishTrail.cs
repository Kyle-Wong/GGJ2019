using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildFishTrail : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform FishPrefab;
    public static List<Vector3> Trail;
    public static List<GameObject> FishList;
    public int MaxFishCount = 8;
    private int MaxLength;
    public int FishGap = 20;

    void Awake()
    {
        MaxLength = MaxFishCount*FishGap;
        Trail = new List<Vector3>();
        FishList = new List<GameObject>();
        for(int i = 0; i < MaxLength; i++)
        {
            Trail.Add(transform.position);
        }
    }

    // Update is called once per frame
    void Update()
    {
      
        Trail.Insert(0,transform.position);
        if(Trail.Count >= MaxLength){
            Trail.RemoveAt(Trail.Count-1);
        }
    }
    public void AddFish()
    {
        int index = (int)(FishGap*1.8f);
        if(FishList.Count > 0)
            index = FishList[FishList.Count-1].GetComponent<FollowTrail>().desiredIndex+FishGap;
        if(index >= MaxLength){
            //currently, do not make fish if they go beyond the maximum trail length
            return;
        }
        Transform fish = Instantiate(FishPrefab,Trail[index],Quaternion.identity);
        fish.GetComponent<FollowTrail>().currentIndex = Mathf.Min(MaxLength,index);
        fish.GetComponent<FollowTrail>().desiredIndex = index;
        FishList.Add(fish.gameObject);
    }
    public void RemoveFish(int fishIndex)
    {
        for(int i = fishIndex; i < FishList.Count; i++)
        {
            FishList[i].GetComponent<FollowTrail>().desiredIndex = Mathf.Max(0,FishList[i].GetComponent<FollowTrail>().desiredIndex-FishGap);
        }
        Destroy(FishList[fishIndex]);
        FishList.RemoveAt(fishIndex);
    }
    void OnDrawGizmos()
    {
        
        Gizmos.color = Color.green;
        for(int i = 0; i < Trail.Count; i++)
        {
            Gizmos.DrawWireSphere(Trail[i],0.5f);
        }
    }
}
