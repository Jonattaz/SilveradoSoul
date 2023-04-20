using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;


    public class CountDownTimer : MonoBehaviour
    {
        [SerializeField] private float currentTime = 0f;
        [SerializeField] private float startingTime = 5f;
        [SerializeField] private Text countDownText;
        [SerializeField] private Text warningMessage;
        [SerializeField] public bool startDuel;
        [SerializeField] private FirstPersonController player;
        [SerializeField] private LeaningController leaningMech;
        [SerializeField] private Crouch crouchMech;
        [SerializeField] private WeaponController weaponControlMech;
        [SerializeField] private SlowMotion slowMotionMech;
        [SerializeField] private AI_Enemy enemy;
        [HideInInspector] public bool canCount;   

       
        // Start is called before the first frame update
        void Start(){
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
            countDownText.gameObject.SetActive(true);
            warningMessage.gameObject.SetActive(true);
        
            currentTime -= 1 * Time.deltaTime;
            countDownText.text = currentTime.ToString();
            
            
            if(currentTime <= 0){
                warningMessage.text = "Atire";
                currentTime = 0;
                startDuel = true;
                countDownText.gameObject.SetActive(false);
                warningMessage.gameObject.SetActive(false);
                slowMotionMech.enabled = true;
                player.GetComponent<FirstPersonController>().canMove = true;
                player.GetComponent<FirstPersonController>().duelMode = false;
                leaningMech.enabled = true;
                crouchMech.enabled = true;
                weaponControlMech.duelMode = false;

                enemy.GetComponent<AI_Enemy>().nav.enabled = true;
                enemy.duelingMode = false;
                canCount = false;
            }
        }

    }
