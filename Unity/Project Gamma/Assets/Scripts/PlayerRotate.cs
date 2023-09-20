using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerRotate : MonoBehaviour
{
    public Image dragInputField;
    public Camera mainCamera;

    public float rotateSpeed;
    private bool isDragging;
    private uint dragCount;

    Vector3 dragOrigin;
    Vector3 currentMousePosition;

    private void Start()
    {

    }

    private void Update()
    {
        if (isDragging == true)
        {
            whenDragging();
        }
    }

    public void whenStartDrag()
    {
        isDragging = true;
        dragOrigin = Input.mousePosition;
    }

    public void whenEndDrag()
    {
        dragCount--;

        isDragging = false;
    }

    public void whenDragging()
    {
        if (isDragging == true)
        {
            currentMousePosition = Input.mousePosition;
            float deltaX = currentMousePosition.x - dragOrigin.x;
            float deltaY = currentMousePosition.y - dragOrigin.y;

            transform.Rotate(0, deltaX * rotateSpeed * Time.deltaTime, 0);

            float newYAngle = mainCamera.transform.eulerAngles.x - (deltaY * rotateSpeed * Time.deltaTime);
            mainCamera.transform.rotation = Quaternion.Euler(newYAngle, transform.eulerAngles.y, 0);

            dragOrigin = currentMousePosition;
        }
    }
}
