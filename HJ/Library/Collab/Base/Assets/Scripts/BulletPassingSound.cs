using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPassingSound : MonoBehaviour
{
    System.Random random = new System.Random();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Collider[] col = Physics.OverlapSphere(transform.position, 2.0f);

        for (int i = 0; i < col.Length; ++i)
        {
            if (transform.tag == "EnemyBullet" && col[i].tag == "Player")
            {
                int rnd = random.Next(3);

                if (rnd == 0)
                    transform.FindChild("Sound-BulletPass1").GetComponent<AudioSource>().Play();
                if (rnd == 1)
                    transform.FindChild("Sound-BulletPass2").GetComponent<AudioSource>().Play();
                if (rnd == 2)
                    transform.FindChild("Sound-BulletPass3").GetComponent<AudioSource>().Play();
                if (rnd == 3)
                    transform.FindChild("Sound-BulletPass4").GetComponent<AudioSource>().Play();

                break;
            }
        }
    }
}
