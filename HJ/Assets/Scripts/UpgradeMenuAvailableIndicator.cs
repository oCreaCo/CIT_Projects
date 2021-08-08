using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeMenuAvailableIndicator : MonoBehaviour
{
    float down = -1.0f;
    IEnumerator moveCoroutine;
    [SerializeField] float moveSpeed;
    
    public void MoveCall(bool A){
        if(moveCoroutine != null) StopCoroutine(moveCoroutine);
        moveCoroutine = Move(A);
        if(!A) transform.GetComponent<RectTransform>().localPosition = new Vector3(0, 250, 0);
        StartCoroutine(moveCoroutine);
    }

    public IEnumerator Move(bool Disapper){
        if(Disapper) down = 1.0f;
        else down = -1.0f;
        float slowDownPace = moveSpeed/50.0f;
        float CurrentSpeed = moveSpeed;
        for(int i = 0 ; i < 50 ; i++){
            transform.GetComponent<RectTransform>().localPosition = new Vector3(0, transform.GetComponent<RectTransform>().localPosition.y + CurrentSpeed*down, 0);
            CurrentSpeed -= slowDownPace;
            yield return new WaitForSeconds(0.01f);
        }
        if(Disapper){
            transform.GetComponent<RectTransform>().localPosition = new Vector3(0, 250, 0);
        }
    }
}
