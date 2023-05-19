using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerQuests : MonoBehaviour
{
    public Quest[] quests;
    public bool[] questResult;

    public GameObject[] questsObjects;

    // Update is called once per frame
    void Update(){
        if(questResult[0] && !questResult[1]){
            questsObjects[0].SetActive(false);
            questsObjects[1].SetActive(true);
        }else if(questResult[1] && questResult[0]){
           questsObjects[1].SetActive(false);
           questsObjects[2].SetActive(true);
        }
    }
}