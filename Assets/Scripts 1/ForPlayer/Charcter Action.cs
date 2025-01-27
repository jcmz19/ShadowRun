using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.InputSystem;
using System.Linq;

/// <summary>
/// ��ɫ�ƶ��߼�
/// ���ṩ��̬
/// </summary>
public class CharcterAction : MonoBehaviour
{
    public Camera mainCamera;
    [Header("��ɫ��ǰ�ƶ��ٶ�")]
    public float moveForwardSpeed;
    [Header("��ɫ����ƶ��ٶ�")]
    public float moveBackSpeed;
    [Header("��ɫ�̶��ƶ��ٶ�")]
    public float fixSpeed;
    [Header("��ɫ��Ծ���ʱ��")]
    public float jumpMaxTime;
    [Header("��ɫ��Ծ����")]
    public float jumpHeight;


    private Rigidbody2D rb;
    private Collider2D cd;
    Animator ani;
    SpriteRenderer targetRenderer;
    CharacterInputSystem inputActions;
    private float jumpingTime;
    private bool falling;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        cd = GetComponent<Collider2D>();
        targetRenderer = GetComponent<SpriteRenderer>();
        inputActions = new CharacterInputSystem();
        jumpingTime = 0.0f;
        falling = false;
    }


    // Update is called once per frame
    void Update()
    {

        GetMoveState();
        GetJumpState();

    }

   

    private void GetMoveState() 
    {
        //�����ƶ�����
        float moveX = Input.GetAxis("Horizontal");

        Vector2 moveDir = new Vector2(moveX, 0).normalized;
        rb.velocity = new Vector2(1, 0) * fixSpeed;

        //������ǰ�����ļ��ٶ�
        if(moveDir.x > 0)
        {
            rb.AddForce(moveDir * (moveForwardSpeed - fixSpeed), ForceMode2D.Impulse);
        }
        else if(moveDir.x < 0)
        {
            rb.AddForce(moveDir * (moveBackSpeed + fixSpeed), ForceMode2D.Impulse);
        }
        

        
        if (rb.velocity.sqrMagnitude > 0.01) ani.SetBool("Move", true);
        else ani.SetBool("Move", false);

        //��ת
        if (rb.velocity.x < 0)
        {
            targetRenderer.flipX = true;
        }
        else
        {
            targetRenderer.flipX = false;
        }

    }

    private void GetJumpState()
    {
        if (OnGround())
        {
            //Debug.Log("on");
            ani.SetTrigger("StopFalling");

            falling = false;
        }
        float jump = Input.GetAxis("Jump");
        if (jump >= 0.1 && !falling)
        {

         
            if(!OnGround())
            {
                jumpingTime += Time.deltaTime;//����������Ծ�У�������Ծ��ʱ����Ӱ��׹��
               
                if (jumpingTime >= jumpMaxTime)
                {
                    jumpingTime = 0;
                    falling = true;
                    ani.SetTrigger("Fall");
                    return;
                }
            }

            ani.SetTrigger("Jump");
            rb.AddForce(new Vector2(0.0f, 1) * jumpHeight, ForceMode2D.Impulse);
            
            //Debug.Log(jumpingTime);
        }
        else if (jump <= 0 && (!OnGround()))
        {
            falling = true;
            ani.SetTrigger("Fall");
        }
    }

   

    private bool OnGround()
    {
        return cd.IsTouchingLayers(LayerMask.GetMask("Collider"));
    } 

}



