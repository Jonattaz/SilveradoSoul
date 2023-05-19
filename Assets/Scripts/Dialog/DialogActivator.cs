using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogActivator : MonoBehaviour
{
    [SerializeField] private GameObject DialogDisplay;
    [SerializeField] private PlayerQuests playerQuests;
    [SerializeField] private int questNumber;
    [SerializeField] private bool questDialog;

    /// OnTriggerStay is called once per frame for every Collider other that is touching the trigger.
    void OnTriggerStay(Collider other){
        if(other.gameObject.tag == "Player")
            DialogDisplay.SetActive(true);
    }

    /// OnTriggerExit is called when the Collider other has stopped touching the trigger.
    void OnTriggerExit(Collider other){
            DialogDisplay.SetActive(false);
    }
}
