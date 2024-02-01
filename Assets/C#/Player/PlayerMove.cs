// передвежение игрока
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;

public class PlayerMove : MonoBehaviour
{
    [Header("Move")]
    public float speed = 2f; // Скорость

    [Header("Jump")]
    [Tooltip("Сила прыжка")] public float jumpForce = 7;
    [SerializeField] int maxJumpValue = 2; // максимальное Кол.во прыжков
    int jumpCount = 0; // реальное количество прыжков

    [Header("Dash")]
    [Range(0, 3)] public int countDash;
    public List<GameObject> dashList;
    [SerializeField] float timeRecovery;

    [Tooltip("Сила рывка")]
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
    public bool onGraund; // Если на земле
    [SerializeField] Transform GraundCheck; // Обект проверяюший косается ли игрок земли 
    [SerializeField] float checkRadius = 0.5f; // радиус проверки земли
    [SerializeField] LayerMask Graund; // Слой земли
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
    }// Поворот игрока
    void walk()
    {
        moveVector.x = Input.GetAxis("Horizontal");
        anim.SetFloat("moveX", Mathf.Abs(moveVector.x));
        rb.velocity = new Vector2(moveVector.x * speed, rb.velocity.y); // передвижение - физика
        //rb.AddForce(moveVector * speed); // передвижение + физика
    }// ходьба
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
    }// прыжок
    void Lunge()
    {
        if (Input.GetKeyDown(jerkClic) && !lockLunge)
        {
            if (countDash > 0)
            {
                CountDashAnim();
                lockLunge = true;
                Invoke("LockLunge", 3);
                rb.velocity = new Vector2(0, 0);
                finish.nLunge += 1;
                if (!faceRight) { rb.AddForce(Vector2.left * lungeInpulse); }
                else { rb.AddForce(Vector2.right * lungeInpulse); }
            }
        }
    }// рывок

    void CountDashAnim()
    {
        countDash--;
        DOTween.Sequence()
            .Append(dashList[countDash].transform.GetChild(0).GetComponent<Image>().DOFillAmount(0f, timeRecovery));
    }


    void ChekingGround()
    {
        onGraund = Physics2D.OverlapCircle(GraundCheck.position, checkRadius, Graund);
        anim.SetBool("isGround", onGraund);
    } // Проверка на нахождение игрока на земле
    void IgnoreLayerOff()
    {
        Physics2D.IgnoreLayerCollision(6, 7, false);
    } // Метод Игнорирования слоёв
    void LockLunge()
    {
        lockLunge = false;
    } // Метод востановление рывка
}
