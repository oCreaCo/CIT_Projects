using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] float MoveSpeed;
    [SerializeField] float CameraSensivity;
    [SerializeField] float YRotationLimit;
    [SerializeField] int YRotationReverse;
    [SerializeField] Transform Head;
    //[SerializeField] Transform ArmPart;
    public Animator Anim;
    [SerializeField] Transform Weapon;
    [SerializeField] Gun gun;
    public bool canMove = true;
    public bool canSwitchWeapon = true;
    protected bool Wielding = false;
    float Cooling = 0;
    float xRot, yRot;

    [SerializeField] int playerOriHp;
    [SerializeField] int playerHp;

    public List<Transform> DetectedEnemies = new List<Transform>();
    public EnemyHealthIndicator Indicator;

    void Start()
    {
        playerHp = playerOriHp;
    }

    void Update()
    {
        if (canMove) {
            float x = Input.GetAxisRaw("Horizontal");
            float z = Input.GetAxisRaw("Vertical");

            if (x == 1 || x == -1 || z == 1 || z == -1)
            {
                Anim.SetBool("Walking", true);
            }
            else Anim.SetBool("Walking", false);

            Vector3 dir = new Vector3(x, 0, z);
            dir.Normalize();

            transform.Translate(dir * MoveSpeed * Time.deltaTime);
        }

        MouseRot();
        if (Cooling > 0) Cooling -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Alpha1) && Cooling <= 0 && canSwitchWeapon)
        {
            if (Anim.GetBool("WeaponWielding") == false)
            {
                Anim.SetBool("WeaponWielding", true);
                Cooling = 1.50f;
            }
            else
            {
                Anim.SetBool("WeaponWielding", false);
                Cooling = 1.25f;
            }
        }
        if(Input.GetButtonDown("Reload")){
            if(gun.isAutomode==true && gun.bulletFireCount != 0) Anim.SetTrigger("Reload");
            if(gun.isAutomode==false && gun.SnipeFireCount != 0) Anim.SetTrigger("Reload");
        }
    }

    void MouseRot()
    {
        float _yRot = Input.GetAxisRaw("Mouse X") * CameraSensivity;
        float _xRot = Input.GetAxisRaw("Mouse Y") * YRotationReverse * CameraSensivity;

        xRot += _xRot;
        yRot += _yRot;
        xRot = Mathf.Clamp(xRot, -YRotationLimit, YRotationLimit);

        transform.rotation = Quaternion.Euler(transform.rotation.x, yRot, transform.rotation.z);
        Head.localRotation = Quaternion.Euler(xRot, 0, 0);
        //ArmPart.localRotation = Quaternion.Euler(xRot, 0, 0);
    }
    public void WeaponSwitch()
    {
        if (Wielding == false)
        {
            Weapon.parent = transform.GetChild(5);
            Wielding = true;
            Weapon.localPosition = new Vector3(0, 0, 0);
            Weapon.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            Weapon.parent = transform.GetChild(6);
            Wielding = false;
            Weapon.localPosition = new Vector3(0, 0, 0);
            Weapon.localRotation = Quaternion.Euler(0, 0, 0);
        }

    }

    public void WieldOrUnwield() {
        gun.WieldOrUnwield();
    }

    public void GetDamage(int dmg)
    {
        if (playerHp - dmg <= 0)
        {
            playerHp = 0;
            Debug.Log("Player Died!!");
            return;
        }

        playerHp -= dmg;
    }

    public void ReloadGun()
    {
        gun.Reload();
        gun.BulletInfoUI();
    }
    public void UpdateEnemyHPList()
    {
        
    }
    IEnumerator RemoveOneIndicator()
    {
        yield return new WaitForSeconds(5.0f);
        Indicator.RemoveIndicator();
    }
}
