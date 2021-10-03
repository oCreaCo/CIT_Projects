using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeMenu : MonoBehaviour
{
    [SerializeField] Image Menu1Panel1;
    [SerializeField] Image Menu1Panel2;
    [SerializeField] GameObject Menu1Button1;
    [SerializeField] GameObject Menu1Button2;
    [SerializeField] GameObject Menu1Text1;
    [SerializeField] GameObject Menu1Text2;
    [SerializeField] GameObject Menu2Buttons;
    [SerializeField] GameObject Menu3Buttons;
    [SerializeField] Text[] MoneyTexts;
    [SerializeField] UpgradeManagement Manager;
    [SerializeField] Gun gun;
    [SerializeField] PlayerScript Player;
    IEnumerator ApperingCoroutine;
    public void Apper(bool Disappering){
        if(Disappering){
            Menu1Button1.SetActive(false);
            Menu1Button2.SetActive(false);
            Menu1Text1.SetActive(false);
            Menu1Text2.SetActive(false);
        }
        if(ApperingCoroutine!=null) StopCoroutine(ApperingCoroutine);
        ApperingCoroutine = Appering(Disappering);
        StartCoroutine(ApperingCoroutine);
    }
    public void Menu2Apper(){
        StartCoroutine(Menu2(false));
    }
    public void Menu2Close(){
        StartCoroutine(Menu2(true));
    }
    public void Menu3Apper(){
        StartCoroutine(Menu3(false));
    }
    public void Menu3Close(){
        StartCoroutine(Menu3(true));
    }

    IEnumerator Appering(bool Disappering){
        float a;
        if(Disappering){
            a = -0.02f;
            Menu2Buttons.SetActive(false);
            Menu3Buttons.SetActive(false);
        }
        else{ 
            a = 0.02f;
            gun.Disabled = true;
            gun.BulletInfo.SetActive(false);
            Player.canSwitchWeapon = false;
        }
        for(int i = 0 ; i < 50 ; i++){
            float r = Menu1Panel1.color.r;
            float g = Menu1Panel1.color.g;
            float b = Menu1Panel1.color.b;
            float aa = Menu1Panel1.color.a + a;
            Menu1Panel1.color = new Color(r, g, b, aa);
            r = Menu1Panel2.color.r;
            g = Menu1Panel2.color.g;
            b = Menu1Panel2.color.b;
            aa = Menu1Panel2.color.a + a;
            Menu1Panel2.color = new Color(r, g, b, aa);
            yield return new WaitForSeconds(0.01f);
        }
        if(!Disappering){
            Menu1Button1.SetActive(true);
            Menu1Button2.SetActive(true);
            Menu1Text1.SetActive(true);
            Menu1Text2.SetActive(true);
        }
        else{
            Menu1Panel1.GetComponent<RectTransform>().localPosition = new Vector3(-50, 0, 0);
            Menu1Panel2.GetComponent<RectTransform>().localPosition = new Vector3(50, 0, 0);
            gun.Disabled = false;
            if(gun.isRifleOnHands == true){
                gun.BulletInfo.SetActive(true);
            }
            Player.canSwitchWeapon = true;
        }
    }
    IEnumerator Menu2(bool Returning){
        if(Returning==false){
            Menu1Button1.SetActive(false);
            Menu1Button2.SetActive(false);
            Menu1Text1.SetActive(false);
            Menu1Text2.SetActive(false);
        }
        else Menu2Buttons.SetActive(false);
        Menu1Panel1.transform.SetSiblingIndex(1);
        float o;
        if(Returning) o = -1.0f;
        else o = 1.0f; 
        for(int i = 0 ; i < 50 ; i++){
            Menu1Panel1.GetComponent<RectTransform>().localPosition = new Vector3(Menu1Panel1.GetComponent<RectTransform>().localPosition.x+o, Menu1Panel1.GetComponent<RectTransform>().localPosition.y, Menu1Panel1.GetComponent<RectTransform>().localPosition.z);
            yield return new WaitForSeconds(0.01f);
        }
        if(Returning){
            Menu1Button1.SetActive(true);
            Menu1Button2.SetActive(true);
            Menu1Text1.SetActive(true);
            Menu1Text2.SetActive(true);
        }
        else Menu2Buttons.SetActive(true);
    }
    IEnumerator Menu3(bool Returning){
        if(Returning==false){
            Menu1Button1.SetActive(false);
            Menu1Button2.SetActive(false);
            Menu1Text1.SetActive(false);
            Menu1Text2.SetActive(false);
        }
        else Menu3Buttons.SetActive(false);
        Menu1Panel2.transform.SetSiblingIndex(1);
        float o;
        if(Returning) o = 1.0f;
        else o = -1.0f; 
        for(int i = 0 ; i < 50 ; i++){
            Menu1Panel2.GetComponent<RectTransform>().localPosition = new Vector3(Menu1Panel2.GetComponent<RectTransform>().localPosition.x+o, Menu1Panel2.GetComponent<RectTransform>().localPosition.y, Menu1Panel2.GetComponent<RectTransform>().localPosition.z);
            yield return new WaitForSeconds(0.01f);
        }
        if(Returning){
            Menu1Button1.SetActive(true);
            Menu1Button2.SetActive(true);
            Menu1Text1.SetActive(true);
            Menu1Text2.SetActive(true);
        }
        else Menu3Buttons.SetActive(true);
    }
    public void MoneyTextUpdate(){
        for(int i = 0 ; i < MoneyTexts.Length ; i++){
            MoneyTexts[i].text = "Money: " + Manager.Money;
        }
    }
}
