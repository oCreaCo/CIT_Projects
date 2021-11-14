using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] float range;
    [SerializeField] LayerMask whatTohit;
    RaycastHit hit;

    [SerializeField] float FireDelay;
    [SerializeField] float FireDelayTmp;

    [SerializeField] GameObject Bullet;
    [SerializeField] GameObject Muzzle;

    [SerializeField] Transform firePoint;

    [SerializeField] int EnemyOriHp;
    [SerializeField] int EnemyHp;
    [SerializeField] int EnemyDamage;
    [SerializeField] int EnemyBulletSpeed;
    
    [SerializeField] GameObject IndicatorPrefab;
    [SerializeField] Transform IndicatorCanvas;
    public int TurretTargetPriority = 0;
    public int EnemyImageCode;
    public string EnemyName;
    int IndicatorDestroyCount = 0;
    IEnumerator CountCoroutine;

    void Start()
    {
        FireDelayTmp = FireDelay;
        EnemyHp = EnemyOriHp;
    }

    void Update()
    {
        FireDelayTmp += Time.deltaTime;

        if (FireDelayTmp > FireDelay)
        {
            if (Physics.Raycast(firePoint.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, whatTohit))
            {
                //Debug.Log(hit.transform);
                FireDelayTmp = 0;
                GameObject bul = Instantiate(Bullet, Muzzle.transform.position, Muzzle.transform.rotation);
                bul.GetComponent<Bullet>().SetBulletDetails(EnemyDamage, EnemyBulletSpeed, "Player");
            }

        }
    }

    public void EnemyGetDamage(int dmg)
    {
        EnemyHp -= dmg;
        bool Included = false;
        for(int i = 0 ; i < EnemyHealthIndicator.EHI.Indicators.Count ; i++){
            if(EnemyHealthIndicator.EHI.Indicators[i].Enemy == transform){
                Included = true;
                IndicatorDestroyCount = 5;
                EnemyHealthIndicator.EHI.Indicators[i].IndicatorObject.transform.GetChild(3).GetComponent<RectTransform>().localScale = new Vector3((float)EnemyHp/(float)EnemyOriHp, 1, 1);
                Color e = new Color(1.0f-(float)EnemyHp/(float)EnemyOriHp, (float)EnemyHp/(float)EnemyOriHp, 0);
                EnemyHealthIndicator.EHI.Indicators[i].IndicatorObject.transform.GetChild(3).GetComponent<Image>().color = e;
                break;
            }
        }
        if(Included == false){
            IndicatorDestroyCount = 5;
            Indicator a = new Indicator();
            a.Enemy = transform;
            a.IndicatorObject = Instantiate(IndicatorPrefab, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0));
            a.IndicatorObject.transform.SetParent(IndicatorCanvas);
            a.IndicatorObject.transform.GetChild(3).GetComponent<RectTransform>().localScale = new Vector3((float)EnemyHp/(float)EnemyOriHp, 1, 1);
            Color e = new Color(1.0f-(float)EnemyHp/(float)EnemyOriHp, EnemyHp/EnemyOriHp, 0);
            a.IndicatorObject.transform.GetChild(3).GetComponent<Image>().color = e;
            EnemyHealthIndicator.EHI.Indicators.Insert(0, a);
            EnemyHealthIndicator.EHI.NewIndicator();
            if(CountCoroutine == null){
                CountCoroutine = IndicatorDisapper();
                StartCoroutine(CountCoroutine);
            }
        }

        if (EnemyHp <= 0)
        {
            EnemyHp = 0;
            for(int i = 0 ; i < EnemyHealthIndicator.EHI.Indicators.Count ; i++){
                if(EnemyHealthIndicator.EHI.Indicators[i].Enemy == transform){
                    Destroy(EnemyHealthIndicator.EHI.Indicators[i].IndicatorObject);
                    EnemyHealthIndicator.EHI.RemoveIndicator(i);
                    break;
                }
            }
            Destroy(this.gameObject);
        }
    }
    IEnumerator IndicatorDisapper(){
        while(IndicatorDestroyCount > 0){
            yield return new WaitForSeconds(1.0f);
            IndicatorDestroyCount--;
        }
       for(int i = 0 ; i < EnemyHealthIndicator.EHI.Indicators.Count ; i++){
            if(EnemyHealthIndicator.EHI.Indicators[i].Enemy == transform){
                Destroy(EnemyHealthIndicator.EHI.Indicators[i].IndicatorObject);
                EnemyHealthIndicator.EHI.RemoveIndicator(i);
                break;
            }
        }
        CountCoroutine = null;
    }
}
