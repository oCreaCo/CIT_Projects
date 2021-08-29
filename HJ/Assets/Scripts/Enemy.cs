using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    int EnemyHp;
    [SerializeField] int EnemyDamage;
    [SerializeField] int EnemyBulletSpeed;

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
                bul.GetComponent<Bullet>().SetBulletDetails(EnemyDamage, EnemyBulletSpeed);
            }

        }
    }

    public void EnemyGetDamage(int dmg)
    {
        if (EnemyHp - dmg <= 0)
        {
            EnemyHp = 0;
            Destroy(gameObject);
        }
    }
}
