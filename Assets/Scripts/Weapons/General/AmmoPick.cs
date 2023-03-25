using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoPick : MonoBehaviour
{
   
    [SerializeField]
    GameObject fakeAmmoClip;
    [SerializeField]
    AudioSource ammoPickupSound;
    [SerializeField]
    private GameObject pickupDisplay;

    void OnTriggerEnter(Collider other){
        
        fakeAmmoClip.SetActive(false);
        ammoPickupSound.Play();
        GlobalAmmo.ammo += 10;
        pickupDisplay.SetActive(false);
        pickupDisplay.GetComponent<Text>().text = "CLIPE DE BALAS";
        pickupDisplay.SetActive(true);
    }


}
