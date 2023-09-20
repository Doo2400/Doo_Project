using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Craft
{
    public string craftName; // �̸�.
    public GameObject go_Prefab; // ���� ��ġ�� ������.
    public GameObject go_PreviewPrefab; // �̸����� ������.
}

public class CraftManual : MonoBehaviour
{
    private bool isActivated = false;

    [SerializeField]
    private GameObject go_BaseUI; // �⺻ ���̽� UI

    [SerializeField]
    private Craft[] craft_fire; // ��ںҿ� ��.

    private GameObject go_Preview; // �̸����� �������� ���� ����.

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
            Window();
    }

    private void Window()
    {
        if (!isActivated)
        {
            OpenWindow();
        }
        else
        {
            CloseWindow();
        }
    }

    private void OpenWindow()
    {
        isActivated = true;
        go_BaseUI.SetActive(true);
    }

    private void CloseWindow()
    {
        isActivated = false;
        go_BaseUI.SetActive(false);
    }
}
