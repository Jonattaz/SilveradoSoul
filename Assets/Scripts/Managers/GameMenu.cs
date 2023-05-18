using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityStandardAssets.Characters.FirstPerson;


public class GameMenu : MonoBehaviour
{
        [SerializeField] private FirstPersonController character;
        [SerializeField] private GameObject configurationPanel;
        [SerializeField] private GameObject deathMenu;
        [SerializeField] private GameObject controlsPanel;
        [SerializeField] private GameObject gameHUD;
        [SerializeField] private AudioMixer musicAudioMixer;
        [SerializeField] private AudioMixer fxAudioMixer;
        [SerializeField] private SlowMotion slowMotionMech;
        [SerializeField] private string menuScene;
        [SerializeField] private string reloadScene;
        [SerializeField] private bool deadInfo; 

        [SerializeField] private bool isPaused;
        [SerializeField] private KeyCode menuButton;
        public static GameMenu gameMenuInstance;

        void Start(){
            
            gameMenuInstance = this;
            Time.timeScale = 1;
        }

        // Update is called once per frame
        void Update()
        {
            if(deadInfo){
                PlayerDead();
            }else{
                MenuActivator();
            }  
        }

        private void MenuActivator(){
            if(UnityEngine.Input.GetKeyDown(menuButton)){
                // Ativar menu
                slowMotionMech.enabled = false;
                character.canMove = false;
                isPaused = true; 
                gameHUD.SetActive(false);
                configurationPanel.SetActive(true);
            }
            
            if(character.canLock)
                PauseGame();
        }
        public void CloseConfigurations(){
                // Desativar menu
                character.canMove = true;
                isPaused = false;
                gameHUD.SetActive(true);
                configurationPanel.SetActive(false);
                slowMotionMech.enabled = true;

        }

        public void OpenControls(){
            configurationPanel.SetActive(false);
            controlsPanel.SetActive(true);
        }

        public void CloseControls(){
            controlsPanel.SetActive(false);
            configurationPanel.SetActive(true);
        }


    	public void LoadMenu(){
			if(menuScene != ""){
                Time.timeScale = 1;
				SceneManager.LoadScene(menuScene, LoadSceneMode.Single);
			}
		}

        private void PauseGame(){
            if(isPaused){
                Time.timeScale = 0;
                isPaused = true;
                
            }else{
                Time.timeScale = 1;
                isPaused = false;
            }
        }

        public void ReloadGame(){
            if(reloadScene != ""){
				SceneManager.LoadScene(reloadScene, LoadSceneMode.Single);
			}
        }

        public void PlayerDead(){
            Time.timeScale = 0;
            configurationPanel.SetActive(false);
            deathMenu.SetActive(true);
        }

        public void SetMusicVolume(float sliderValue){
            musicAudioMixer.SetFloat("MusicMasterVolume", Mathf.Log10(sliderValue) * 20);
        }

        public void SetFXVolume(float sliderValue){
            fxAudioMixer.SetFloat("FXMasterVolume", Mathf.Log10(sliderValue) * 20);
        }
}
