using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DefendingPoint : MonoBehaviour
{
    [SerializeField] int Health, maxHealth;
    [SerializeField] Transform HealthBar, HealthText, ProtectPointUI;
    [SerializeField] string PointName;
    void Start(){
        Health = maxHealth;
        HealthBar.GetComponent<Image>().color = new Color(0, 1, 0);
        HealthText.GetComponent<Text>().text = PointName;
        ProtectPointUI.gameObject.SetActive(true);
    }

    public void GetDamage(int damage){
        Health -= damage;
            if(Health <= 0){
                //insert gameover here.
            }
        HealthBar.GetComponent<RectTransform>().localScale = new Vector3((float)maxHealth/(float)Health, 0.5f, 1);
    }

}
