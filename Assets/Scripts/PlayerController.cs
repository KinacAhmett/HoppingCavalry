using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour

{
    public Animator animator;
    float horizontalMove = 0f;
    public float runSpeed = 4f;

    [SerializeField] private float _jumpForce = 13f;
    [SerializeField] private float _movementSpeed = 5f;
    [SerializeField] private float _maxDistanceFromCameraBeforeDeath = 5f;

    private Rigidbody2D _rigidbody;
    private InputAction _moveInputAction;


    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _moveInputAction = InputSystem.actions.FindAction("Move");
    }

    void Update()
    {
        // Ölüm kontrolü
        if (transform.position.y < Camera.main.transform.position.y - _maxDistanceFromCameraBeforeDeath)
        {
            gameObject.SetActive(false); // Oyuncu nesnesini devre dışı bırak
        }
    }

    private void FixedUpdate()
    {
        Vector2 moveInput = _moveInputAction.ReadValue<Vector2>();
        _rigidbody.linearVelocity = new Vector2(moveInput.x * _movementSpeed, _rigidbody.linearVelocity.y);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        foreach (ContactPoint2D contact in other.contacts)
        {
            if (contact.normal.y > 0.5f)
            {
                _rigidbody.linearVelocity = new Vector2(_rigidbody.linearVelocity.x, 0f); // Yüksekliği sıfırla
                _rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
                break;
            }
        }
    }
}