using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogActivator : MonoBehaviour
{
    [SerializeField] private GameObject DialogDisplay;

    /// OnTriggerStay is called once per frame for every Collider other that is touching the trigger.
    void OnTriggerStay(Collider other){
        DialogDisplay.SetActive(true);
    }

    /// OnTriggerExit is called when the Collider other has stopped touching the trigger.
    void OnTriggerExit(Collider other){
        DialogDisplay.SetActive(false);
    }
}
