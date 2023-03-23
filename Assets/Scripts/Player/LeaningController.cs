using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaningController : MonoBehaviour
{
    [SerializeField] private Transform leanLeft;
    [SerializeField] private Transform leanRight;
    [SerializeField] private Transform idle;

    [SerializeField] private float lerpTime = 0.15f;

    // Update is called once per frame
    void Update(){
        LeaningInput();
    }

    private void LeaningInput(){
        if(Input.GetKey(KeyCode.Q)){
            
            transform.position = Vector3.Lerp(transform.position, leanLeft.position, lerpTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, leanLeft.rotation, lerpTime);

        }else{
            transform.position = Vector3.Lerp(transform.position, idle.position, lerpTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, idle.rotation, lerpTime);
        }

        if(Input.GetKey(KeyCode.E)){
    
            transform.position = Vector3.Lerp(transform.position, leanRight.position, lerpTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, leanRight.rotation, lerpTime);

        }else{
            transform.position = Vector3.Lerp(transform.position, idle.position, lerpTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, idle.rotation, lerpTime);
        }

    }

}
