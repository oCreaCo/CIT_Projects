using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] GameObject Bul;

    [SerializeField] int bulletDamage;
    [SerializeField] float bulletSpeed;
    [SerializeField] GameObject Enemy;
    [SerializeField] GameObject Player;
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
        if(other.tag == "Untagged" || other.tag == "Obstacles")
        {
            Destroy(gameObject);
        }
        else if(other.tag == "Enemy")
        {
            Enemy.GetComponent<Enemy>().EnemyGetDamage(bulletDamage);
        }
        else if(other.tag == "Player")
        {
            Player.GetComponent<PlayerScript>().GetDamage(bulletDamage);
        }
    }
    public void SetBulletDetails(int damage, float speed){
        bulletDamage = damage;
        bulletSpeed = speed;
    }
    public void SetPierce(int p){
        Pierce = p;
    }
}
