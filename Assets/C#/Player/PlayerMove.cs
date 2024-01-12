// ������������ ������
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Header("Move")]
    public float speed = 2f; // ��������

    [Header("Jump")]
    [Tooltip("���� ������")] public float jumpForce = 7;
    [SerializeField] int maxJumpValue = 2; // ������������ ���.�� �������
    int jumpCount = 0; // �������� ���������� �������

    [Header("Jerk")]
    [Tooltip("���� �����")]
    [SerializeField] int lungeInpulse = 5000;
    [SerializeField] KeyCode jerkClic;
    bool lockLunge = false;

    [Header("PLatform")]
    [SerializeField] KeyCode downArrowClic;

    [Header("Option")]
    [SerializeField] Rigidbody2D rb;
    Vector2 moveVector;
    [SerializeField] SpriteRenderer sr;
    [SerializeField] Animator anim;

    [Header("Parameters")]
    [SerializeField] bool onGraund; // ���� �� �����
    [SerializeField] Transform GraundCheck; // ����� ����������� �������� �� ����� ����� 
    [SerializeField] float checkRadius = 0.5f; // ������ �������� �����
    [SerializeField] LayerMask Graund; // ���� �����
    [SerializeField] Finish finish;

    bool faceRight = true;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        walk();
        Reflect();
        Jump();
        ChekingGround();
        Lunge();
    }

    void Reflect()
    {
        if (moveVector.x > 0 && !faceRight || (moveVector.x < 0 && faceRight))
        {
            transform.localScale *= new Vector2(-1, 1);
            faceRight = !faceRight;
        }
    }// ������� ������
    void walk()
    {
        moveVector.x = Input.GetAxis("Horizontal");
        anim.SetFloat("moveX", Mathf.Abs(moveVector.x));
        rb.velocity = new Vector2(moveVector.x * speed, rb.velocity.y); // ������������ - ������
        //rb.AddForce(moveVector * speed); // ������������ + ������
    }// ������
    void Jump()
    {
        if (Input.GetKeyDown(downArrowClic))
        {
            Physics2D.IgnoreLayerCollision(6, 7, true);
            Invoke("IgnoreLayerOff", 0.5f);
            finish.nDownPlatform += 1;
        }

        if (Input.GetKeyDown(KeyCode.Space) && (onGraund || (++jumpCount < maxJumpValue)))
        {
            finish.nJump += 1;
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(Vector2.up * jumpForce);
        }

        if (onGraund) { jumpCount = 0; }
    }// ������
    void Lunge()
    {
        if (Input.GetKeyDown(jerkClic) && !lockLunge)
        {
            lockLunge = true;
            Invoke("LockLunge", 3);
            rb.velocity = new Vector2(0, 0);
            finish.nLunge += 1;
            if (!faceRight)
            {
                rb.AddForce(Vector2.left * lungeInpulse);
            }
            else
            {
                rb.AddForce(Vector2.right * lungeInpulse);
            }
        }
    }// �����


    void ChekingGround()
    {
        onGraund = Physics2D.OverlapCircle(GraundCheck.position, checkRadius, Graund);
        anim.SetBool("isGround", onGraund);
    } // �������� �� ���������� ������ �� �����
    void IgnoreLayerOff()
    {
        Physics2D.IgnoreLayerCollision(6, 7, false);
    } // ����� ������������� ����
    void LockLunge()
    {
        lockLunge = false;
    } // ����� ������������� �����
}
