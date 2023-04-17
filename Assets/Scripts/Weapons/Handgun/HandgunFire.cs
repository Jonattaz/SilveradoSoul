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
    
    [Header("Bullet Properties")]
    [SerializeField] private GameObject impactEffect;
    [SerializeField] private int handgunDamage = 10;
    [SerializeField] private TrailRenderer bulletTrail;
    private RaycastHit hit;
    private Ray ray;



    // Update is called once per frame
    void Update(){
        ray = Camera.main.ViewportPointToRay(new Vector3(.5f, .5f, 0));
        HandgunInput();
    }

    IEnumerator FiringHandgun(){
        isFiring = true;
        GlobalAmmo.ammo -= 1;
        theGun.GetComponent<Animator>().Play("handgunFire", -1, 0f);
        
        if(Physics.Raycast(ray, out hit, Mathf.Infinity)){
            TrailRenderer trail = Instantiate(bulletTrail, transform.position, Quaternion.identity);
            StartCoroutine(SpawnTrail(trail, hit));

            GameObject impactEffectGO = Instantiate(impactEffect, hit.point, Quaternion.identity) as GameObject; 
            Destroy(impactEffectGO, 5);
            if(hit.collider.gameObject.tag == "Enemy"){
                AI_Enemy enemy = hit.collider.gameObject.GetComponent<AI_Enemy>();
                enemy.TakeDamage(handgunDamage);
            }
        }

        muzzleFlash.SetActive(true); 
        gunFire.Play();
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
