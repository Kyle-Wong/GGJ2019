using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTimerUI : MonoBehaviour
{
    // Start is called before the first frame update
    
    private GameController gc;

    private Text text;

    
  

    void Start()
    {
        gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if(gc.GameTimer < gc.GameDuration)
        {
            int minutes = (int)(gc.GameDuration-gc.GameTimer)/60;
            int seconds = (int)(gc.GameDuration-gc.GameTimer)%60;
            if(seconds <= 9)
                text.text = minutes + ":0"+seconds;
            else
                text.text = minutes + ":" + seconds;
        } else {
            text.text = "";
        }
    }
}
