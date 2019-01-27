using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSounds : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioClip FishCollectSound;
    public AudioClip FishDeathSound;
    public AudioClip FishGetsHomeSound;
    private AudioSource Source;
    void Start()
    {
        Source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlaySound(AudioClip clip)
    {
        Source.PlayOneShot(clip);
    }

}
