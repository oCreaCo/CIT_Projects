using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Pathfinding;

public class SupporterScript : MonoBehaviour
{
    public List<GameObject> Enemies = new List<GameObject>();
    [SerializeField] AIPath ai;
    [SerializeField] GameObject Bullet;
    [SerializeField] Animator anim;
    [SerializeField] Transform Weapon;
    int WeaponLoc = 0;
    bool WeaponWielding = false;
    int UnWieldCountDown = 0;
    IEnumerator CountCoroutine;



    void OnTriggerEnter(Collider other){
        if(other.transform.tag == "Enemy"){
            if(Enemies.Count == 0) Enemies.Add(other.gameObject);
            else{
                bool Inserted = false;
                for(int i = 0 ; i < Enemies.Count ; i++){
                    if(Enemies[i].GetComponent<Enemy>().TurretTargetPriority < other.GetComponent<Enemy>().TurretTargetPriority){
                        Enemies.Insert(i, other.gameObject);
                        Inserted = true;
                        break;
                    }
                }
                if(!Inserted) Enemies.Add(other.gameObject);
            }
        }
    }
    void OnTriggetExit(Collider other){
        if(other.tag == "Enemy") Enemies.Remove(other.gameObject);
    }

    void Update(){
        if(Enemies.Count != 0){ 
            if(Enemies[0] == null) Enemies.Remove(Enemies[0]);
            Vector3 tmp = Enemies[0].transform.position - transform.position;
            transform.rotation = Quaternion.LookRotation(new Vector3(tmp.x, 0, tmp.z));
            if(WeaponWielding) Weapon.rotation = Quaternion.LookRotation(Enemies[0].transform.position - Weapon.position);
        }
        if(Enemies.Count != 0 && WeaponWielding == false){
            anim.SetBool("WeaponWielding", true);
        }
        if(Enemies.Count == 0){
            UnWieldCountDown = 5;
            if(CountCoroutine == null){
                CountCoroutine = UnWieldCount();
                StartCoroutine(CountCoroutine);
            }
        }
        if(ai.reachedEndOfPath == false) anim.SetBool("Walking", true);
        else anim.SetBool("Walking", false);
    }

    void WeaponSwitch(){
        if(WeaponLoc == 0){
            Weapon.parent = transform.GetChild(9).transform.GetChild(2);
            WeaponLoc = 1;
            Weapon.localPosition = new Vector3(0, 0, 0);
            Weapon.localRotation = Quaternion.Euler(0, 0, 0);
        }
        if(WeaponLoc == 1){
            Weapon.parent = transform.GetChild(5);
            WeaponLoc = 0;
            Weapon.localPosition = new Vector3(0, 0, 0);
            Weapon.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }
    void WieldOrUnwield(){
        if(WeaponWielding) WeaponWielding = false;
        else WeaponWielding = true;
    }
    IEnumerator UnWieldCount(){
        while(UnWieldCountDown > 0){
            yield return new WaitForSeconds(1.0f);
            UnWieldCountDown--;
        }
        anim.SetBool("WeaponWielding", false);
    }
}
