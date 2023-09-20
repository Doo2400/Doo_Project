using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInventoryGUI : MonoBehaviour
{
    Vector3 currentMousePosition;

    public void WhenDragging()
    {
        currentMousePosition = Input.mousePosition;
        transform.position = new Vector3(currentMousePosition.x, currentMousePosition.y, 0);
    }
}
