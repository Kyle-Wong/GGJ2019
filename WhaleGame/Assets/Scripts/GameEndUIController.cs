using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameEndUIController : MonoBehaviour
{
    // Start is called before the first frame update
    private EventSystem EventSystem;
    private Canvas MyCanvas;
    public GameObject DefaultButton;
    
    void Awake()
    {
        EventSystem = GameObject.FindGameObjectWithTag("EventSystem").GetComponent<EventSystem>();
        MyCanvas = GetComponent<Canvas>();
        MyCanvas.enabled = false;
    }

    public void ShowUI(bool b)
    {
        if(b)
        {
            MyCanvas.enabled = true;
            if(DefaultButton != null)
                EventSystem.SetSelectedGameObject(DefaultButton);
        } else {
            MyCanvas.enabled = false;
        }
    }
    public void MakeButtonActive(GameObject g)
    {
        EventSystem.SetSelectedGameObject(g);
    }
    public void RetryPress()
    {
        StartCoroutine(MainMenuController.TransitionThenLoad("GameScene"));
    }
    public void MainMenuPress()
    {
        StartCoroutine(MainMenuController.TransitionThenLoad("MainMenu"));
    }
}
