using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cutscene1Manager : MonoBehaviour
{
    [SerializeField] Animator Anim;
    [SerializeField] Image Screen;
    [SerializeField] Text ScreenText;
    [SerializeField] float OpeningTime = 2.0f;
    [SerializeField] GameObject[] CutsceneObjects;
    [SerializeField] GameObject[] CutsceneEndObjects;
    float measure = 0.0f;
    void Awake(){
        for(int i = 0 ; i < CutsceneObjects.Length ; i++){
            CutsceneObjects[i].SetActive(true);
        }
        for(int i = 0 ; i < CutsceneEndObjects.Length ; i++){
            CutsceneEndObjects[i].SetActive(false);
        }
    }
    void Start(){
        StartCoroutine(StartCutscene());
    }
    
    public void CutsceneEnd(){
        for(int i = 0 ; i < CutsceneObjects.Length ; i++){
            CutsceneObjects[i].SetActive(false);
        }
        for(int i = 0 ; i < CutsceneEndObjects.Length ; i++){
            CutsceneEndObjects[i].SetActive(true);
        }
    }

    IEnumerator StartCutscene(){
        yield return new WaitForSeconds(OpeningTime);
        Anim.SetTrigger("StartCutscene");
        for(int i = 0 ; i < 100 ; i++){
            Screen.color = new Color(0, 0, 0, Screen.color.a - 0.01f);
            ScreenText.color = new Color(1, 1, 1, ScreenText.color.a - 0.01f);
            yield return new WaitForSeconds(0.01f);
        }
    }
    public IEnumerator EndCutscene(){
        for(int i = 0 ; i < 50 ; i++){
            Screen.color = new Color(0, 0, 0, Screen.color.a + 0.02f);
            yield return new WaitForSeconds(0.01f);
        }
        CutsceneEnd();
        for(int i = 0 ; i < 50 ; i++){
            Screen.color = new Color(0, 0, 0, Screen.color.a - 0.02f);
            yield return new WaitForSeconds(0.01f);
        }
    }


}
