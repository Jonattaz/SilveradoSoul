using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class DialogDisplay : MonoBehaviour
{
    public Conversation conversation;

    public GameObject speakerLeft;

    public GameObject speakerRight;
    public GameObject dialogPanel;
    [SerializeField] private bool duelDialog;
    [SerializeField] private bool questDialog;
    [SerializeField] private QuestGiver quest;
    [SerializeField] private Teleporting teleportObject;
    [SerializeField] private bool questStarted;
    [SerializeField] private bool finalDialog;
    [SerializeField] private string menuScene;


    private SpeakerUI speakerUILeft;

    private SpeakerUI speakerUIRight;

    private int activeLineIndex = 0;

    

    // Start is called before the first frame update
    void Start()
    {
        speakerUILeft = speakerLeft.GetComponent<SpeakerUI>();
        speakerUIRight = speakerRight.GetComponent<SpeakerUI>();

        speakerUILeft.Speaker = conversation.speakerLeft;
        speakerUIRight.Speaker = conversation.speakerRight;
    }

    // Update is called once per frame
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)) 
        {
            AdvanceConversation();
        }
           
    }


  public void AdvanceConversation() 
    {
        if (activeLineIndex < conversation.lines.Length) 
        {
            // Bool that inform if the player is talking = true
            DisplayLine();
            dialogPanel.SetActive(true);
            activeLineIndex += 1;
        }
        else
        {
            speakerUILeft.Hide();
            speakerUIRight.Hide();
            dialogPanel.SetActive(false);
            
            if(duelDialog){
                teleportObject.canTeleport = true;
            }

            if(questDialog){
                quest.OpenQuestWindow();
            }

            if(finalDialog){
                if(menuScene != ""){
                    Time.timeScale = 1;
				    SceneManager.LoadScene(menuScene, LoadSceneMode.Single);
			    }
            }


            activeLineIndex = 0;
             // Bool that inform if the player is talking = false
        }
    }

    void DisplayLine()
    {
        Line line = conversation.lines[activeLineIndex];
        Character character = line.character;

        if (speakerUILeft.SpeakerIs(character)) 
        {
            SetDialog(speakerUILeft, speakerUIRight, line.text);

        }
        else
        {
            SetDialog(speakerUIRight, speakerUILeft, line.text);
        }
    }

    void SetDialog
    (
        SpeakerUI activeSpeakerUI,
        SpeakerUI inactiveSpeakerUI,
        string text) 
    {
        activeSpeakerUI.Dialog = text;
        activeSpeakerUI.Show();

        inactiveSpeakerUI.Dialog = text;
        inactiveSpeakerUI.Hide();
    
    }
}
