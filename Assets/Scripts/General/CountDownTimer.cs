using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;


    public class CountDownTimer : MonoBehaviour
    {
        [SerializeField] private float currentTime = 0f;
        [SerializeField] private float startingTime = 10f;
        [SerializeField] private Text countDownText;
        [SerializeField] public bool startDuel;
        [SerializeField] private FirstPersonController player;
        [SerializeField] private AI_Enemy enemy;
        [HideInInspector] private bool restart;
        [HideInInspector] public bool canCount;   

       
        // Start is called before the first frame update
        void Start(){
            restart = false;
            currentTime = startingTime;
        }

        // Update is called once per frame
        void Update()
        {
            if(canCount){
                CountDownController();
            }else{
                currentTime = startingTime;
            }
        }

        // TimerController
        void CountDownController(){
            currentTime -= 1 * Time.deltaTime;
            countDownText.text = currentTime.ToString("0");
   
            if(currentTime <= 0 && !restart){
                currentTime = 0;
                startDuel = true;
                player.GetComponent<FirstPersonController>().enabled = true;
                enemy.GetComponent<AI_Enemy>().enabled = false;

            } else if(restart){
                currentTime = startingTime;
                restart = !restart;
               
            }
        }

    }
