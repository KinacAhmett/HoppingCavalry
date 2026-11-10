using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour

{
    public Animator animator;

    [SerializeField] private float _jumpForce = 13f;
    [SerializeField] private float _movementSpeed = 5f;
    [SerializeField] private float _maxDistanceFromCameraBeforeDeath = 5f;
    [SerializeField] private AudioClip _jumpSound;
    
    
    private Rigidbody2D _rigidbody;
    private InputAction _moveInputAction;
    private AudioSource _audioSource;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _moveInputAction = InputSystem.actions.FindAction("Move");
        _audioSource = GetComponent<AudioSource>();
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
        // Klavye girdisi (A/D, ok tuşları)
        float x = _moveInputAction.ReadValue<Vector2>().x;

        // Dokunmatik girdi: ekrana dokunuluyorsa klavyenin yerine geçer
        if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.isPressed)
        {
            float touchX = Touchscreen.current.primaryTouch.position.ReadValue().x;
            x = touchX < Screen.width / 2f ? -1f : 1f;
        }

        _rigidbody.linearVelocity = new Vector2(x * _movementSpeed, _rigidbody.linearVelocity.y);

        // --- Ekran sarması ---
        Vector2 pos = _rigidbody.position;
        float halfWidth = Camera.main.orthographicSize * Camera.main.aspect;

        if (pos.x > halfWidth)
        {
            pos.x = -halfWidth;
            _rigidbody.position = pos;
        }
        else if (pos.x < -halfWidth)
        {
            pos.x = halfWidth;
            _rigidbody.position = pos;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        foreach (ContactPoint2D contact in other.contacts)
        {
            if (contact.normal.y > 0.5f)
            {
                _rigidbody.linearVelocity = new Vector2(_rigidbody.linearVelocity.x, 0f); // Yüksekliği sıfırla
                _rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
                _audioSource.PlayOneShot(_jumpSound);
                break;
            }
        }
    }
}