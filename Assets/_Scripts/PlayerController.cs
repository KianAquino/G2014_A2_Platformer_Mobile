using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Rigidbody2D _rigidbody2D;
    // [SerializeField] SpriteRenderer _spriteRenderer;
    [SerializeField] Animator _animator;
    private float _walkspeed;
    private float _jumpStrength;
    private float _hVel;
    private float _vVel;
    private Vector2 _velocity;
    private bool _isGrounded = false;
    private Vector2 _lastGroundedPosition;
    private bool _doubleJumpActive = true;

    public Vector2 LastGroundedPosition => _lastGroundedPosition;

    [Header("For PC Testing")]
    [SerializeField] bool _keyboardControls;

    private void Update()
    {
        _walkspeed = GameController.PlayerStats.Walkspeed;
        _jumpStrength = GameController.PlayerStats.JumpStrength;

        _velocity = GetVelocity();

        MoveCharacter(_velocity);
    }

    private void MoveCharacter(Vector2 velocity)
    {
        _rigidbody2D.linearVelocityX = velocity.x * _walkspeed;

        if (Input.GetKeyDown(KeyCode.Space))
            Jump();

        // FLIP SPRITE BASED ON X VELOCITY
        if (_rigidbody2D.linearVelocityX < -0.01f) transform.localScale = new Vector3(-1f, 1f, 1f);
        else if (_rigidbody2D.linearVelocityX > 0.01f) transform.localScale = new Vector3(1f, 1f, 1f);

        // CHANGE ANIMATION STATES
        if (_isGrounded)
        {
            if (_rigidbody2D.linearVelocityX != 0f) _animator.SetTrigger("Run");
            else _animator.SetTrigger("Idle");
        }
        else
        {
            if (_rigidbody2D.linearVelocityY > 0f) _animator.SetTrigger("Jump");
            else if (_rigidbody2D.linearVelocityY < 0f) _animator.SetTrigger("Fall");
        }
    }

    private Vector2 GetVelocity()
    {
        // KEYBOARD
        if (_keyboardControls)
        {
            _hVel = Input.GetAxis("Horizontal");
            _vVel = Input.GetAxis("Vertical");
        }

        return new Vector2(_hVel, _vVel);
    }

    public void IsGrounded(bool value)
    {
        _isGrounded = value;

        if (!value)
            _lastGroundedPosition = transform.position;
        else
            _doubleJumpActive = true;
    }

    public void RespawnTo(Vector2 location)
    {
        transform.position = location;
        GameController.PlayerStats.DecreaseLife();
        AudioSystem.PlaySFX(SFXType.DEAD, 0.5f);
    }

    public void MoveLeftStart() => _hVel = -1f;
    public void MoveLeftEnd() => _hVel = 0f;
    public void MoveRightStart() => _hVel = 1f;
    public void MoveRightEnd() => _hVel = 0f;

    public void Jump()
    {
        if (_isGrounded)
        {
            _rigidbody2D.AddForce(Vector2.up * _jumpStrength, ForceMode2D.Impulse);
            AudioSystem.PlaySFX(SFXType.JUMP, 0.25f);
        }
        else if (_doubleJumpActive)
        {
            _rigidbody2D.linearVelocityY = 0f;
            _rigidbody2D.AddForce(Vector2.up * _jumpStrength, ForceMode2D.Impulse);
            _doubleJumpActive = false;
            AudioSystem.PlaySFX(SFXType.JUMP, 0.25f);
        }
    }
}
