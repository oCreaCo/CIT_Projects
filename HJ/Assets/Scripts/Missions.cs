using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missions : MonoBehaviour
{
    [SerializeField] GameObject Marker;


    Transform _pos;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void SummonMarker(float x, float y, float z)
    {
        _pos.position = new Vector3(x, y, z);

        Instantiate(Marker, _pos);
    }
}
