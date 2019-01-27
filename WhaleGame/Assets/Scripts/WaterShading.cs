using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterShading : MonoBehaviour
{
    GameController gc;
    public MeshRenderer Front;
    public MeshRenderer Back;
    public Color StartColor;
    public Color EndColor;
    // Start is called before the first frame update
    void Start()
    {
        gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        float progression = gc.GameTimer/gc.GameDuration;
        Color newColor = Color.Lerp(StartColor,EndColor,progression);
        Front.material.SetColor("Color_FEB1F004",newColor);
        Back.material.SetColor("Color_FEB1F004",newColor);

    }
}
