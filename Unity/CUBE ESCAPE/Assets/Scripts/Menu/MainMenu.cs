using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject startMenu;
    public GameObject loginMenu;
    public GameObject mainMenu;
    public GameObject optionMenu;
    public GameObject loadMenu;

    public void OnClick_StartMenu_Login()
    {
        loginMenu.SetActive(true);
    }

    public void OnClick_LoginMenu_Login()
    {
        loginMenu.SetActive(false);
        startMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void OnClick_LoginMenu_Exit()
    {
        loginMenu.SetActive(false);
    }

    public void OnClick_NewGame()
    {
        SceneManager.LoadScene("Main");
    }

    public void OnClick_Load()
    {
        loadMenu.SetActive(true);
    }

    public void OnClick_LoadMenu_Load()
    {
        
    }

    public void OnClick_LoadMenu_Exit()
    {
        loadMenu.SetActive(false);
    }

    public void OnClick_Option()
    {
        optionMenu.SetActive(true);
    }

    public void OnClick_OptionMenu_Set()
    {

    }

    public void OnClick_OptionMenu_Exit()
    {
        optionMenu.SetActive(false);
    }

    public void OnClickQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
