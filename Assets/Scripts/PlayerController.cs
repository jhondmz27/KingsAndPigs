using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D m_rigidbody2D;
    private GatterInputs m_gatterInput;
    private Transform m_transform;
    [SerializeField] private float speed;

    void Start()
    {
        m_gatterInput = GetComponent<GatterInputs>();
        m_transform = GetComponent<Transform>();
        m_rigidbody2D = GetComponent<Rigidbody2D>();
    }


    void FixedUpdate()
    {
        m_rigidbody2D.velocity = new Vector2(speed * m_gatterInput.ValueX, m_rigidbody2D.velocityY);
    }
}
