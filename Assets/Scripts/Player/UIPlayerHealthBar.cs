using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class UIPlayerHealthBar : MonoBehaviour
{
    [SerializeField] private FirstPersonController character;
    [SerializeField] private Image foregroundImage;
    [SerializeField] private Image backgroundImage;
    [SerializeField] private AI_Enemy[] enemies;
    [SerializeField] private int enemyCounter = 0;
    [SerializeField] private bool healing = true;
    [SerializeField] private int index;
    
   

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update(){
         HealingController();
    }

    public void HealingController(){
        
        if(enemyCounter >= index && enemyCounter > enemies.Length){
            enemyCounter = index;
            healing = true;
        }else{
            healing = false;
        }

        if(healing){
            character.canHeal = true;
        }else{
            character.canHeal = false;
        }

        for (index = 0; index < enemies.Length; index++){
            if(!enemies[index].isFiring){
                enemyCounter++;
            }else if(enemies[index].isFiring){
                enemyCounter = 0;
                return;
            }   
        }


        
    }

    public void SetHealthBarPercentage(float percentage){
        float parentWidth =GetComponent<RectTransform>().rect.width;
        float width = parentWidth * percentage;
        foregroundImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
    }

}








 