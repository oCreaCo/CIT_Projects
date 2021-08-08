using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] protected GameObject Bullet;
    [SerializeField] protected GameObject RifleBody;
    [SerializeField] protected GameObject Muzzle;

    public bool isRifleOnHands;

    public float normalDelay;
    protected float normalDelayTmp;


    void Start()
    {
        isRifleOnHands = false;
        normalDelayTmp = normalDelay;
    }

    void Update()
    {
        normalDelayTmp += Time.deltaTime;
        AddDelay();

        // 단발-연발 변환
        AutomodeCheck();
 
        // 총 발사
        if ((Input.GetKey(KeyCode.Mouse0)) && isRifleOnHands)
        {
            Fire();

        }
       
    }
    public virtual void Fire()
    {
        
    }

    public virtual void AutomodeCheck()
    {

    }

    public virtual void AddDelay()
    {

    }
    //총 들기, 집어넣기
    public void WieldOrUnwield(){
        if (isRifleOnHands)
        {
            isRifleOnHands = false;
        }
        else
        {
            isRifleOnHands = true;
        }
    }
}
