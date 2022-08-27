using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandgunPickup : MonoBehaviour
{
    [SerializeField]
    GameObject realHandgun;
    [SerializeField]
    GameObject fakeHandgun;
    [SerializeField]
    AudioSource handgunPickupSound;

    void OnTriggerEnter(Collider other){
        realHandgun.SetActive(true);
        fakeHandgun.SetActive(false);
        handgunPickupSound.Play();
    }
}
