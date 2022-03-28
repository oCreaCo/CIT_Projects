using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager uiManager;

    [SerializeField] GameObject escMenu;

    [SerializeField] GameObject settingsMenu;
    [SerializeField] GameObject mouseSensitiveInput;

    [SerializeField] GameObject keyGuideMenu;

    public bool isEscMenuActived;

    void Start()
    {
        UIManager.uiManager = this;
        isEscMenuActived = false;
        mouseSensitiveInput.GetComponent<InputField>().text = "10";
    }

    [System.Obsolete]
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (keyGuideMenu.activeSelf)
            {
                keyGuideMenu.SetActive(false);
                escMenu.SetActive(true);
            }

            if (settingsMenu.activeSelf)
            {
                if (mouseSensitiveInput.transform.GetChild(3).GetComponent<Text>().color != Color.red)
                {
                    escMenu.SetActive(true);
                    settingsMenu.SetActive(false);
                }
            }

            else if (!isEscMenuActived)
            {
                Time.timeScale = 0.0f;
                escMenu.SetActive(true);
                isEscMenuActived = true;
            }
            else
            {
                Time.timeScale = 1.0f;
                escMenu.SetActive(false);
                isEscMenuActived = false;
            }
        }

        if (settingsMenu.activeSelf)
        {
            float mouseSensitive;

            if (float.TryParse(mouseSensitiveInput.GetComponent<InputField>().textComponent.text, out mouseSensitive))
            {
                PlayerScript.playerScript.CameraSensivity = mouseSensitive;
                mouseSensitiveInput.transform.Find("Title").GetComponent<Text>().color = Color.white;
            }
            else
            {
                //mouseSensitiveInput.GetComponent<InputField>().text = "10.0";
                mouseSensitiveInput.transform.Find("Title").GetComponent<Text>().color = Color.red;
            }
        }
    }


    public void GetButtonSettingsMenu()
    {
        escMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    public void GetButtonResume()
    {
        Time.timeScale = 1.0f;
        escMenu.SetActive(false);
        isEscMenuActived = false;
    }

    public void getButtonKeyGuide()
    {
        escMenu.SetActive(false);
        keyGuideMenu.SetActive(true);
    }
}
