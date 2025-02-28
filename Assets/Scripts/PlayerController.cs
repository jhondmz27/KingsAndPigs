using System;
using UnityEditor.Tilemaps;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //COMPONENTS
    private Rigidbody2D m_rigidbody2D;
    private GatterInputs m_gatterInput;
    private Transform m_transform;
    private Animator m_animator;

    //VALUES
    [SerializeField] private float speed;
    private int direction = 1;
    private int IdSpeed;
    [SerializeField] private float jumpForce;

    [SerializeField] private Transform lFoot, rFoot;
    [SerializeField] private bool isGrounded;
    [SerializeField] private float rayLegnth;
    [SerializeField] private LayerMask groundLayer;


    void Start()
    {
        m_gatterInput = GetComponent<GatterInputs>();
        m_transform = GetComponent<Transform>();
        m_rigidbody2D = GetComponent<Rigidbody2D>();
        m_animator = GetComponent<Animator>();
        IdSpeed = Animator.StringToHash("Speed");
        lFoot = GameObject.Find("LFoot").GetComponent<Transform>();
        rFoot = GameObject.Find("RFoot").GetComponent<Transform>();
    }

    void Update()
    {
        SetAnimatorValues();
    }

    private void SetAnimatorValues()
    {
        m_animator.SetFloat(IdSpeed, Mathf.Abs(m_rigidbody2D.velocityX));
    }

    void FixedUpdate()
    {
        Move();
        Jump();
        CheckGround();
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
            if(isGrounded)
                m_rigidbody2D.velocity = new Vector2(speed * m_gatterInput.ValueX, jumpForce);
        }
        m_gatterInput.IsJumping = false;
    }

    private void CheckGround()
    {
        RaycastHit2D lFootRay = Physics2D.Raycast(lFoot.position, Vector2.down, rayLegnth,groundLayer);
        RaycastHit2D rFootRay = Physics2D.Raycast(rFoot.position, Vector2.down, rayLegnth,groundLayer);
        if(lFootRay || rFootRay)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }
}
