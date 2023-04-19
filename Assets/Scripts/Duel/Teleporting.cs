using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;

public class Teleporting : MonoBehaviour
{
    [SerializeField] private Transform playerTeleportTarget;
    [SerializeField] private Transform enemyTeleportTarget;
    [SerializeField] private FirstPersonController player;
    [SerializeField] private LeaningController leaningMech;
    [SerializeField] private SlowMotion slowMotionMech;
    [SerializeField] private Crouch crouchMech;
    [SerializeField] private WeaponController weaponControlMech;
    [SerializeField] private AI_Enemy enemy;
    [SerializeField] private CountDownTimer duelCounter;
    [SerializeField] private bool canTeleport;
    [SerializeField] private BoxCollider boxCollider;
    

    void OnTriggerStay(Collider other){
        if(Input.GetKeyDown(KeyCode.R)){
            canTeleport = true;
        }
        
        if(canTeleport){
            StartCoroutine(Teleport());
            weaponControlMech.duelMode = true;
            CameraFade.FadeInstance.Fade();
            slowMotionMech.enabled = false;
            player.GetComponent<FirstPersonController>().canMove = false;
            player.GetComponent<FirstPersonController>().duelMode = true;
            leaningMech.enabled = false;
            crouchMech.enabled = false;            
            enemy.GetComponent<AI_Enemy>().nav.speed = 0;
            enemy.GetComponent<AI_Enemy>().duelingMode = true;
            enemy.GetComponent<AI_Enemy>().nav.enabled = false;
        }                 
    }
  

    IEnumerator Teleport(){
        yield return new WaitForSeconds(1);
     
        player.transform.position = new Vector3(
        playerTeleportTarget.transform.position.x,
        playerTeleportTarget.transform.position.y,
        playerTeleportTarget.transform.position.z);

        enemy.transform.position = new Vector3(
        enemyTeleportTarget.transform.position.x,
        enemyTeleportTarget.transform.position.y,
        enemyTeleportTarget.transform.position.z);

        
        duelCounter.canCount = true;
        boxCollider.enabled = false;        
    }
            
    
}
