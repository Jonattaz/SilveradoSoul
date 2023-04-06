using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class AI_Enemy : MonoBehaviour{
           
    [Header("Movement Settings")]
    [SerializeField] private float normalSpeed;
    [SerializeField] float idleSpeed;
    [SerializeField] private float runningSpeed;

    [SerializeField] private float waitTimeAtWaypoint;
    [SerializeField] private bool canMove;
    [SerializeField] private Transform[] patrolSpots;
    private float waitTime;
    float distance;
    private int randomSpotNumber;

    [Header("General Settings")]
    [SerializeField] private int currentHealth = 50;
    [SerializeField] private bool isShooting; 
    [SerializeField] private Text stateObject;
    [SerializeField] private Transform playerPositionReference;
    NavMeshAgent nav;
    Animator anim;

    [Header("AI Sight Settings")]
    [SerializeField] private float fieldOfViewAngle;
    // Line Of Sight(LOS)
    [SerializeField] private float lOSRadius;
     [SerializeField] private bool playerIsInLOS = false;

    [Header("Shooting Settings")]
    [SerializeField] private AudioSource gunFire;
    [SerializeField] private string hitTag;
    [SerializeField] private Camera shootingRaycastArea;
    [SerializeField] private float shootingDistance;
    private RaycastHit hit;


    /// Awake is called when the script instance is being loaded.
    void Awake(){
        nav = GetComponent<NavMeshAgent>();
        nav.enabled = true;
        anim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start(){
        waitTime = waitTimeAtWaypoint;
        randomSpotNumber = Random.Range(0, patrolSpots.Length);
        isShooting = false;

    }

        // Update is called once per frame
    void Update(){
        distance = Vector3.Distance(playerPositionReference.position, transform.position);
        anim.SetFloat("Speed", nav.speed);
                        
        if(nav.isActiveAndEnabled){
            CheckLOS();
            if(!isShooting && canMove){
                if(playerIsInLOS == false){
                    Patrol();
                }else if(playerIsInLOS == true){
                    ChasePlayer();
                }else if(playerIsInLOS == true){
                    ChasePlayer();
                }
            }else{
                ShootPlayer();
            }
        }
    }
    
    void StateController(){
        
    }

    // Check Line Of Sight
    void CheckLOS(){
        Vector3 direction = playerPositionReference.position - transform.position;
        float angle = Vector3.Angle(direction, transform.forward);
        if(angle < fieldOfViewAngle * 0.5f){
            RaycastHit hit;
            if(Physics.Raycast(transform.position, direction.normalized, out hit, lOSRadius)){
                if(hit.collider.tag == "Player"){
                    playerIsInLOS = true;
                }else{
                    playerIsInLOS = false;
                    isShooting = false;
                    canMove = true;
                }
            }else{
                    playerIsInLOS = false;
                    isShooting = false;
                    canMove = true;
                }
        }
    }

    void Patrol(){
            stateObject.text = "Patroling";
            Vector3 LookAtPos = new Vector3(patrolSpots[randomSpotNumber].position.x,transform.position.y,patrolSpots[randomSpotNumber].position.z);
            transform.LookAt(LookAtPos);
            nav.speed = normalSpeed;
            nav.SetDestination(patrolSpots[randomSpotNumber].position);
                    
            if(Vector3.Distance(transform.position, patrolSpots[randomSpotNumber].position) < 2.0f){
                if(waitTime <= 0){
                    nav.speed = normalSpeed;
                    randomSpotNumber = Random.Range(0, patrolSpots.Length);
                    waitTime = waitTimeAtWaypoint;
                }else{
                    nav.speed = idleSpeed;
                    waitTime -= Time.deltaTime; 
                }
            }        
    }

    void ChasePlayer(){            
        if(distance > shootingDistance){ 
            canMove = true;
            isShooting = false;
            stateObject.text = "Chase Mode";
            nav.speed = runningSpeed;
            nav.destination = playerPositionReference.position;
        }else{
            stateObject.text = "Shooting";
            nav.speed = idleSpeed;
            isShooting = true;
            canMove = false; 
            transform.LookAt(playerPositionReference);
            nav.SetDestination(transform.position);
        }
    }

    private void ShootPlayer(){ 
        if(Physics.Raycast(shootingRaycastArea.transform.position, shootingRaycastArea.transform.forward, out hit, shootingDistance)){
            hitTag = hit.transform.name;
            Debug.Log("Acertou " + hitTag);

            if(hit.collider.gameObject.tag == "Player"){
                //anim.Play("Shooting");
                Debug.Log("Shooting Player");
            }
        }
    }

    public void TakeDamage(int damage){
        currentHealth -= damage;
        if(currentHealth <= 0){
            stateObject.text = "Dead";
            canMove = false;
            nav.speed = idleSpeed;
        }
    }
}

























