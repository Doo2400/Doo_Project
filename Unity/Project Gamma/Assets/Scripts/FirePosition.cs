using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePosition : MonoBehaviour
{
    private void Update()
    {
        Vector3 camPos = Camera.main.transform.position;
        Ray ray = new Ray(camPos, Camera.main.transform.forward);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo))
        {
            //Debug.Log("충돌한 객체: " + hitInfo.collider.gameObject.name);
            //Debug.Log("충돌 지점: " + hitInfo.point);

            gameObject.transform.LookAt(hitInfo.point);
        }
    }
}
