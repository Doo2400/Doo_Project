using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerMove : MonoBehaviour
{
    CharacterController cc;

    public float moveSpeed;
    public float jumpPower;
    public float rotateSpeed;

    float gravity = -15f;
    public float yVelocity = 0;
    public bool isJumping;

    public bool isDragging;
    public Vector3 dragOrigin;

    public float pushPower = 2.0F;
    public Button jumpBtn;
    Animator playerAni;

    private void Start()
    {
        cc = GetComponent<CharacterController>();
        isJumping = false;
        isDragging = false;
        playerAni = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        Move();
        //Jump();   점프는 캔버스상의 점프버튼의 OnAction으로 호출됨.
    }

    public void Move()
    {
        float h = SimpleInput.GetAxis("Horizontal");   //Input.GetAxis("Horizontal");
        float v = SimpleInput.GetAxis("Vertical");     //Input.GetAxis("Vertical");

        Vector3 dir = new Vector3(h, 0, v);
        dir = dir.normalized;

        dir = Camera.main.transform.TransformDirection(dir);

        if (isJumping && cc.collisionFlags == CollisionFlags.Below)
        {
            isJumping = false;

            yVelocity = 0;
        }

        yVelocity += gravity * Time.deltaTime;
        dir.y = yVelocity;
        cc.Move(dir * moveSpeed * Time.deltaTime);

        playerAni.SetFloat("xDir", h);
        playerAni.SetFloat("zDir", v);
    }

    public void Jump()
    {
        if(isJumping == false)
        {
            yVelocity = jumpPower;
            isJumping = true;
            cc.Move(Vector3.up * jumpPower * Time.deltaTime);
            playerAni.SetTrigger("Jump");
        }
    }

    /*public void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.CompareTag("Enemy"))
        {
            Debug.Log($"OnControllerColliderHit - {hit.collider.name}");
        }
    }*/
}