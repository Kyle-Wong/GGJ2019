using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakePersistentUnique : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        GameObject[] g = GameObject.FindGameObjectsWithTag("UniqueSoundObject");
        if(g.Length > 1)
            Destroy(gameObject);
        else
            DontDestroyOnLoad(gameObject);
    }
  

    // Update is called once per frame
    void Update()
    {
        
    }
}
