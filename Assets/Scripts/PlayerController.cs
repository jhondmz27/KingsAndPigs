using System;
using UnityEditor.Tilemaps;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D m_rigidbody2D;
    private GatterInputs m_gatterInput;
    private Transform m_transform;
    [SerializeField] private float speed;
    private int direction = 1;

    void Start()
    {
        m_gatterInput = GetComponent<GatterInputs>();
        m_transform = GetComponent<Transform>();
        m_rigidbody2D = GetComponent<Rigidbody2D>();
    }


    void FixedUpdate()
    {
        Move();
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
}
