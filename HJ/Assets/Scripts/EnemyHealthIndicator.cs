using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthIndicator : MonoBehaviour
{
    [SerializeField] GameObject EnemyHealthIndicatorPrefab;
    List<GameObject> Indicators = new List<GameObject>();


    public void AddIndicator(int ImageCode, Text EnemyName)
    {
        GameObject a = Instantiate(EnemyHealthIndicatorPrefab, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0));
        a.transform.SetParent(transform);
        a.GetComponent<RectTransform>().localPosition = new Vector3(400, 350-(Indicators.Count*50), 0);
        a.transform.GetChild(2).GetChild(ImageCode).gameObject.SetActive(true);
        Indicators.Insert(0, a);
    }

    public void RemoveIndicator()
    {
        Destroy(Indicators[Indicators.Count-1]);
        if (Indicators.Count >= 2)
        {
            for(int i = 0; i < Indicators.Count-2; i++)
            {
                Indicators[i].GetComponent<RectTransform>().localPosition = new Vector3(400, Indicators[i].GetComponent<RectTransform>().localPosition.y+50, 0);
            }
        }
    }
}
