using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandgunPickup : MonoBehaviour
{
    [SerializeField]
    GameObject realHandgun;
    [SerializeField]
    GameObject fakeHandgun;
    [SerializeField]
    AudioSource handgunPickupSound;
    [SerializeField]
    GameObject pickupDisplay;
    public static bool handgunCollected;
    
    void OnTriggerEnter(Collider other){
        handgunCollected = true;
       
        if(WeaponController.rifleEquiped){
            fakeHandgun.SetActive(false);
            handgunPickupSound.Play();
            GetComponent<BoxCollider>().enabled = false;
            pickupDisplay.SetActive(false);
            pickupDisplay.GetComponent<Text>().text = "BERETTA M9";
            pickupDisplay.SetActive(true);

        
        }else{
            WeaponController.handgunEquiped = true;
            realHandgun.SetActive(true);
            fakeHandgun.SetActive(false);
            handgunPickupSound.Play();
            GetComponent<BoxCollider>().enabled = false;
            pickupDisplay.SetActive(false);
            pickupDisplay.GetComponent<Text>().text = "BERETTA M9";
            pickupDisplay.SetActive(true);
        }
           
            
    }
}
