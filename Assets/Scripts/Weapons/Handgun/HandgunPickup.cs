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

    void OnTriggerEnter(Collider other){
        realHandgun.SetActive(true);
        fakeHandgun.SetActive(false);
        handgunPickupSound.Play();
        GetComponent<BoxCollider>().enabled = false;
        pickupDisplay.SetActive(false);
        pickupDisplay.GetComponent<Text>().text = "HANDGUN";
        pickupDisplay.SetActive(true);
    }
}
