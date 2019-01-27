using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class BackgroundMusic : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioClip MenuMusic;
    public AudioClip GameMusic;
    private AudioSource Source;
    private string Scene;
    void Start()
    {
        Source = GetComponent<AudioSource>();
        Scene = SceneManager.GetActiveScene().name;
    }

    // Update is called once per frame
    void Update()
    {
        string SceneName = SceneManager.GetActiveScene().name;
        if(Scene != SceneName)
        {
            if(SceneName.Equals("GameScene")){
                Source.clip = GameMusic;
                Source.Play();
            } 
            if(SceneName.Equals("MainMenu") || SceneName.Equals("CreditsScene")){
                if(Scene.Equals("GameScene")){
                    Source.clip = MenuMusic;
                    Source.Play();
                }
            }
        }
        Scene = SceneName;
    }
}
