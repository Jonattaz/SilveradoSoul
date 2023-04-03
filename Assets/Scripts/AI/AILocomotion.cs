using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AILocomotion : MonoBehaviour
{
    NavMeshAgent agent;
    public float maxTime;
    public float maxDistance;
    public Transform playerTransform;
    Animator animator;
    float timer = 0.0f;

    // Start is called before the first frame update
    void Start(){
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update(){
        timer -= Time.deltaTime;
        if(timer < 0.0f){
            float sqDistance = (playerTransform.position - agent.destination).magnitude;
            if(sqDistance > maxDistance * maxDistance){
                agent.destination= playerTransform.position;

            }
            timer = maxTime;
        }
        animator.SetFloat("Speed", agent.velocity.magnitude);   
    }
}
