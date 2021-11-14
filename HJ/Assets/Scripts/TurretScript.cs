using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretScript : MonoBehaviour
{
    public List<GameObject> Enemies = new List<GameObject>();
    [SerializeField] float FireRate;
    [SerializeField] GameObject[] FirePoints;
    [SerializeField] bool FireAllPointsAtOnce;
    [SerializeField] float offSet;
    [SerializeField] string WhatToTarget;
    [SerializeField] GameObject Bullet;
    [SerializeField] GameObject AimPart;
    [SerializeField] int damage;
    [SerializeField] float speed;
    [SerializeField] int pierce;
    [SerializeField] int BulletDespawnTimer = 0;
    float tmp = 0;
    int currentFirePoint = 0;
    
    void OnTriggerEnter(Collider other){
        if(other.transform.tag == WhatToTarget){
            if(other.transform.tag == "Enemy"){
                if(Enemies.Count != 0){
                    bool Inserted = false;
                    for(int i = 0 ; i < Enemies.Count ; i++){
                        if(Enemies[i].GetComponent<Enemy>().TurretTargetPriority < other.GetComponent<Enemy>().TurretTargetPriority){
                            Enemies.Insert(i, other.gameObject);
                            Inserted = true;
                            break;
                        }
                    }
                    if(Inserted == false){
                        Enemies.Add(other.gameObject);
                    }
                }
                else Enemies.Add(other.gameObject);
            }
            else Enemies.Add(other.gameObject);
            Enemies.Add(other.gameObject);
        }
    }

    void OnTriggerExit(Collider other){
        if(other.transform.tag == WhatToTarget){
            Enemies.Remove(other.gameObject);
        }
    }
    
    void Update(){
        if(Enemies.Count != 0){
            if(Enemies[0]==null) Enemies.Remove(Enemies[0]);
        }
        if(Enemies.Count != 0){
            AimPart.transform.rotation = Quaternion.LookRotation(Enemies[0].transform.position - transform.position);
        }
        if(tmp >= FireRate && Enemies.Count != 0){
            Fire();
            tmp = 0;
        }
        tmp += Time.deltaTime;
    }

    void Fire(){
        if(FireAllPointsAtOnce){
            for(int i = 0 ; i < FirePoints.Length ; i++){
                float randomOffSetX = Random.Range(-offSet, offSet);
                float randomOffSetY = Random.Range(-offSet, offSet);
                float x = AimPart.transform.eulerAngles.x + randomOffSetX;
                float y = AimPart.transform.eulerAngles.y + randomOffSetY;
                float z = AimPart.transform.eulerAngles.z;
                GameObject a = Instantiate(Bullet, FirePoints[i].transform.position, Quaternion.Euler(x, y, z));
                a.GetComponent<Bullet>().SetBulletDetails(damage, speed, WhatToTarget);
                a.GetComponent<Bullet>().SetPierce(pierce);
                if(BulletDespawnTimer != 0) a.GetComponent<Bullet>().SetDespawnTimer(BulletDespawnTimer);
            }
        }
        else{
            float randomOffSetX = Random.Range(-offSet, offSet);
            float randomOffSetY = Random.Range(-offSet, offSet);
            float x = AimPart.transform.eulerAngles.x + randomOffSetX;
            float y = AimPart.transform.eulerAngles.y + randomOffSetY;
            float z = AimPart.transform.eulerAngles.z;
            GameObject a = Instantiate(Bullet, FirePoints[currentFirePoint].transform.position, Quaternion.Euler(x, y, z));
            a.GetComponent<Bullet>().SetBulletDetails(damage, speed, WhatToTarget);
            a.GetComponent<Bullet>().SetPierce(pierce);
            if(BulletDespawnTimer != 0) a.GetComponent<Bullet>().SetDespawnTimer(BulletDespawnTimer);
            currentFirePoint++;
            if(currentFirePoint == FirePoints.Length) currentFirePoint = 0;
        }
    }
}