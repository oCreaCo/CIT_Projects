using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Pathfinding;

public class Enemy : MonoBehaviour
{
    Animator anim;
    [SerializeField] bool HasAnimation = false;
 
    [SerializeField] LayerMask whatToHit;
    RaycastHit hit;

    [SerializeField] float FireDelay;
    [SerializeField] float FireDelayTmp;

    [SerializeField] GameObject Target;
    [SerializeField] LayerMask WhatToTarget;
    [SerializeField] float[] DetectRange;
    [SerializeField] GameObject AimPart;
    [SerializeField] bool AimPartYRotation;
    [SerializeField] GameObject Body;
    [SerializeField] bool BodyRotation;
    [SerializeField] bool BodyYRotation;
    [SerializeField] GameObject Bullet;
    [SerializeField] GameObject Muzzle;
    [SerializeField] GameObject[] FirePoints;

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

    [SerializeField] float enemyGunEnableDistance;
    [SerializeField] float enemyWalkEnableDistance;

    AIPath aip;
    AIDestinationSetter aid;

    public int GetHP() {return EnemyHp;}
    
    void Start()
    {
        anim = GetComponent<Animator>();

        FireDelayTmp = FireDelay;
        EnemyHp = EnemyOriHp;

        aip = GetComponent<AIPath>();
        aid = GetComponent<AIDestinationSetter>();
    }

    void Update()
    {
        FireDelayTmp += Time.deltaTime;
        bool TargetExists = false;
        for(int i = 0 ; i < DetectRange.Length ; i++){
            Collider[] hit = Physics.OverlapSphere(transform.position, DetectRange[i], WhatToTarget);
            int smallest = 999, smallestCode = 0;
            for(int k = 0 ; k < hit.Length ; k++){
                if(hit[k].GetComponent<TargetObject>() != null){
                    if(hit[k].GetComponent<TargetObject>().priority < smallest){
                        smallest = hit[k].GetComponent<TargetObject>().priority;
                        smallestCode = k;
                    }
                }
            }
            if(hit.Length != 0){
                Target = hit[smallestCode].gameObject;
                TargetExists = true;
                break;
            }
        }
        if(TargetExists == false){
            if(HasAnimation) anim.SetBool("isInArea", false);
            return;
        }

        Vector3 vec = AimPart.transform.position - Target.transform.position;
        if(AimPartYRotation == false) AimPart.transform.rotation = Quaternion.LookRotation(new Vector3(vec.x, 0, vec.z));
        else AimPart.transform.rotation = Quaternion.LookRotation(new Vector3(vec.x, vec.y, vec.z));

        if (/*Vector3.Distance(Target.transform.position, transform.position) < enemyGunEnableDistance*/true)
        {
            // Shoot
            if (FireDelayTmp > FireDelay)
            {
                if (Physics.Raycast(Muzzle.transform.position, Muzzle.transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, whatToHit))
                {
                    //Debug.Log(hit.transform);
                    FireDelayTmp = 0;
                    for(int i = 0 ; i < FirePoints.Length ; i++){
                        GameObject bul = Instantiate(Bullet, FirePoints[i].transform.position, FirePoints[i].transform.rotation);
                        bul.tag = "EnemyBullet";
                        bul.GetComponent<Bullet>().SetBulletDetails(EnemyDamage, EnemyBulletSpeed, "Player");
                        transform.GetComponent<AudioSource>().Play();
                    }
                }
            }

            if(HasAnimation) anim.SetBool("isInArea", true);
        }
        else
        {
            if(HasAnimation) anim.SetBool("isInArea", false);
        }

        /*if (Vector3.Distance(Player.transform.position, transform.position) < enemyWalkEnableDistance)
        {
            if(HasAnimation) anim.SetBool("Walking", true);
            aid.target = Player.transform;
            aip.canMove = true;
        }
        else
        {
            if(HasAnimation) anim.SetBool("Walking", true);
            aid.target = null;
            aip.canMove = false;
        }*/
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
[System.Serializable] struct T{
    public float TargetDistance;
    public GameObject TargetObject;

}