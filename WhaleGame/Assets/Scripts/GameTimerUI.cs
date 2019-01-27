using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTimerUI : MonoBehaviour
{
    // Start is called before the first frame update
    private GameController gc;
        public GameObject InnerBar;

    private RectTransform Rect;
    private Image Image;

    
    public Color[] Colors;
    public Text text;
    public string[] Phrases;

    void Start()
    {
        gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        Rect = InnerBar.GetComponent<RectTransform>();
        Image = InnerBar.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        float progression = gc.GameTimer/gc.GameDuration;
        Rect.anchorMax = new Vector2(Mathf.Lerp(0,1,progression),1);
        if(progression > .8f)
        {
            text.text = Phrases[2];
            Image.color = Colors[2];
        } else if(progression > 0.4f)
        {
            text.text = Phrases[1];
            Image.color = Colors[1];
        } else{
            text.text = Phrases[0];
            Image.color = Colors[0];
        }
    }
}
