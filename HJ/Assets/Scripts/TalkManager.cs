using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalkManager : MonoBehaviour
{
    [SerializeField] GameObject TalkUI;
    [SerializeField] Chapter1Manager ChapterManager;
    [SerializeField] string[] Talker;
    List<DialogueContent> Queue = new List<DialogueContent>();
    IEnumerator Process;

    public void Talk(int TalkingPerson, string Dialogues, bool start, bool end, float endTime0, float DialogueSpeed, float PeriodSpeed, float CommaSpeed, float GapSpeed1, float GapSpeed2){
        DialogueContent Content;
        Content.Talker = TalkingPerson;
        Content.Dialogue = Dialogues;
        Content.starter = start;
        Content.ender = end;
        Content.endTime = endTime0;
        Content.Wait = false;
        Content.Event = false;
        Content.EventNumber = 0;
        Content.DialogueSpeed = DialogueSpeed;
        Content.PeriodSpeed = PeriodSpeed;
        Content.CommaSpeed = CommaSpeed;
        Content.GapSpeed1 = GapSpeed1;
        Content.GapSpeed2 = GapSpeed2;
        Queue.Insert(Queue.Count, Content);
    }
    public void Event(int EventCode, float endTime0){
        DialogueContent Content;
        Content.Talker = 0;
        Content.Dialogue = "Error";
        Content.starter = false;
        Content.ender = false;
        Content.Wait = false;
        Content.endTime = endTime0;
        Content.Event = true;
        Content.EventNumber = EventCode;
        Content.DialogueSpeed = 0.0f;
        Content.PeriodSpeed = 0.0f;
        Content.CommaSpeed = 0.0f;
        Content.GapSpeed1 = 0.0f;
        Content.GapSpeed2 = 0.0f;
        Queue.Insert(Queue.Count, Content);
    }
    public void Wait(float time){
        DialogueContent Content;
        Content.Talker = 0;
        Content.Dialogue = "Error";
        Content.starter = false;
        Content.ender = false;
        Content.Wait = true;
        Content.endTime = time;
        Content.Event = false;
        Content.EventNumber = 0;
        Content.DialogueSpeed = 0.0f;
        Content.PeriodSpeed = 0.0f;
        Content.CommaSpeed = 0.0f;
        Content.GapSpeed1 = 0.0f;
        Content.GapSpeed2 = 0.0f;
        Queue.Insert(Queue.Count, Content);
    }
    IEnumerator TextAdjust(bool start, bool end, int TalkingPerson, string Dialogue, float endTime, float DialogueSpd, float PeriodSpeed, float CommaSpeed, float GapSpeed1, float GapSpeed2){
        TalkUI.transform.GetChild(2).GetComponent<Text>().text = " ";
        if(start){
            TalkUI.transform.GetChild(1).GetComponent<Text>().text = Talker[TalkingPerson];
            TalkUI.SetActive(true);
            float value = 14.0f;
            for(int i = 0 ; i < 50 ; i++){
                TalkUI.GetComponent<RectTransform>().localPosition = new Vector3(0, TalkUI.GetComponent<RectTransform>().localPosition.y + value, 0);
                yield return new WaitForSeconds(0.01f);
                value -= 0.28f;
            }
        }
        for(int i = 0 ; i < Dialogue.Length ; i++){
            if(Dialogue[i] != '_' || Dialogue[i] != '=') TalkUI.transform.GetChild(2).GetComponent<Text>().text += Dialogue[i];
            if(Dialogue[i] == '.'){
                yield return new WaitForSeconds(PeriodSpeed);
            }
            if(Dialogue[i] == ','){
                yield return new WaitForSeconds(CommaSpeed);
            }
            if(Dialogue[i] == '_'){
                yield return new WaitForSeconds(GapSpeed1);
            }
            if(Dialogue[i] == '='){
                yield return new WaitForSeconds(GapSpeed2);
            }
            else yield return new WaitForSeconds(DialogueSpd);
        }
        yield return new WaitForSeconds(endTime);
        if(end){
            float value = 0.28f;
            for(int i = 0 ; i < 50 ; i++){
                TalkUI.GetComponent<RectTransform>().localPosition = new Vector3(0, TalkUI.GetComponent<RectTransform>().localPosition.y - value, 0);
                yield return new WaitForSeconds(0.01f);
                value += 0.28f;
            }
            TalkUI.SetActive(false);
        }
        StartCoroutine(Next(false));
    }

       public IEnumerator Next(bool start){
        if(start == false) Queue.Remove(Queue[0]);
        if(Queue.Count != 0){
            if(Queue[0].Wait == true){
                yield return new WaitForSeconds(Queue[0].endTime);
                StartCoroutine(Next(false));
            }
            else if(Queue[0].Event == true){
                ChapterManager.Event(Queue[0].EventNumber);
                Debug.Log("WORKS_____________________________");
                yield return new WaitForSeconds(Queue[0].endTime);
                StartCoroutine(Next(false));
            }
            else{
                StartCoroutine(TextAdjust(Queue[0].starter, Queue[0].ender, Queue[0].Talker, Queue[0].Dialogue, Queue[0].endTime, Queue[0].DialogueSpeed, Queue[0].PeriodSpeed, Queue[0].CommaSpeed, Queue[0].GapSpeed1, Queue[0].GapSpeed2));
            }
        }
    }
}
struct DialogueContent{
    public int Talker;
    public string Dialogue;
    public float DialogueSpeed;
    public bool starter;
    public bool ender;
    public float endTime;
    public bool Wait;
    public bool Event;
    public int EventNumber;
    public float PeriodSpeed;
    public float CommaSpeed;
    public float GapSpeed1;
    public float GapSpeed2;
}