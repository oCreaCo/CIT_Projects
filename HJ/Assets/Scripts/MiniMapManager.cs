using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapManager : MonoBehaviour
{
    int isMapDownsized;


    void Start()
    {
        isMapDownsized = 1;
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (isMapDownsized == 0)
            {
                transform.position = new Vector3(transform.position.x, 10, transform.position.z);
                isMapDownsized++;
            }
            else if (isMapDownsized == 1)
            {
                transform.position = new Vector3(transform.position.x, 30, transform.position.z);
                isMapDownsized++;
            }
            else
            {
                transform.position = new Vector3(transform.position.x, 60, transform.position.z);
                isMapDownsized = 0;
            }
        }
    }
}
