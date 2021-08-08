using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] GameObject Bul;

    [SerializeField] int bulletDamage;
    [SerializeField] float bulletSpeed;
    [SerializeField] GameObject Self;
    int Pierce = 0;

    void Start()
    {
        
    }


    void Update()
    {
        transform.Translate(Vector3.forward * bulletSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Untagged" || other.tag == "Obstacles"){
            Destroy(Self);
        }
        else if(other.tag == "Enemy"){
            //적의 HP 감소 코드
            if(Pierce==0) Destroy(Self);
            else Pierce--;
        }
    }
    public void SetDamageNSpeed(int damage, float speed){
        bulletDamage = damage;
        bulletSpeed = speed;
    }
    public void SetPierce(int p){
        Pierce = p;
    }
}
