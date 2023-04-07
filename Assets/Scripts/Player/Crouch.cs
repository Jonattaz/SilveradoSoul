using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Crouch : MonoBehaviour
{   
   [SerializeField] CharacterController characterController;
   
    // Start is called before the first frame update
    void Start(){
        characterController = characterController.gameObject.GetComponent<CharacterController>();

    }

    // Update is called once per frame
    void Update(){
        if(Input.GetKey(KeyCode.C)){
            characterController.height = 1.0f;
        }else{
            characterController.height = 1.8f;
        }
    }
}
