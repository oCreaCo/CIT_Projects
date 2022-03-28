using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] LayerMask mask;
    [SerializeField] Vector3 center;
    [SerializeField] float radius;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Collider[] targets = Physics.OverlapSphere(center, radius, mask);
    }
}
