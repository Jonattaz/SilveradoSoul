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
   private bool isAiming = false;

   [Header("Bullet Properties")]
   [SerializeField] private Transform bulletSpawnPoint;
   [SerializeField] private GameObject bulletPrefab;
   [SerializeField] private float bulletSpeed;

    // Update is called once per frame
    void Update(){
        RifleInput();
    }

    IEnumerator FiringRifle(){
        isFiring = true;
        GlobalAmmo.ammo -= 1;
        theGun.GetComponent<Animator>().Play("rifleFire", -1, 0f);
        muzzleFlash.SetActive(true); 
        gunFire.Play();
        var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * bulletSpeed;
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
        
        // Para testar o riffle usando apenas teclas
        /* #if UNITY_EDITOR
            if(Input.GetKeyDown(KeyCode.K) && isAiming){
                if(GlobalAmmo.ammo < 1){
                    theGun.GetComponent<Animator>().Play("Default");
                    isAiming = false;
                    emptyAmmoSound.Play();

                    StartCoroutine(ReloadingRifle());
                }else{
                    if(!isFiring){
                        StartCoroutine(FiringRifle());
                    } 
                }    
            }

            if(Input.GetKey(KeyCode.J)){
                theGun.GetComponent<Animator>().Play("rifleAim");
                isAiming = true;
            }else{
                theGun.GetComponent<Animator>().Play("Default");
                isAiming = false;
            }
       
        #endif */

        if(Input.GetButtonDown("Fire1") && isAiming){
                if(GlobalAmmo.ammo < 1){
                    theGun.GetComponent<Animator>().Play("Default");
                    isAiming = false;
                    emptyAmmoSound.Play();

                    StartCoroutine(ReloadingRifle());
                }else{
                    if(!isFiring){
                        StartCoroutine(FiringRifle());
                    } 
                }    
            }

        if(Input.GetButton("Fire2")){
            theGun.GetComponent<Animator>().Play("rifleAim");
            isAiming = true;
        }else{
            theGun.GetComponent<Animator>().Play("Default");
            isAiming = false;
        }
    }
}
