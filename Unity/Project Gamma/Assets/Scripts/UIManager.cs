using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    public Button shootingBtn;
    public Button aimsightBtn;
    public Button useBtn;
    public Button jumpBtn;
    public Button reloadBtn;
    public Button menuBtn;
    public Button itemInventoryBtn;
    public GameObject joystick;
    public Text bulletText;

    public Image itemInventory;
    public Image gameSettings;

    public void OnClickShootingBtn()
    {
        
    }
    public void OnClickAimsightBtn()
    {
        
    }
    public void OnClickUseBtn()
    {
        
    }
    public void OnClickJumpBtn()
    {
        
    }
    public void OnClickReloadBtn()
    {
        
    }
    public void OnClickMenuBtn()
    {
        if(gameSettings.IsActive() == false)
        {
            gameSettings.gameObject.SetActive(true);
        }
        else
        {
            gameSettings.gameObject.SetActive(false);
        }
    }
    public void OnClickItemInventoryBtn()
    {
        if(itemInventory.IsActive() == false)
        {
            itemInventory.gameObject.SetActive(true);
        }
        else
        {
            itemInventory.gameObject.SetActive(false);
        }
    }

}
