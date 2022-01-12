using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using System;

public class AudioManager : MonoBehaviour
{
    public AudioMixer mixer;
    public GameObject musicON;
    public GameObject musicOFF;
    public GameObject sfxON;
    public GameObject sfxOFF;
    bool music = true;
    bool sfx = true;
    public float volumemusica = 0f;
    public float volumesfx = 0f;

    private void Update()
    {
        //Buscar PlayerPrefs
        volumemusica = PlayerPrefs.GetFloat("volumemusicaatual");
        volumesfx = PlayerPrefs.GetFloat("volumesfxatual");

        //Atribuir valores aos mixers
        mixer.SetFloat("MusicVolume", volumemusica);
        mixer.SetFloat("SFXVolume", volumesfx);

        //Ativar / Desativar icons
        if (volumemusica == 0f)
        {
            music = true;
            musicON.SetActive(true);
            musicOFF.SetActive(false);
        }
        else
        {
            music = false;
            musicON.SetActive(false);
            musicOFF.SetActive(true);
        }

        if (volumesfx == 0f)
        {
            sfx = true;
            sfxON.SetActive(true);
            sfxOFF.SetActive(false);
        }
        else
        {
            sfx = false;
            sfxON.SetActive(false);
            sfxOFF.SetActive(true);
        }
    }
    public void ClickButtonMusic()
    {
        //Ativar musica
        if (music == false)
        {
            volumemusica = 0f;
            PlayerPrefs.SetFloat("volumemusicaatual", volumemusica);
            music = true;
            musicON.SetActive(true);
            musicOFF.SetActive(false);
            mixer.SetFloat("MusicaVolume", volumemusica);
        }
        //Desativar musica
        else if (music == true)
        {
            volumemusica = -80f;
            PlayerPrefs.SetFloat("volumemusicaatual", volumemusica);
            music = false;
            musicON.SetActive(false);
            musicOFF.SetActive(true);
            mixer.SetFloat("MusicaVolume", volumemusica);
        }
    }

    public void ClickButtonSFX()
    {
        //Ativar efeitos sonoros
        if (sfx == false)
        {
            volumesfx = 0f;
            PlayerPrefs.SetFloat("volumesfxatual", volumesfx);
            sfx = true;
            sfxON.SetActive(true);
            sfxOFF.SetActive(false);
            mixer.SetFloat("SFXVolume", volumesfx);
        }
        //Desativar efeitos sonoros
        else if (sfx == true)
        {
            volumesfx = -80f;
            PlayerPrefs.SetFloat("volumesfxatual", volumesfx);
            sfx = false;
            sfxON.SetActive(false);
            sfxOFF.SetActive(true);
            mixer.SetFloat("SFXVolume", volumesfx);
        }
    }
}
