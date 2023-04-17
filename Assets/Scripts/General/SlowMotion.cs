using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMotion : MonoBehaviour
{
    [SerializeField] private float slowMotionTimeScale;
    [SerializeField] private bool bulletTimeControl;
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
        if(Input.GetKeyDown(KeyCode.F)){
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
    }

    private void StopSlowMotion(){
        Time.timeScale = startTimeScale;
        Time.fixedDeltaTime = startFixedDeltaTime;
    }
}
