using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : MonoBehaviour
{
    [SerializeField] GameObject Bullet;
    [SerializeField] GameObject RifleBody;
    [SerializeField] GameObject Muzzle;

    public bool isRifleOnHands;

    [SerializeField] int bulletDamge;
    [SerializeField] int bulletSpeed;
    [SerializeField] double automodeShootDelay;
    [SerializeField] double normalmodeShootDelay;

    double automodeShotDelayTemp;
    double normalmodeShotDelayTemp;

    [SerializeField] bool isAutomode;

   
    void Start()
    {
        isRifleOnHands = false;
        automodeShotDelayTemp = automodeShootDelay;
        normalmodeShotDelayTemp = normalmodeShootDelay;
    }

    void Update()
    {
        automodeShotDelayTemp += Time.deltaTime;
        automodeShotDelayTemp += Time.deltaTime;

        // 총 들기 || 총 집어넣기
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (isRifleOnHands)
            {
                // 현재무기 비활성화
                RifleBody.SetActive(false);
                isRifleOnHands = false;
            }
            else
            {
                RifleBody.SetActive(true);
                isRifleOnHands = true;
            }
        }

        // 단발-연발 변환
        if (Input.GetKeyDown(KeyCode.B) && isRifleOnHands)
        {
            if (isAutomode)
            {
                isAutomode = false;
            }
            else
            {
                isAutomode = true;
            }
        }

        // 총 발사
        if ((Input.GetKey(KeyCode.Mouse0) || Input.GetKey(KeyCode.Mouse0)) && isRifleOnHands)
        {
            GameObject bul = Instantiate(Bullet);
            bul.transform.SetParent(Muzzle.transform);

            if (isAutomode)
            {
                if (automodeShotDelayTemp > automodeShootDelay)
                {
                    automodeShotDelayTemp = 0;
                    bul.transform.Translate(Vector3.forward * bulletSpeed * Time.deltaTime);
                }
            }
            else
            {
                if (normalmodeShotDelayTemp > normalmodeShootDelay)
                {
                    normalmodeShotDelayTemp = 0;
                    bul.transform.Translate(Vector3.forward * bulletSpeed * Time.deltaTime);
                }
            }

        }
    }
}
