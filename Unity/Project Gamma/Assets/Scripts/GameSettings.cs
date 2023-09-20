using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameSettings : MonoBehaviour
{
    public Button btn30FPS;
    public Button btn60FPS;
    public Button btn720P;
    public Button btn1080P;

    public Slider sldBgmVolume;
    public Slider sldFxVolume;
    public Slider sldRotSpeed;

    private void Start()
    {
        if (PlayerPrefs.HasKey("currentFPS") == true)
        {
            if (PlayerPrefs.GetInt("currentFPS") == 30)
            {
                OnClickBtn30FPS();
            }
            else
            {
                OnClickBtn60FPS();
            }
        }
        else
        {
            OnClickBtn60FPS();
        }

        if (PlayerPrefs.HasKey("currentScrrenSize") == true)
        {
            if (PlayerPrefs.GetInt("currentScrrenSize") == 720)
            {
                OnClickBtn720P();
            }
            else
            {
                OnClickBtn1080P();
            }
        }
        else
        {
            OnClickBtn1080P();
        }

        if (PlayerPrefs.HasKey("BgmVolume") == true)
        {
            SetSliderValueFromPrefeb("BgmVolume", sldBgmVolume);
        }
        else
        {
            sldBgmVolume.value = 0.5f;
        }

        if (PlayerPrefs.HasKey("FxVolume") == true)
        {
            SetSliderValueFromPrefeb("FxVolume", sldFxVolume);
        }
        else
        {
            sldFxVolume.value = 0.5f;
        }

        if (PlayerPrefs.HasKey("RotSpeed") == true)
        {
            SetSliderValueFromPrefeb("RotSpeed", sldRotSpeed);
        }
        else
        {
            sldRotSpeed.value = 0.5f;
        }
    }

    public void SetSliderValueFromPrefeb(string prefebKey, Slider slider)
    {
        slider.value = PlayerPrefs.GetFloat(prefebKey);
    }

    public void SavePrefebsFromSlideValue()
    {
        PlayerPrefs.SetFloat("BgmVolume", sldBgmVolume.value);
        PlayerPrefs.SetFloat("FxVolume", sldFxVolume.value);
        PlayerPrefs.SetFloat("RotSpeed", sldRotSpeed.value);
    }

    public void SetFrameRate(int secondPerFrame)
    {
        Application.targetFrameRate = secondPerFrame;
        PlayerPrefs.SetInt("currentFPS", secondPerFrame);
    }

    public void SetResolution(int screenHeight)
    {
        Screen.SetResolution((screenHeight * 16) / 9, screenHeight, true);
        PlayerPrefs.SetInt("currentScrrenSize", screenHeight);
    }

    public void OnClickBtn30FPS()
    {
        btn30FPS.interactable = false;
        btn60FPS.interactable = true;
        SetFrameRate(30);
    }
    public void OnClickBtn60FPS()
    {
        btn30FPS.interactable = true;
        btn60FPS.interactable = false;
        SetFrameRate(60);
    }
    public void OnClickBtn720P()
    {
        btn720P.interactable = false;
        btn1080P.interactable = true;
        SetResolution(720);
    }
    public void OnClickBtn1080P()
    {
        btn720P.interactable = true;
        btn1080P.interactable = false;
        SetResolution(1080);
    }

    public void OnClickBackToMenu()
    {
        SavePrefebsFromSlideValue();
        SceneManager.LoadScene("MainTitle");
    }
}
