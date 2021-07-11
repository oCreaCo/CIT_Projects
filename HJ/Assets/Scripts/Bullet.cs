using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] GameObject Bul;

    [SerializeField] int bulletDamge;
    [SerializeField] int bulletSpeed;

    void Start()
    {
        
    }


    void Update()
    {
        transform.Translate(Vector3.forward * bulletSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("player hitted");
        }
        else if (other.tag == "Ground")
            Debug.Log("Obj hitted");
    }
}
