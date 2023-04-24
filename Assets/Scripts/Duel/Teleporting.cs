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
    [SerializeField] private GameObject enemyDecoy;
    [SerializeField] private CountDownTimer duelCounter;
    [SerializeField] public bool canTeleport;
    [SerializeField] private BoxCollider boxCollider;
    [SerializeField] private GameObject dialog;
    

    void OnTriggerStay(Collider other){
        
        if(canTeleport){
            weaponControlMech.duelMode = true;
            CameraFade.FadeInstance.Fade();
            slowMotionMech.enabled = false;
            player.GetComponent<FirstPersonController>().canMove = false;
            player.GetComponent<FirstPersonController>().duelMode = true;
            leaningMech.enabled = false;
            crouchMech.enabled = false;

            enemy.gameObject.SetActive(true);
            enemy.GetComponent<AI_Enemy>().duelingMode = true;
            enemy.GetComponent<AI_Enemy>().nav.speed = 0;
            enemy.GetComponent<AI_Enemy>().nav.enabled = false;
            StartCoroutine(Teleport());

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

        dialog.SetActive(false);
        enemyDecoy.SetActive(false);
        duelCounter.canCount = true;
        boxCollider.enabled = false;        
        
    }
}
