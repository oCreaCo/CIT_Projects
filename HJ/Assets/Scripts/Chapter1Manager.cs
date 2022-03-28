using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Chapter1Manager : MonoBehaviour
{
    public static Chapter1Manager Manager;
    [SerializeField] TalkManager TalkManage;
    [SerializeField] Dialogues[] Conversation;
    [SerializeField] GameObject ResearchFacility;
    public Tl[] TargetList;

    void Start(){
        Chapter1Manager.Manager = this;
        for(int i = 0 ; i < Conversation.Length ; i++){
            if(Conversation[i].Wait == true){
                TalkManage.Wait(Conversation[i].TimeUntilNextDialogueOrEnd);
            }
            else if(Conversation[i].EventRelated.isEvent == true){
                TalkManage.Event(Conversation[i].EventRelated.EventCode, Conversation[i].TimeUntilNextDialogueOrEnd);
            }
            else TalkManage.Talk(Conversation[i].Talker, Conversation[i].Dialogue, Conversation[i].StartOfConversation, Conversation[i].EndOfConversation, Conversation[i].TimeUntilNextDialogueOrEnd, Conversation[i].DialogueSpeed, Conversation[i].Miscellaneous.PeriodPrintSpeed, Conversation[i].Miscellaneous.CommaPrintSpeed, Conversation[i].Miscellaneous.Gap1Speed, Conversation[i].Miscellaneous.Gap2Speed);
        }
        StartCoroutine(TalkManage.Next(true));
    }
    public void Event(int eventCode){
        if(eventCode == 0){
            ResearchFacility.SetActive(true);
        }
    }
}
[System.Serializable]struct Dialogues{
    public int Talker;
    public string Dialogue;
    public float DialogueSpeed;
    public bool StartOfConversation;
    public bool EndOfConversation;
    public float TimeUntilNextDialogueOrEnd;
    public bool Wait;
    public Event EventRelated;
    public others Miscellaneous;
}
[System.Serializable]struct Event{
    public bool isEvent;
    public int EventCode;   
}
[System.Serializable]class others{
    public float PeriodPrintSpeed = 0.5f;
    public float CommaPrintSpeed = 0.5f;
    public float Gap1Speed = 0.5f;
    //Gap 1: _
    //Gaps are not printed, they are just for making a time gap between words.
    public float Gap2Speed = 0.5f;
    //Gap 2: =

}
[System.Serializable] public struct Tl{
    public GameObject Target;
    public float TargetDistance;
}