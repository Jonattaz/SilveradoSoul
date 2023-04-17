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
    [SerializeField] private GameObject impactEffect;
    [SerializeField] private int rifleDamage = 30;
    [SerializeField] private TrailRenderer bulletTrail;

    private RaycastHit hit;
    private Ray ray;

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
       
        if(Physics.Raycast(ray, out hit, Mathf.Infinity)){
            TrailRenderer trail = Instantiate(bulletTrail, transform.position, Quaternion.identity);
            StartCoroutine(SpawnTrail(trail, hit));
            
            GameObject impactEffectGO = Instantiate(impactEffect, hit.point, Quaternion.identity) as GameObject;
             Destroy(impactEffectGO, 3);
            if(hit.collider.gameObject.tag == "Enemy"){
                AI_Enemy enemy = hit.collider.gameObject.GetComponent<AI_Enemy>();
                enemy.TakeDamage(rifleDamage);
                // Tocar som de acerto
            }
        }

        yield return new WaitForSeconds(0.05f);
        muzzleFlash.SetActive(false);
        yield return new WaitForSeconds(0.25f);
        isFiring = false;

    }

     private IEnumerator SpawnTrail(TrailRenderer Trail, RaycastHit Hit){
        float time = 0;
        Vector3 startPosition = Trail.transform.position;

        while (time < 1){
            Trail.transform.position = Vector3.Lerp(startPosition, Hit.point, time);
            time += Time.deltaTime / Trail.time;

            yield return null;
        }

        Trail.transform.position = Hit.point;
        Destroy(Trail.gameObject, Trail.time);
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
