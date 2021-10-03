using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManage : MonoBehaviour
{
    [SerializeField] int Destination;

    public void SceneChange(){
        SceneManager.LoadScene(Destination);
    }
}