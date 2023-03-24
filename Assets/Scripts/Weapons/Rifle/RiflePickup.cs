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

    void OnTriggerEnter(Collider other){
        realRifle.SetActive(true);
        fakeRifle.SetActive(false);
        riflePickupSound.Play();
        GetComponent<BoxCollider>().enabled = false;
        pickupDisplay.SetActive(false);
        pickupDisplay.GetComponent<Text>().text = "RIFLE";
        pickupDisplay.SetActive(true);
    }
}
