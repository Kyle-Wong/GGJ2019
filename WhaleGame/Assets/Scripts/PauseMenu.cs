using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public static bool Paused;
    public KeyCode PauseKey;
    private Canvas MyCanvas;
    void Awake()
    {
        Paused = false;
        MyCanvas = GetComponent<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Paused)
        {
            if(MainMenuController.AllowUIInput && Input.GetKeyDown(PauseKey)){
                Paused = false;
                MyCanvas.enabled = false;
                Time.timeScale = 1;
            }
        } else {
            if(MainMenuController.AllowUIInput && Input.GetKeyDown(PauseKey)){
                Paused = true;
                MyCanvas.enabled = true;
                Time.timeScale = 0;
            }
        }
    }
    public void ResumePress()
    {
        if(!MainMenuController.AllowUIInput)
            return;
        Paused = false;
        MyCanvas.enabled = false;
        Time.timeScale = 1;    
    }
    public void MainMenuPress()
    {
        if(!MainMenuController.AllowUIInput)
            return;
        Time.timeScale =1;
        SceneManager.LoadScene("MainMenu");
    }
}
