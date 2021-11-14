using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class Npc : MonoBehaviour
{
    bool isNearAtNpc;
    bool isTalking;

    [SerializeField] Text textObj;
    [SerializeField] List<string> text; 
    [SerializeField] GameObject player;
    [SerializeField] StringBuilder sb;
    //[SerializeField] string text;
    [SerializeField] float delay;

    void Start()
    {
        isTalking = false;
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isNearAtNpc && !isTalking)
            {
                StartCoroutine("TextAnimation");
            }
        }

        float y = Quaternion.LookRotation(textObj.transform.position - player.transform.position).eulerAngles.y;
        textObj.transform.rotation = Quaternion.Euler(0, y, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            transform.FindChild("Canvas").gameObject.SetActive(true);
            isNearAtNpc = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            transform.FindChild("Canvas").gameObject.SetActive(false);
            isNearAtNpc = false;

            if (isTalking)
                StopCoroutine("TextAnimation");
            textObj.text = "Press E to talk.";
            isTalking = false;
        }
    }

    IEnumerator TextAnimation()
    {
        isTalking = true;

        for (int i = 0; i < text.Count; ++i)
        {
            sb = new StringBuilder();

            for (int j = 0; j < text[i].Length; ++j)
            {
                sb.Append(text[i][j]);
                textObj.text = sb.ToString();

                yield return new WaitForSeconds(delay);
            }
            
            yield return new WaitForSeconds(delay + 0.5f);
        }

        isTalking = false;
        textObj.text = "Press E to talk again.";
    }
}