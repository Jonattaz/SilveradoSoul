using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Crouch : MonoBehaviour
{   
    CharacterController characterController;

    // Start is called before the first frame update
    void Start(){
        characterController = gameObject.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update(){
        if(Input.GetKey(KeyCode.C)){
            characterController.height = 1.0f;
        }else{
            characterController.height = 1.8f;
        }

         if(Input.GetKey(KeyCode.Escape)){
            Debug.Log("Saiu");
            SceneManager.LoadScene("Menu");
        } 
    }
}
