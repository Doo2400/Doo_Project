using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    public bool isDisplayMenu;

    public Image bg1;       //Two Title Heroine
    public Image bg2;       //UnityEngine Logo
    public Image bg3;       //GameLogo

    public Image pressToStartMessage;   // "TAB TO START"
    public Image recieveInputField;

    public float blinkSpeed = 1f;
    private float currentAlpha = 1f;
    private bool ttsIncreasing = false;
    public Button GameStartBtn;
    public Button SettingsBtn;
    public Button ExitBtn;
    public bool isSettingsMode;
    public GameObject Settings;


    private void Start()
    {
        isDisplayMenu = false;
    }

    private void Update()
    {
        switch (isDisplayMenu)
        {
            case true:
                WhenDisplayMenu();
                break;
            case false:
                NotWhenDisplayMenu();
                break;
        }
    }

    private void WhenDisplayMenu()
    {

    }
    private void NotWhenDisplayMenu() // Tab to start 버튼 알파값 점멸 코드
    {
        if (ttsIncreasing)
        {
            currentAlpha += 0.015f;
            if (currentAlpha >= 1f)
            {
                ttsIncreasing = false;
            }
        }
        else
        {
            currentAlpha -= 0.015f;
            if (currentAlpha <= 0f)
            {
                ttsIncreasing = true;
            }
        }

        Color color = pressToStartMessage.color;
        color.a = currentAlpha;
        pressToStartMessage.color = color;
    }


    public void WhenTabScreen()     // Only Called When touch PTSM EventTrigger
    {
        if (isDisplayMenu == false)
        {
            isDisplayMenu = true;

            pressToStartMessage.gameObject.SetActive(false);
            recieveInputField.gameObject.SetActive(false);

            GameStartBtn.gameObject.SetActive(true);
            SettingsBtn.gameObject.SetActive(true);
            ExitBtn.gameObject.SetActive(true);
        }
    }

    public void WhenClickGameStartBtn()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void WhenClickGameSetupBtn()
    {
        isSettingsMode = !isSettingsMode;

        if (isSettingsMode == true)
        {
            Settings.SetActive(true);
        }
        else
        {
            Settings.SetActive(false);
        }
    }

    public void WhenClickExitGameBtn()
    {
        Application.Quit();
    }
}
