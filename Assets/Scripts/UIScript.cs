using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIScript : MonoBehaviour
{
    //Menu de Pausa
    public GameObject showmenu;
    public GameObject hidemenu;

    private LevelLoader levelLoader;

    private void Start()
    {
        levelLoader = GameObject.FindGameObjectWithTag("LevelLoader").GetComponent<LevelLoader>();
    }

    //Começar a transição de cena
    public void Changescreen()
    {
        levelLoader.wasFromTP = false;
        levelLoader.LoadNextScene();
    }

    //Começar a transição de cena para o menu
    public void BackToMenu()
    {
        levelLoader.wasFromTP = true;
        levelLoader.LoadNextScene();
        Time.timeScale = 1;
        hidemenu.SetActive(false);
    }

    //Sair do jogo
    public void QuitGame()
    {
        Application.Quit();
    }

    //Ativar / Desativar menu de pausa
    public void ClickOpen()
    {
        showmenu.SetActive(true);
    }

    public void ClickClose()
    {
        hidemenu.SetActive(false);
    }

    public void Continue()
    {
        Time.timeScale = 1;
    }

    public void Pause()
    {
        Time.timeScale = 0;
    }
}