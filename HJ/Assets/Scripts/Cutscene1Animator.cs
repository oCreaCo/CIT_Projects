using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene1Animator : MonoBehaviour
{
    [SerializeField] Cutscene1Manager Manager;

    void Cutscene1End(){
        Manager.StartCoroutine(Manager.EndCutscene());
    }
}
