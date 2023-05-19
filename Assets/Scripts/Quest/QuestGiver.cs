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

    [HideInInspector]
    public bool localActive;

    public int questIndex;
    
    public GameObject enemies;

    private void Awake() {
        instance = this;
    }

    /// Update is called every frame, if the MonoBehaviour is enabled.
    void Update(){
        if(canSpawnEnemies)
            enemies.SetActive(true);
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
            CompleteQuest();
        }
    }

    public void CompleteQuest(){ 
        Debug.Log("Ué");
        questWindow.SetActive(false);
        quest.isActive = false;
        title.text = "";
        description.text = "";
        localActive = quest.isActive;
        Destroy(this.gameObject);
    }
}