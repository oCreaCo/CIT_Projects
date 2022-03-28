using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BulletPassingSound : MonoBehaviour
{
    System.Random random = new System.Random();

    void Start()
    {

    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            int rnd = random.Next(3);

            transform.Find("BulletPassingSound" + rnd)
                .GetComponent<AudioSource>().Play();
        }
    }
}