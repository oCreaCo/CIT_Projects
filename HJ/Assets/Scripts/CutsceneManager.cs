using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneManager : MonoBehaviour
{
    [SerializeField] Animator Anim;
    [SerializeField] SceneManage SceneMove;
    
    public IEnumerator Scene1(){
        Anim.SetTrigger("Scene1");
        yield return new WaitForSeconds(1.0f);
    }
}
