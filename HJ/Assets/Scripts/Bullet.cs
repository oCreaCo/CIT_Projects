using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] GameObject Bul;

    [SerializeField] int bulletDamage;
    [SerializeField] float bulletSpeed;
    [SerializeField] bool Beam = false;
    [SerializeField] Material[] BeamMaterial;
    string whatToHit = " ";
    int Pierce = 0;
    int DespawnTimer = 0;

    void Start()
    {
        if(Beam) StartCoroutine(BeamDisapper());
    }


    void Update()
    {
        transform.Translate(Vector3.forward * bulletSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(Beam == false){
            if(other.tag == "Untagged" || other.tag == "Obstacles")
            {
                Destroy(gameObject);
            }
            else if(other.tag == whatToHit)
            {
                if(whatToHit == "Enemy") other.transform.GetComponent<Enemy>().EnemyGetDamage(bulletDamage);
                else if(whatToHit == "Player") other.transform.GetComponent<PlayerScript>().GetDamage(bulletDamage);
                if(Pierce == 0) Destroy(gameObject);
                Pierce--;
            }
            
        }
        else{
            if(other.tag == whatToHit){
                if(whatToHit == "Enemy") other.transform.GetComponent<Enemy>().EnemyGetDamage(bulletDamage);
                else if(whatToHit == "Player") other.transform.GetComponent<PlayerScript>().GetDamage(bulletDamage);
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
        StartCoroutine(Disapper());
    }
    IEnumerator BeamDisapper(){
        for(int i = 0 ; i < BeamMaterial.Length ; i++){
            Bul.GetComponent<MeshRenderer>().material = BeamMaterial[i];
            yield return new WaitForSeconds(0.2f);
        }
        Destroy(gameObject);
    }
    IEnumerator Disapper(){
        while(true){
            yield return new WaitForSeconds(1.0f);
            DespawnTimer--;
            if(DespawnTimer == 0) Destroy(gameObject);
        }
    }
}
