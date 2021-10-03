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
    int Pierce = 0;

    void Start()
    {
        if(Beam) StartCoroutine(BeamDisapper());
    }


    void Update()
    {
        if(Beam==false) transform.Translate(Vector3.forward * bulletSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(Beam == false){
            if(other.tag == "Untagged" || other.tag == "Obstacles")
            {
                Destroy(gameObject);
            }
            else if(other.tag == "Enemy")
            {
                other.transform.GetComponent<Enemy>().EnemyGetDamage(bulletDamage);
                if(Pierce == 0) Destroy(gameObject);
                Pierce--;
            }
            else if(other.tag == "Player")
            {
                other.transform.GetComponent<PlayerScript>().GetDamage(bulletDamage);
            }
        }
        else{
            if(other.tag == "Enemy"){
                other.transform.GetComponent<Enemy>().EnemyGetDamage(bulletDamage);           
            }
        }
    }
    public void SetBulletDetails(int damage, float speed){
        bulletDamage = damage;
        bulletSpeed = speed;
    }
    public void SetPierce(int p){
        Pierce = p;
    }
    IEnumerator BeamDisapper(){
        for(int i = 0 ; i < BeamMaterial.Length ; i++){
            Bul.GetComponent<MeshRenderer>().material = BeamMaterial[i];
            yield return new WaitForSeconds(0.2f);
        }
        Destroy(gameObject);
    }
}
