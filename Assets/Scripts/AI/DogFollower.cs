using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DogFollower : MonoBehaviour
{
    [SerializeField] private NavMeshAgent dogNav;
    [SerializeField] private float dogSpeed;
    [SerializeField] private Transform player;
    [SerializeField] private Animator dogAnim;

    /// Awake is called when the script instance is being loaded.
    void Awake(){
        dogNav =  GetComponent<NavMeshAgent>();
        dogNav.enabled = true;
        dogAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update(){
        dogNav.SetDestination(player.position);
        dogNav.speed = dogSpeed;
        //dogAnim.SetFloat("Speed",dogSpeed);
    }
}
