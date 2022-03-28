using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Bullet : MonoBehaviour
{
    [SerializeField] GameObject Bul;

    [SerializeField] int bulletDamage;
    [SerializeField] float bulletSpeed;
    [SerializeField] bool Beam = false;
    [SerializeField] Material[] BeamMaterial;
    [SerializeField] GameObject player;
    string whatToHit = " ";
    int Pierce = 0;
    int DespawnTimer = 0;

    System.Random random = new System.Random();

    void Start()
    {
        if(Beam) StartCoroutine(BeamDisappear());
    }


    void Update()
    {
        transform.Translate(Vector3.forward * bulletSpeed * Time.deltaTime);

        // if(Vector3.Distance(player.transform.position, transform.position) < 10 && Beam == false)
        // {
        //     int rnd = random.Next(3);

        //     if (rnd == 0)
        //         player.transform.Find("Sound-BulletPass1").GetComponent<AudioSource>().Play();
        //     if (rnd == 1)
        //         player.transform.Find("Sound-BulletPass2").GetComponent<AudioSource>().Play();
        //     if (rnd == 2)
        //         player.transform.Find("Sound-BulletPass3").GetComponent<AudioSource>().Play();
        //     if (rnd == 3)
        //         player.transform.Find("Sound-BulletPass4").GetComponent<AudioSource>().Play();
        // }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(Beam == false){
            if(other.tag == "Obstacles")
            {
                if(Pierce==0) Destroy(gameObject);
                Pierce--;
            }
            if(other.transform.GetComponent<Enemy>() != null && whatToHit == "Enemy"){
                other.transform.GetComponent<Enemy>().EnemyGetDamage(bulletDamage);
                if(Pierce==0) Destroy(gameObject);
                Pierce--;
            }
            if(other.transform.GetComponent<PlayerScript>() != null && whatToHit == "Player"){
                other.transform.GetComponent<PlayerScript>().GetDamage(bulletDamage);
                if(Pierce==0) Destroy(gameObject);
                Pierce--;
            }
            if(other.transform.GetComponent<DefendingPoint>() != null && whatToHit == "Player"){
                other.transform.GetComponent<DefendingPoint>().GetDamage(bulletDamage);
                if(Pierce==0) Destroy(gameObject);
                Pierce--;
            }
        }
        else{
            if(other.transform.GetComponent<Enemy>() != null && whatToHit == "Enemy"){
                other.transform.GetComponent<Enemy>().EnemyGetDamage(bulletDamage);
            }
            if(other.transform.GetComponent<PlayerScript>() != null && whatToHit == "Player"){
                other.transform.GetComponent<PlayerScript>().GetDamage(bulletDamage);
            }
            if(other.transform.GetComponent<DefendingPoint>() != null && whatToHit == "Player"){
                other.transform.GetComponent<DefendingPoint>().GetDamage(bulletDamage);
            }
        }
    }
    public void SetBulletDetails(int damage, float speed, string target){
        bulletDamage = damage;
        bulletSpeed = speed;
        whatToHit = target;
    }
    public void SetPierce(int p){
        Pierce = p;
    }
    public void SetDespawnTimer(int r){
        DespawnTimer = r;
        StartCoroutine(Disappear());
    }
    IEnumerator BeamDisappear(){
        for(int i = 0 ; i < BeamMaterial.Length ; i++){
            Bul.GetComponent<MeshRenderer>().material = BeamMaterial[i];
            yield return new WaitForSeconds(0.2f);
        }
        Destroy(gameObject);
    }
    IEnumerator Disappear(){
        while(true){
            yield return new WaitForSeconds(1.0f);
            DespawnTimer--;
            if(DespawnTimer == 0) Destroy(gameObject);
        }
    }
}
