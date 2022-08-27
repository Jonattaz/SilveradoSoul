using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpenFirst : MonoBehaviour
{
   [SerializeField] 
   GameObject theDoor;

   [SerializeField]
   AudioSource doorFX;

   [SerializeField]
   int doorCloseTime = 0;

   void OnTriggerEnter(Collider other)
   {
      doorFX.Play();
      theDoor.GetComponent<Animator>().Play("doorOpen");    
      this.GetComponent<BoxCollider>().enabled = false;
      StartCoroutine(CloseDoor());
   }

   IEnumerator CloseDoor(){
      yield return new WaitForSeconds(doorCloseTime);
      doorFX.Play();
      theDoor.GetComponent<Animator>().Play("doorClose");
      this.GetComponent<BoxCollider>().enabled = true;

   }





}
