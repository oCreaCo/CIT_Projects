using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rifle : Gun
{
    [SerializeField] GameObject TracerBul;

    public float automodeDelay;
    float automodeDelayTmp;

    public int AutoDamage;
    public int NormalDamage;

    public float AutoSpeed;
    [SerializeField] float NormalSpeed;

    [SerializeField] int AutoTracerApperCount;
    [SerializeField] int NormalTracerApperCount;
    [SerializeField] int tracerBulDamageMultiplier;
    [SerializeField] int untilTracer;
    [SerializeField] int NormalPierce = 0;

    public bool isBulletBeam;
    public bool isSnipeBulletInf = false;
    public bool isNormalBulletInf = false;


    void Start()
    {
        base.Start();
        untilTracer = NormalTracerApperCount;
        bulletFireCount = 0;
        BulletInfoUI();
    }

    public override void AddDelay()
    {
        automodeDelayTmp += Time.deltaTime;
    }

    public override void AutomodeCheck()
    {
        if (Input.GetKeyDown(KeyCode.B) && isRifleOnHands && Disabled == false)
        {
            isAutomode = !isAutomode;
            BulletInfoUI();
        }
    }

    public override void Fire()
    {

        if (isAutomode)
        {
            if (automodeDelayTmp > automodeDelay)
            {   
                if(bulletFireCount < magazineSize)
                {
                    if(untilTracer <= 0)
                    {
                        untilTracer = AutoTracerApperCount;
                        GameObject tracerBul = Instantiate(TracerBul, Muzzle.transform.position, Muzzle.transform.rotation);
                        tracerBul.transform.GetComponent<Bullet>().SetBulletDetails(AutoDamage*tracerBulDamageMultiplier, AutoSpeed, "Enemy");
                        bulletFireCount++;
                        BulletInfoUI();
                    }
                    else
                    {
                        GameObject bul = Instantiate(Bullet, Muzzle.transform.position, Muzzle.transform.rotation);
                        bul.transform.GetComponent<Bullet>().SetBulletDetails(AutoDamage, AutoSpeed, "Enemy");
                        untilTracer--;
                        bulletFireCount++;
                        BulletInfoUI();
                    }
                    automodeDelayTmp = 0;
                }
            }
        }
        else
        {
            if(normalDelayTmp > normalDelay){
                if(SnipeFireCount < SnipeMagazineSize)
                {
                    if (isBulletBeam)
                    {
                        GameObject bul = Instantiate(BeamBullet, Muzzle.transform.position, Muzzle.transform.rotation);
                        bul.transform.GetComponent<Bullet>().SetBulletDetails(NormalDamage, 0, "Enemy");
                        SnipeFireCount++;
                        BulletInfoUI();
                    }
                    else{
                        if(untilTracer <= 0){
                            untilTracer = NormalTracerApperCount;
                            GameObject tracerBul = Instantiate(TracerBul, Muzzle.transform.position, Muzzle.transform.rotation);
                            tracerBul.transform.GetComponent<Bullet>().SetBulletDetails(NormalDamage*tracerBulDamageMultiplier, NormalSpeed, "Enemy");
                            tracerBul.transform.GetComponent<Bullet>().SetPierce(NormalPierce);
                            SnipeFireCount++;
                            BulletInfoUI();
                        }
                        else
                        {
                            GameObject bul = Instantiate(Bullet, Muzzle.transform.position, Muzzle.transform.rotation);
                            bul.transform.GetComponent<Bullet>().SetBulletDetails(NormalDamage, NormalSpeed, "Enemy");
                            bul.transform.GetComponent<Bullet>().SetPierce(NormalPierce);
                            untilTracer--;
                            SnipeFireCount++;
                            BulletInfoUI();
                        }
                    }
                    normalDelayTmp = 0;
                }
            }
        }
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
        else if(WhatToEdit == 6){
            magazineSize = (int)EditValue;
            bulletFireCount = 0;
            BulletInfoUI();
        }
        else if(WhatToEdit == 7){
            SnipeMagazineSize = (int)EditValue;
            SnipeFireCount = 0;
            BulletInfoUI();
        }
        else if(WhatToEdit == 8){
            isBulletBeam = true;
        }
        else if(WhatToEdit == 9){
            isNormalBulletInf = true;
        }
        else if(WhatToEdit == 10){
            isSnipeBulletInf = true;
        }
        
    }
    public override void BulletInfoUI()
    {
        if(isAutomode){
            if(isNormalBulletInf){
                
            }
            BulletInfoText.text = (magazineSize - bulletFireCount)
            .ToString() + "/" + magazineSize.ToString();
        }
        else{
            BulletInfoText.text = (SnipeMagazineSize - SnipeFireCount)
            .ToString() + "/" + SnipeMagazineSize.ToString();
        }
    }
    public override void Reload(){
        if(isAutomode){
            bulletFireCount = 0;
        }
        else SnipeFireCount = 0;
        BulletInfoUI();
    }
}