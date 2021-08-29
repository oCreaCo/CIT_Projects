using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager uiManager;

    void Start()
    {
        UIManager.uiManager = this;
    }

    void Update()
    {

    }
}
