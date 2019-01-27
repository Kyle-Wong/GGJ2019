using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsController : MonoBehaviour
{
    // Start is called before the first frame update
    public UIRevealer[] Names;
    public UIRevealer[] Roles;
    public float InitialDelay;
    public float RepeatDelay;

    void Awake()
    {
        StartCoroutine(CreditsRevealer());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private IEnumerator CreditsRevealer()
    {
        yield return new WaitForSeconds(InitialDelay);
        for(int i = 0; i < Names.Length; i++)
        {
            Names[i].revealUI();
            Roles[i].revealUI();
            yield return new WaitForSeconds(RepeatDelay);
        }
    }
    public void BackPress()
    {
        StartCoroutine(MainMenuController.TransitionThenLoad("MainMenu"));
    }
}
