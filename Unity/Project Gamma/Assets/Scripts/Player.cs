using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public GameManager gm;
    public Button useBtn;
    public bool isUseBtnPressed;

    public float hp;
    public float maxHp;
    public float ap;            //Armor Point
    public float maxAP;

    public Text hpText;
    public Text apText;

    public Image whenDamagedRedScreen;

    private void Start()
    {
        gm.players.Add(gameObject);
        isUseBtnPressed = false;

        hpAndAPUpdate();
    }

    private void Update()
    {
        CheckUseBtnDowned();
    }

    public void hpAndAPUpdate()
    {
        hpText.text = "HP : " + hp;
        apText.text = "AP : " + ap;
    }

    public void CheckUseBtnDowned()
    {
        if (useBtn.GetComponent<UseBtn>().isPressed == true)
        {
            isUseBtnPressed = true;
        }
        else
        {
            isUseBtnPressed = false;
        }
    }

    public void GetDamageFromEnemy(int hitPower)
    {
        WDRCBlink();

        if (ap > 0)
        {
            if (ap - hitPower > 0)
            {
                ap -= hitPower;
            }
            else //else if(ap - hitPower < 0)
            {
                hp = hp - Mathf.Abs(ap - hitPower);
                ap = 0;
            }
        }
        else
        {
            hp -= hitPower;
        }

        hpAndAPUpdate();
    }

    public void Heal(int healAmount)
    {
        if (hp + healAmount > maxHp)
        {
            hp = maxHp;
        }
        else
        {
            hp = hp + healAmount;
        }

        hpAndAPUpdate();
    }

    public void EquipArmor()
    {
        ap = maxAP;

        hpAndAPUpdate();
    }

    public void WDRCBlink()
    {
        if (whenDamagedRedScreen.IsActive() == false)
        {
            whenDamagedRedScreen.gameObject.SetActive(true);
            StartCoroutine(WDRCBlinkProcess());
        }
    }
    IEnumerator WDRCBlinkProcess()
    {
        yield return new WaitForSeconds(0.2f);
        whenDamagedRedScreen.gameObject.SetActive(false);
    }
}
