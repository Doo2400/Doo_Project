using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject bulletOwner;
    private float disappearTime;

    private void Start()
    {
        disappearTime = 0.5f;
    }

    private void OnEnable()
    {
        Invoke("Disappear", disappearTime);
    }

    void Disappear()
    {
        BackToMyObjPool();
    }

    private void OnTriggerEnter(Collider other)
    {
        BackToMyObjPool();
    }

    private void OnCollisionEnter(Collision collision)
    {
        BackToMyObjPool();
    }

    private void BackToMyObjPool()
    {
        gameObject.SetActive(false);
    }
}
