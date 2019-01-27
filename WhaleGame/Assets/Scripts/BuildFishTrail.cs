using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildFishTrail : MonoBehaviour
{
    // Start is called before the first frame update
    public static BuildFishTrail instance;
    public Transform FishPrefab;
    public static List<Vector3> Trail;
    public static List<GameObject> FishList;
    public static int MaxFishCount = 50;
    private int MaxLength;
    public int FishGap = 13;
    public int InitialGap = 10;

    void Awake()
    {
        instance = this;
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
    /*
    public void AddFish()
    {
        int index = FishGap+InitialGap;
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
    */
    public static void AddFish(GameObject g)
    {
        BuildFishTrail instance = BuildFishTrail.instance;
        int index = instance.InitialGap+instance.FishGap;
        g.GetComponent<FollowTrail>().enabled = true;

        if (FishList.Count > 0)
            index = FishList[FishList.Count-1].GetComponent<FollowTrail>().desiredIndex+instance.FishGap;
        if(index >= instance.MaxLength){
            //currently, do not make fish if they go beyond the maximum trail length
            return;
        }

        g.GetComponent<FollowTrail>().currentIndex = Mathf.Min(instance.MaxLength,index);
        g.GetComponent<FollowTrail>().desiredIndex = index;
        FishList.Add(g);
        Debug.Log("Added: " + FishList.IndexOf(g));
    }
    public static void RemoveFish(int fishIndex)
    {
        for(int i = fishIndex; i < FishList.Count; i++)
        {
            FishList[i].GetComponent<FollowTrail>().desiredIndex = Mathf.Max(0,FishList[i].GetComponent<FollowTrail>().desiredIndex-instance.FishGap);
        }

        GameObject temp = FishList[fishIndex];
        FishList.RemoveAt(fishIndex);
        Destroy(temp);
        Debug.Log("Removed: "+ fishIndex);
    }
    public static void RemoveFish(GameObject toRemove)
    {
        int FishIndex;

        if (FishList.IndexOf(toRemove) == -1)
        {
            Destroy(toRemove);
        }
        else
        {
            FishIndex = FishList.IndexOf(toRemove);
            Debug.Log(FishIndex);
            RemoveFish(FishIndex);
        }
    }
    //void OnDrawGizmos()
    //{
        
    //    Gizmos.color = Color.green;
    //    for(int i = 0; i < Trail.Count; i++)
    //    {
    //        Gizmos.DrawWireSphere(Trail[i],0.5f);
    //    }
    //}
}
