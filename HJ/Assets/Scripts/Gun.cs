using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
    [SerializeField] protected GameObject Bullet;
    [SerializeField] protected GameObject BeamBullet;
    [SerializeField] protected GameObject RifleBody;
    [SerializeField] protected GameObject Muzzle;

    public bool isRifleOnHands;
    public bool Disabled = false;

    public float normalDelay;
    protected float normalDelayTmp;

    public int magazineSize;
    public int bulletFireCount;
    public int SnipeMagazineSize;
    public int SnipeFireCount;


    [SerializeField] Animator anim;
    public bool isAutomode;
    public GameObject BulletInfo;
    public Text BulletInfoText;

    protected void Start()
    {
        isRifleOnHands = false;
        normalDelayTmp = normalDelay;
        anim = transform.parent.parent.GetComponent<PlayerScript>().Anim;
    }

    void Update()
    {
        normalDelayTmp += Time.deltaTime;
        AddDelay();

        // 단발-연발 변환
        AutomodeCheck();
 
        // 총 발사
        if ((Input.GetKey(KeyCode.Mouse0)) && isRifleOnHands && Disabled==false)
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
    
    public virtual void Reload(){

    }

    //총 들기, 집어넣기
    public void WieldOrUnwield(){
        if (isRifleOnHands)
        {
            BulletInfo.SetActive(false);
            isRifleOnHands = false;
        }
        else
        {
            BulletInfo.SetActive(true);
            isRifleOnHands = true;
        }
    }
    public void DisableOrEnableFire(bool e){
        isRifleOnHands = e;
        if(e==true) Debug.Log("true");
    }

    public virtual void BulletInfoUI()
    {

    }
}
