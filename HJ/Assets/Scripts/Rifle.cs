using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : Gun
{
    [SerializeField] GameObject TracerBul;

    [SerializeField] bool isAutomode;

    public float automodeDelay;
    [SerializeField] int AutoTracerApperCount;
    [SerializeField] int NormalTracerApperCount;
    public int AutoDamage;
    public float AutoSpeed;
    public int NormalDamage;
    [SerializeField] float NormalSpeed;
    [SerializeField] int tracerBulDamageMultiplier;
    float automodeDelayTmp;
    [SerializeField] int untilTracer;
    [SerializeField] int NormalPierce = 0;

    void Start(){
        untilTracer = NormalTracerApperCount;
    }

    public override void AddDelay()
    {
        automodeDelayTmp += Time.deltaTime;
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
                if(untilTracer <= 0){
                    untilTracer = AutoTracerApperCount;
                    GameObject tracerBul = Instantiate(TracerBul, Muzzle.transform.position, Muzzle.transform.rotation);
                    tracerBul.transform.GetComponent<Bullet>().SetDamageNSpeed(AutoDamage*tracerBulDamageMultiplier, AutoSpeed);
                }
                else{
                    GameObject bul = Instantiate(Bullet, Muzzle.transform.position, Muzzle.transform.rotation);
                    bul.transform.GetComponent<Bullet>().SetDamageNSpeed(AutoDamage, AutoSpeed);
                    untilTracer--;
                }
                automodeDelayTmp = 0;
            }
        }
        else
        {
            if(normalDelayTmp > normalDelay){
                if(untilTracer <= 0){
                    untilTracer = NormalTracerApperCount;
                    GameObject tracerBul = Instantiate(TracerBul, Muzzle.transform.position, Muzzle.transform.rotation);
                    tracerBul.transform.GetComponent<Bullet>().SetDamageNSpeed(NormalDamage*tracerBulDamageMultiplier, NormalSpeed);
                    tracerBul.transform.GetComponent<Bullet>().SetPierce(NormalPierce);
                }
                else{
                    GameObject bul = Instantiate(Bullet, Muzzle.transform.position, Muzzle.transform.rotation);
                    bul.transform.GetComponent<Bullet>().SetDamageNSpeed(NormalDamage, NormalSpeed);
                    bul.transform.GetComponent<Bullet>().SetPierce(NormalPierce);
                    untilTracer--;
                }
                normalDelayTmp = 0;
            }
        }
    }
    public void AssasultDamageSet(int damage){
        AutoDamage = damage;
    }
    public void SnipeDamageSet(int damage){
        NormalDamage = damage;
    }
    public void AssasultFirerateSet(float firerate){
        automodeDelay = firerate;
    }
    public void SnipeFirerateSet(float firerate){
        normalDelay = firerate;
    }
    public void AssasultSpeedSet(float speed){
        AutoSpeed = speed;
    }
    public void SnipePierceSet(int pierce){
        NormalPierce = pierce;
    }
    public void SetProperty(int WhatToEdit, float EditValue){
        if(WhatToEdit == 0){
            automodeDelay = EditValue;
        }
        else if(WhatToEdit == 1){
            AutoDamage = (int)EditValue;
        }
        else if(WhatToEdit == 2){
            AutoSpeed = EditValue;
        }
        else if(WhatToEdit == 3){
            normalDelay = EditValue;
        }
        else if(WhatToEdit == 4){
            NormalDamage = (int)EditValue;
        }
        else if(WhatToEdit == 5){
            NormalPierce = (int)EditValue;
        }
    }
}