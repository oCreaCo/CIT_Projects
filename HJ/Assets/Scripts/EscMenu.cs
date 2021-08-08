using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscMenu : MonoBehaviour
{
    [SerializeField] GameObject UI;
    [SerializeField] GameObject Menu;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
        }
    }
}