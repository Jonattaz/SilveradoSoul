using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandgunFire : MonoBehaviour
{
   [SerializeField] private GameObject theGun;
   [SerializeField] private GameObject muzzleFlash;
   [SerializeField] private AudioSource gunFire;
   [SerializeField] private bool isFiring = false;
   [SerializeField] private AudioSource emptyAmmoSound;

    // Update is called once per frame
    void Update(){
        if(Input.GetButtonDown("Fire1")){
            if(GlobalAmmo.handgunAmmo < 1){
                emptyAmmoSound.Play();
            }else{
                if(!isFiring)
                    StartCoroutine(FiringHandgun());
            }
            
        }
    }

    IEnumerator FiringHandgun(){
        isFiring = true;
        GlobalAmmo.handgunAmmo -= 1;
        theGun.GetComponent<Animator>().Play("handgunFire");
        muzzleFlash.SetActive(true);
        gunFire.Play();
        yield return new WaitForSeconds(0.05f);
        muzzleFlash.SetActive(false);
        yield return new WaitForSeconds(0.25f);
        theGun.GetComponent<Animator>().Play("Default");
        isFiring = false;

    }

}
