using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RiflePickup : MonoBehaviour
{
    [SerializeField]
    GameObject realRifle;
    [SerializeField]
    GameObject fakeRifle;
    [SerializeField]
    AudioSource riflePickupSound;
    [SerializeField]
    GameObject pickupDisplay;

    public static bool rifleCollected;
  

    void OnTriggerEnter(Collider other){
        rifleCollected = true;
        
        if(WeaponController.handgunEquiped){
            fakeRifle.SetActive(false);
            riflePickupSound.Play();
            GetComponent<BoxCollider>().enabled = false;
            pickupDisplay.SetActive(false);
            pickupDisplay.GetComponent<Text>().text = "RIFLE";
            pickupDisplay.SetActive(true);    

        }else{
            WeaponController.rifleEquiped = true;        
            realRifle.SetActive(true);
            fakeRifle.SetActive(false);
            riflePickupSound.Play();
            GetComponent<BoxCollider>().enabled = false;
            pickupDisplay.SetActive(false);
            pickupDisplay.GetComponent<Text>().text = "RIFLE";
            pickupDisplay.SetActive(true);
        }
      
    }
}
