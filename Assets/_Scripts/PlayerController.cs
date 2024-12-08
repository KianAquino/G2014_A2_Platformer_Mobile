using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Rigidbody2D _rigidbody2D;
    // [SerializeField] SpriteRenderer _spriteRenderer;
    [SerializeField] Animator _animator;
    private float _walkspeed;
    private float _jumpStrength;
    private Vector2 _velocity;
    private bool _isGrounded = false;

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

        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
            _rigidbody2D.AddForce(Vector2.up * _jumpStrength, ForceMode2D.Impulse);

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
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        return new Vector2(h, v);
    }

    public void IsGrounded(bool value) => _isGrounded = value;
}
