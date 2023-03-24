using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifleFire : MonoBehaviour
{
   [SerializeField] private GameObject theGun;
   [SerializeField] private GameObject muzzleFlash;
   [SerializeField] private AudioSource gunFire;
   [SerializeField] private bool isFiring = false;
   [SerializeField] private AudioSource emptyAmmoSound;

    // Update is called once per frame
    void Update(){
        RifleInput();
    }

    IEnumerator FiringRifle(){
        isFiring = true;
        GlobalAmmo.ammo -= 1;
        //theGun.GetComponent<Animator>().Play("rifleFire", -1, 0f);
        muzzleFlash.SetActive(true); 
        gunFire.Play();
        yield return new WaitForSeconds(0.05f);
        muzzleFlash.SetActive(false);
        yield return new WaitForSeconds(0.25f);
        isFiring = false;

    }

    IEnumerator ReloadingRifle(){
        yield return new WaitForSeconds(0.10f);
        // Animação recarregando
        yield return new WaitForSeconds(1f);
        GlobalAmmo.ammo = 10;
    }

    private void RifleInput(){
        if(Input.GetButtonDown("Fire1")){
            if(GlobalAmmo.ammo < 1){
                //theGun.GetComponent<Animator>().Play("Default");
                emptyAmmoSound.Play();
                StartCoroutine(ReloadingRifle());
            }else{
                if(!isFiring){
                    StartCoroutine(FiringRifle());
                } 
            }    
        }

        if(Input.GetButton("Fire2")){
            //theGun.GetComponent<Animator>().Play("rifleAim");
        }else{
            //theGun.GetComponent<Animator>().Play("Default");
        }
    }
}
