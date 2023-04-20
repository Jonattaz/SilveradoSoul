using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

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
    [SerializeField] private float currentHealth;
    [SerializeField] public float maxHealth = 50;
    [SerializeField] private bool isShooting; 
    [SerializeField] private Text stateObject;
    [SerializeField] private Transform playerPositionReference;
    [SerializeField] private FirstPersonController character;
    [HideInInspector] public NavMeshAgent nav;
    Animator anim;
    UIHealthBar healthBar;

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
    [SerializeField] private float fireRate = 1.5f;
    [SerializeField] private int gunDamage = 10;
    [SerializeField] private int randomNumber;
    [SerializeField] private TrailRenderer bulletTrail;
    [HideInInspector] public bool duelingMode;
    [SerializeField] private float currentReactionTime = 0f;
    [SerializeField] private float reactionTime = 5f;
    [SerializeField] private bool duelEnemy;
    [HideInInspector] public bool canCount;   
    [SerializeField] private bool canShot;
    [SerializeField] private int maxIndexShoot;
    [SerializeField] private int indexShoot;
    public bool isFiring;
    private RaycastHit hit;


    /// Awake is called when the script instance is being loaded.
    void Awake(){
        nav = GetComponent<NavMeshAgent>();
        nav.enabled = true;
        anim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start(){
        currentReactionTime = reactionTime;
        currentHealth = maxHealth;
        waitTime = waitTimeAtWaypoint;
        randomSpotNumber = Random.Range(0, patrolSpots.Length);
        isShooting = false;
        healthBar = GetComponentInChildren<UIHealthBar>();
    }

        // Update is called once per frame
    void Update(){
        distance = Vector3.Distance(playerPositionReference.position, transform.position);
        
        if(duelingMode){
            transform.LookAt(playerPositionReference);
            gunDamage = 50;
        }

       
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
        

        if(duelEnemy){
            if(canCount){
                CountDownController();
            }else{
                currentReactionTime = reactionTime;
            }
        }else{
            canShot = true;
        }
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
                }
            }else{
                    playerIsInLOS = false;
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
        if(Physics.Raycast(shootingRaycastArea.transform.position, shootingRaycastArea.transform.forward, out hit, lOSRadius)){
            hitTag = hit.transform.tag;
        }

        if(hitTag == "Player" && !isFiring){        
            StartCoroutine(EnemyFire());

            if(indexShoot != maxIndexShoot){
                canCount = true;
            }
        }

        if(hitTag != "Player"){
            isFiring = false;
            nav.speed = idleSpeed;
            transform.LookAt(playerPositionReference);
        }
    }

    IEnumerator EnemyFire(){
        isFiring = true;
        randomNumber = Random.Range(0, 10);
        
        if(canShot){        
            if(randomNumber % 2 == 0){
                //Debug.Log("Shoot " + randomNumber);
                //Debug.Log("Shooting Player");
                transform.LookAt(playerPositionReference);
                nav.SetDestination(transform.position);
                anim.Play("Shooting", -1, 0f);
                TrailRenderer trail = Instantiate(bulletTrail, transform.position, Quaternion.identity);
                StartCoroutine(SpawnTrail(trail, hit));
                gunFire.Play();
                character.TakeDamage(gunDamage);
            }else{
                // Debug.Log("Miss " + randomNumber);
                // Debug.Log("Shoot " + randomNumber);
                // Debug.Log("Shooting Player");
                transform.LookAt(playerPositionReference);
                nav.SetDestination(transform.position);
                TrailRenderer trail = Instantiate(bulletTrail, transform.position, Quaternion.identity);
                StartCoroutine(SpawnTrail(trail, hit));
                anim.Play("Shooting", -1, 0f);
                gunFire.Play();
            } 
            
            indexShoot = maxIndexShoot;

        }
        yield return new WaitForSeconds(fireRate);
        isFiring = false;
        canCount = false;
        
    }

    void CountDownController(){
        //Debug.Log("Count down Mode");

        currentReactionTime -= 1 * Time.deltaTime;
            
            
        if(currentReactionTime <= 0){
            currentReactionTime = 0;
            canShot = true;
            canCount = false;
        }
    }

    public void TakeDamage(int damage){
        currentHealth -= damage;
        healthBar.SetHealthBarPercentage(currentHealth / maxHealth);
        if(currentHealth <= 0){
            anim.Play("Dying");
            stateObject.text = "Dead";
            healthBar.gameObject.SetActive(false);
            nav.speed = idleSpeed;
            nav.enabled = false;
            canMove = false;
            isShooting = false;       
        }
        
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
}

























