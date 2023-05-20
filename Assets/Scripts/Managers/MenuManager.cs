using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MenuManager : MonoBehaviour{

    [SerializeField] private string levelName;
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject configurationPanel;
    [SerializeField] private GameObject controlsPanel;
    [SerializeField] private GameObject creditsPanel;
    [SerializeField] private AudioMixer musicAudioMixer;
    [SerializeField] private AudioMixer fxAudioMixer;

    public void Play(){
        SceneManager.LoadScene(levelName);
    }
    
    public void OpenOptions(){
        menuPanel.SetActive(false);
        configurationPanel.SetActive(true);
    }

    public void OpenControls(){
        configurationPanel.SetActive(false);
        controlsPanel.SetActive(true);
    }

     public void OpenCredits(){
        menuPanel.SetActive(false);
        creditsPanel.SetActive(true);
    }

    public void CloseOptions(){
        configurationPanel.SetActive(false);
        menuPanel.SetActive(true);
    }

     public void CloseCredits(){
        creditsPanel.SetActive(false);
        menuPanel.SetActive(true);
    }

    public void CloseControls(){
        controlsPanel.SetActive(false);
        configurationPanel.SetActive(true);
    }

    public void SetMusicVolume(float sliderValue){
        musicAudioMixer.SetFloat("MusicMasterVolume", Mathf.Log10(sliderValue) * 20);
    }

    public void SetFXVolume(float sliderValue){
        fxAudioMixer.SetFloat("FXMasterVolume", Mathf.Log10(sliderValue) * 20);
    }

    public void ExitGame(){
        Application.Quit();
    }


}








