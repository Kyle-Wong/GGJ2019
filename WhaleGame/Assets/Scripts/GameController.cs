using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
    public static bool GameOver = false;
    public float GameDuration;
    [HideInInspector]
    public float GameTimer;
    void Start()
    {
        GameOver = false;
        GameTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        GameTimer += Time.deltaTime;
        if(GameTimer > GameDuration)
        {
            GameOver = true;
        }
    }
}
