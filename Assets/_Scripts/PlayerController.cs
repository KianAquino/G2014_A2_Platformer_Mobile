using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Rigidbody2D _rigidbody2D;
    [SerializeField] SpriteRenderer _spriteRenderer;
    [SerializeField] Animator _animator;
    private float _walkspeed;
    private Vector2 _velocity;
    private bool _isGrounded = false;

    private void Update()
    {
        _walkspeed = GameController.PlayerStats.Walkspeed;

        _velocity = GetVelocity();

        MoveCharacter(_velocity);
    }

    private void MoveCharacter(Vector2 velocity)
    {
        _rigidbody2D.linearVelocityX = velocity.x * _walkspeed;

        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        {
            _rigidbody2D.AddForce(Vector2.up * 5f, ForceMode2D.Impulse);
            Debug.Log("Jumped.");
        }

        // FLIP SPRITE BASED ON X VELOCITY
        if (_rigidbody2D.linearVelocityX < -0.01f) _spriteRenderer.flipX = true;
        else if (_rigidbody2D.linearVelocityX > 0.01f) _spriteRenderer.flipX = false;

        // CHANGE ANIMATION BASED ON X VELOCITY
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
