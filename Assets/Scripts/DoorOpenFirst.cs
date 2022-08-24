using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpenFirst : MonoBehaviour
{
   [SerializeField] 
   GameObject theDoor;

   void OnTriggerEnter(Collider other)
   {
        theDoor.GetComponent<Animator>().Play("doorOpen");    
   }
}
