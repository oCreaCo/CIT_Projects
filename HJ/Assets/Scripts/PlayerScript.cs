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
    [SerializeField] Transform ArmPart;
    [SerializeField] Animator Anim;
    float xRot, yRot;

    void Update()
    {
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
        MouseRot();
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
        ArmPart.localRotation = Quaternion.Euler(xRot, 0, 0);
    }
}
