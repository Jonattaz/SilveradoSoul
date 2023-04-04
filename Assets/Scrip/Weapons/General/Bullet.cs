using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float life = 3;
    public float damage = 50;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake(){
        Destroy(gameObject, life);
        
    } 

    /// <summary>
    /// OnCollisionEnter is called when this collider/rigidbody has begun
    /// touching another rigidbody/collider.
    /// </summary>
    void OnCollisionEnter(Collision other){

      // Destroy(gameObject);

    }

}
