using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class TypeWriter : MonoBehaviour
{
    [SerializeField] Text TextObj;
    [SerializeField] StringBuilder sb;
    [SerializeField] string text;
    [SerializeField] float delay;

    int i;

    void Start()
    { 
        i = 0;
    }

    void Update()
    {
               
    }

    IEnumerator TextAnimation()
    {
        sb = new StringBuilder();
        
        while (i < text.Length)
        {
            sb.Append(text[i]);
            TextObj.text = sb.ToString();
            ++i;

            yield return new WaitForSeconds(delay);
        }

        i = 0;
    }
}
