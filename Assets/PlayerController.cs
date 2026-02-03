using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 5f;
    public float jumpForce = 7f;

    [Header("UI")]
    public TMP_Text messageText; // текст на екрані

    private Rigidbody2D rb;
    private Vector3 startPosition;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPosition = transform.position;

        // Автоматичне підключення TextMeshPro
        if (messageText == null)
        {
            GameObject tmpObj = GameObject.Find("MessageText");
            if (tmpObj != null)
                messageText = tmpObj.GetComponent<TMP_Text>();
        }

        if (messageText != null)
            messageText.text = ""; // очищаємо на старті
    }

    void Update()
    {
        Move();
        Jump();
    }

    void Move()
    {
        float moveX = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(moveX * speed, rb.linearVelocity.y);
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            isGrounded = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains("bushes-0"))
        {
            Respawn();
            if (messageText != null)
                messageText.text = "Game Over!";
        }
        else if (collision.gameObject.name.Contains("stones-2"))
        {
            if (messageText != null)
                messageText.text = "You Win!";
        }
    }

    void Respawn()
    {
        transform.position = startPosition;
        rb.linearVelocity = Vector2.zero;
    }
}
