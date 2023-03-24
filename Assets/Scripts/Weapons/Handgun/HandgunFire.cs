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
        HandgunInput();
    }

    IEnumerator FiringHandgun(){
        isFiring = true;
        GlobalAmmo.ammo -= 1;
        theGun.GetComponent<Animator>().Play("handgunFire", -1, 0f);
        muzzleFlash.SetActive(true); 
        gunFire.Play();
        yield return new WaitForSeconds(0.05f);
        muzzleFlash.SetActive(false);
        yield return new WaitForSeconds(0.25f);
        isFiring = false;

    }

     IEnumerator ReloadingHandgun(){
        yield return new WaitForSeconds(0.05f);
        // Animação recarregando
        yield return new WaitForSeconds(0.25f);
        GlobalAmmo.ammo = 10;
    }

    private void HandgunInput(){
        
        // Para testar o tiro usando apenas teclas 
        /* #if UNITY_EDITOR
           if(Input.GetKeyDown(KeyCode.K)){
                if(GlobalAmmo.ammo < 1){
                    theGun.GetComponent<Animator>().Play("Default");
                    emptyAmmoSound.Play();
                    StartCoroutine(ReloadingHandgun());
                }else{
                    if(!isFiring){
                        StartCoroutine(FiringHandgun());
                    } 
                }    
            }

            if(Input.GetKey(KeyCode.J)){
                theGun.GetComponent<Animator>().Play("handgunAim");
            }else{
                theGun.GetComponent<Animator>().Play("Default");
            }
        #endif */


       if(Input.GetButtonDown("Fire1")){
            if(GlobalAmmo.ammo < 1){
                theGun.GetComponent<Animator>().Play("Default");
                emptyAmmoSound.Play();
                StartCoroutine(ReloadingHandgun());
            }else{
                if(!isFiring){
                    StartCoroutine(FiringHandgun());
                } 
            }    
        }

        if(Input.GetButton("Fire2")){
            theGun.GetComponent<Animator>().Play("handgunAim");
        }else{
            theGun.GetComponent<Animator>().Play("Default");
        }
    }
}
