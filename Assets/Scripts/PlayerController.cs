using System;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //PLAYER COMPONENTS
    [Header("Components")]
    [SerializeField] private Transform m_transform;
    private Rigidbody2D m_rigidbody2D;
    private GatterInputs m_gatterInput;

    private Animator m_animator;

    //ANIMATOR IDS
    private int idSpeed;
    private int idIsGrounded;

    [Header("Move settings")]
    [SerializeField] private float speed;
    private int direction = 1;
    
    [Header("Jump settings")]
    [SerializeField] private float jumpForce;
    [SerializeField] private int extraJumps;
    [SerializeField] private int counterExtraJumps;
    [SerializeField] private bool canDoubleJumps;


    [Header("Ground settings")]
    [SerializeField] private Transform lFoot;
    [SerializeField] private Transform rFoot;
    RaycastHit2D lFootRay;
    RaycastHit2D rFootRay;
    [SerializeField] private bool isGrounded;
    [SerializeField] private float rayLength;
    [SerializeField] private LayerMask groundLayer;
    
    [Header("Wall settings")]
    [SerializeField] private float checkWallDistance;
    [SerializeField] private bool isWallDetected;

    private void Awake()
    {
        m_gatterInput = GetComponent<GatterInputs>();
        //m_transform = GetComponent<Transform>();
        m_rigidbody2D = GetComponent<Rigidbody2D>();
        m_animator = GetComponent<Animator>();
    }

    void Start()
    {
        idSpeed = Animator.StringToHash("Speed");
        idIsGrounded = Animator.StringToHash("IsGrounded");
        lFoot = GameObject.Find("LFoot").GetComponent<Transform>();
        rFoot = GameObject.Find("RFoot").GetComponent<Transform>();
        counterExtraJumps = extraJumps;
    }

    void Update()
    {
        SetAnimatorValues();
    }

    private void SetAnimatorValues()
    {
        m_animator.SetFloat(idSpeed, Mathf.Abs(m_rigidbody2D.velocityX));
        m_animator.SetBool(idIsGrounded, isGrounded);
    }

    void FixedUpdate()
    {
        CheckCollision();
        Move();
        Jump();
    }

    private void Move()
    {
        Flip();
        m_rigidbody2D.velocity = new Vector2(speed * m_gatterInput.ValueX, m_rigidbody2D.velocityY);
    }
    private void Flip()
    {
        if(m_gatterInput.ValueX * direction < 0)
        {
            m_transform.localScale = new Vector3(-m_transform.localScale.x, 1, 1);
            direction *= -1;
        }
    }
    private void Jump()
    {
        if (m_gatterInput.IsJumping)
        {
            if (isGrounded)
            {
                m_rigidbody2D.velocity = new Vector2(speed * m_gatterInput.ValueX, jumpForce);
                canDoubleJumps = true;
            }
            else if(counterExtraJumps > 0 && canDoubleJumps)
            {
                m_rigidbody2D.velocity = new Vector2(speed * m_gatterInput.ValueX, jumpForce);
                counterExtraJumps--;
            }
        }
        m_gatterInput.IsJumping = false;
    }

    private void CheckCollision()
    {
        HandleGround();
        HandleWall();
    }

    private void HandleWall()
    {
        isWallDetected = Physics2D.Raycast(m_transform.position, Vector2.right * direction, checkWallDistance, groundLayer);
    }

    private void HandleGround()
    {
        lFootRay = Physics2D.Raycast(lFoot.position, Vector2.down, rayLength, groundLayer);
        rFootRay = Physics2D.Raycast(rFoot.position, Vector2.down, rayLength, groundLayer);
        if (lFootRay || rFootRay)
        {
            isGrounded = true;
            counterExtraJumps = extraJumps;
            canDoubleJumps = false;
        }
        else
        {
            isGrounded = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(m_transform.position, new Vector2(m_transform.position.x +(checkWallDistance * direction), m_transform.position.y));
    }
}
