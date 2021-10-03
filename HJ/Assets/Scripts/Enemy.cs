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

    [SerializeField] PlayerScript Player;
    public Image EnemyImage;
    public string EnemyName;

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
            }

        }
    }

    public void EnemyGetDamage(int dmg)
    {
        EnemyHp -= dmg;
        if(Player.DetectedEnemies.Contains(transform) == false)
        {
            Player.DetectedEnemies.Insert(0, transform);

        }

        if (EnemyHp <= 0)
        {
            EnemyHp = 0;
            Destroy(gameObject);
        }
    }
}
