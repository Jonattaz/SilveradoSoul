using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SlowMotion : MonoBehaviour
{
    [SerializeField] private float slowMotionTimeScale;
    [SerializeField] private bool bulletTimeControl;
    [SerializeField] private float currentTime = 0f;
    [SerializeField] private float startingTime = 1f;
    [SerializeField] private bool canActivateSlowMotion;
    [SerializeField] private UIPlayerSlowMotion slowMotionBar;
    [HideInInspector] public bool menuOn;

    private float startTimeScale;
    private float startFixedDeltaTime;

    // Start is called before the first frame update
    void Start(){
            startTimeScale = Time.timeScale;
            startFixedDeltaTime = Time.fixedDeltaTime;
    }

    // Update is called once per frame
    void Update(){
            SlowMotionController();
        
    }

    private void SlowMotionController(){
        
        
            if(Input.GetKeyDown(KeyCode.C)){
                bulletTimeControl = !bulletTimeControl;
            }
            
            if(bulletTimeControl){
                StartSlowMotion();
            }else{
                StopSlowMotion();
            }
        
    }

    private void StartSlowMotion(){
        Time.timeScale = slowMotionTimeScale;
        Time.fixedDeltaTime = startFixedDeltaTime * slowMotionTimeScale;
        CountDownController();
    }

    private void StopSlowMotion(){
        Time.timeScale = startTimeScale;
        Time.fixedDeltaTime = startFixedDeltaTime;
        StartCoroutine(RechargeSlowMotion());
    }

     void CountDownController(){
            currentTime -= 1 * Time.deltaTime;
            slowMotionBar.SetHealthBarPercentage(currentTime / startingTime);
            if(currentTime <= 0){
                currentTime = 0;
                bulletTimeControl = false;
            }
        }
        IEnumerator RechargeSlowMotion(){
            for(float currentRecharge = currentTime; currentRecharge <= startingTime; currentRecharge += 0.1f){
                currentTime = currentRecharge;
                slowMotionBar.SetHealthBarPercentage(currentTime / startingTime);
                yield return new WaitForSeconds (Time.deltaTime);
            }
            currentTime = startingTime;
        }
}
