using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : Gun
{
    [SerializeField] GameObject TracerBul;

    [SerializeField] bool isAutomode;

    [SerializeField] float automodeDelay;
    [SerializeField] float tracerDelay;
    float automodeDelayTmp;
    float tracerDelayTmp;


    public override void AddDelay()
    {
        automodeDelayTmp += Time.deltaTime;
        tracerDelayTmp += Time.deltaTime;
    }

    public override void AutomodeCheck()
    {
        if (Input.GetKeyDown(KeyCode.B) && isRifleOnHands)
        {
            isAutomode = !isAutomode;
        }
    }

    public override void Fire()
    {
        if (isAutomode)
        {
            if (automodeDelayTmp > automodeDelay)
            {
                if (tracerDelayTmp > tracerDelay)
                {
                    tracerDelayTmp = 0;
                    GameObject tracerBul = Instantiate(TracerBul, Muzzle.transform.position, Muzzle.transform.rotation);
                }
                else
                {
                    automodeDelayTmp = 0;
                    GameObject bul = Instantiate(Bullet, Muzzle.transform.position, Muzzle.transform.rotation);
                }
            }
        }
        else
        {
            if (normalDelayTmp > normalDelay)
            {
                if (tracerDelayTmp > tracerDelay)
                {
                    tracerDelayTmp = 0;
                    GameObject tracerBul = Instantiate(TracerBul, Muzzle.transform.position, Muzzle.transform.rotation);
                }
                else
                {
                    normalDelayTmp = 0;
                    GameObject bul = Instantiate(Bullet, Muzzle.transform.position, Muzzle.transform.rotation);
                }
            }
        }
    }
}