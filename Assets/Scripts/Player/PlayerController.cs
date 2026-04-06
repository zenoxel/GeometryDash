using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 8f;
    public float jumpForce = 14f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float     groundCheckRadius = 0.15f;
    public LayerMask groundLayer;

    private Rigidbody2D _rb;
    private bool        _isGrounded;
    private bool        _isDead;
    private Vector3     _startPos;

    void Awake()
    {
        _rb       = GetComponent<Rigidbody2D>();
        _startPos = transform.position;
    }

    public void ResetPlayer()
    {
        _isDead             = false;
        _rb.velocity        = Vector2.zero;
        _rb.angularVelocity = 0f;
        transform.position  = _startPos;
        transform.rotation  = Quaternion.identity;
        gameObject.SetActive(true);
    }

    void Update()
    {
        if (_isDead || !GameManager.Instance.IsPlaying) return;

        transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);

        _isGrounded = Physics2D.OverlapCircle(
            groundCheck.position, groundCheckRadius, groundLayer);

        bool jump = Input.GetMouseButtonDown(0)
                 || Input.GetKeyDown(KeyCode.Space);

        if (jump && _isGrounded)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, jumpForce);
            SFXManager.Instance?.PlayJump();
        }

        if (!_isGrounded)
            transform.Rotate(0, 0, -400f * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (_isDead) return;
        if (other.CompareTag("Spike")) Die();
        if (other.CompareTag("Win"))   GameManager.Instance?.OnPlayerWin();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (_isDead) return;
        if (!col.gameObject.CompareTag("Ground")) return;
        foreach (var c in col.contacts)
        {
            if (Mathf.Abs(c.normal.x) > 0.7f) { Die(); return; }
        }
    }

    void Die()
    {
        _isDead = true;
        gameObject.SetActive(false);
        GameManager.Instance?.OnPlayerDied();
    }
}