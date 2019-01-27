using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuController : MonoBehaviour
{
    // Start is called before the first frame update
    public string PlayScene;
    public string CreditsScene;
    public static bool AllowUIInput = true;
    void Awake()
    {
        AllowUIInput = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public static IEnumerator TransitionThenLoad(string nextScene)
    {
        AllowUIInput = false;
        ColorLerp transition = GameObject.FindGameObjectWithTag("Transition").GetComponentInChildren<ColorLerp>();
        transition.Activated = true;
        yield return new WaitForSeconds(transition.ChangeDuration);
        AllowUIInput = true;
        SceneManager.LoadScene(nextScene);
    }
    public void PlayButtonPress()
    {
        if(!AllowUIInput)
            return;
        StartCoroutine(TransitionThenLoad(PlayScene));
    }
    public void CreditsButtonPress()
    {
        if(!AllowUIInput)
            return;
        StartCoroutine(TransitionThenLoad(CreditsScene));
    }
    public void QuitButtonPress()
    {
        if(!AllowUIInput)
            return;
        Application.Quit();
    }
    
}
