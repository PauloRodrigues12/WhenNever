using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    //Animação
    public Animator transition;

    //Transição
    public float transitionTime = 1f;
    public int NextScene;

    public bool wasFromTP;

    public void LoadNextScene()
    {
        StartCoroutine(LoadLevel());
    }

    //Transição Fade in / Fade out entre cenas

    private IEnumerator LoadLevel()
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        if (wasFromTP == false)
        {
            int randomNr = Random.Range(1, 4);
            NextScene = randomNr;
        }
        SceneManager.LoadScene(NextScene);
        Time.timeScale = 1;
    }
}
