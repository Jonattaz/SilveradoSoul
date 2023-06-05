using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestGiver : MonoBehaviour
{
    public Quest quest;
    public PlayerQuests playerQuests;

    public QuestGiver instance;
    public GameObject questWindow;
    public Text title;
    public Text description;
    public bool completed = false;
    public bool canSpawnEnemies;
    public bool deactivator;

    [HideInInspector]
    public bool localActive;

    public int questIndex;
    
    public GameObject enemies;
    
    private void Awake() {
        instance = this;
    }

    /// Update is called every frame, if the MonoBehaviour is enabled.
    void Update(){
        if(canSpawnEnemies){
            enemies.SetActive(true);
        }

        if(playerQuests.questResult[0] && !deactivator){
            CompleteFirstQuest();
        }

        if(playerQuests.questResult[1]){
            CompleteSecondQuest();
        }


    }

    public void OpenQuestWindow(){
        canSpawnEnemies = true;
        questWindow.SetActive(true);
        title.text = quest.title;
        description.text = quest.description;
        quest.isActive = true;
        localActive = quest.isActive;
        playerQuests.quests[questIndex] = quest;

         StartCoroutine(ItemQuestVerifier());
    }


    IEnumerator ItemQuestVerifier(){
       completed = true;
       yield return new WaitForSeconds(3);
        if(playerQuests.questResult[questIndex]){
            
        }
    }

    public void CompleteFirstQuest(){ 
        //questWindow.SetActive(false);
        //quest.isActive = false;
        description.text = "Retorne à cidade de Bodie e informe ao Stuart.";
        //localActive = quest.isActive;
        //Destroy(this.gameObject);
        deactivator = true;
    }

       public void CompleteSecondQuest(){ 
        questWindow.SetActive(false);
        //quest.isActive = false;
        description.text = "Converse com Stuart e prepare-se para fugir.";
        //localActive = quest.isActive;
        //Destroy(this.gameObject);
    }

}