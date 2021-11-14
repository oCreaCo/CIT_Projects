using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthIndicator : MonoBehaviour
{
    public static EnemyHealthIndicator EHI;

    void Awake(){
        EnemyHealthIndicator.EHI = this;
    }

    public List<Indicator> Indicators = new List<Indicator>();

    
   public void NewIndicator(){
       Indicators[0].IndicatorObject.GetComponent<RectTransform>().localPosition = new Vector3(220, 220-(Indicators.Count*50), 0);
       Indicators[0].IndicatorObject.GetComponent<Transform>().GetChild(2).GetChild(Indicators[0].Enemy.GetComponent<Enemy>().EnemyImageCode).gameObject.SetActive(true);
       Indicators[0].IndicatorObject.GetComponent<Transform>().GetChild(1).GetComponent<Text>().text = Indicators[0].Enemy.GetComponent<Enemy>().EnemyName;
   }
   public void RemoveIndicator(int which){
       for(int i = which ; i >= 0 ; i--){
           Indicators[i].IndicatorObject.GetComponent<RectTransform>().localPosition = new Vector3(220, Indicators[i].IndicatorObject.GetComponent<RectTransform>().localPosition.y+50, 0);
       }
       Indicators.RemoveAt(which);
   }
}
[System.Serializable] 
public class Indicator{
    public Transform Enemy;
    public GameObject IndicatorObject; 
}