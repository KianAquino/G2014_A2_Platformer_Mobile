using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] Rigidbody2D _rigidbody2D;
    // [SerializeField] SpriteRenderer _spriteRenderer;
    [SerializeField] Animator _animator;

    [Header("Stats")]
    [SerializeField] float _walkspeed = 2f;
    [SerializeField] float _maxChaseDistance = 7f;

    private bool _playerDetected = false;
    private GameObject _target;

    private void LateUpdate()
    {
        MoveTo(_target);
    }

    private void MoveTo(GameObject target)
    {
        if (_playerDetected)
        {
            Vector2 desiredVelocity = (target.transform.position - transform.position).normalized;

            _rigidbody2D.linearVelocityX = desiredVelocity.x * _walkspeed;

            // FLIP SPRITE BASED ON X VELOCITY
            if (_rigidbody2D.linearVelocityX < -0.01f) transform.localScale = new Vector3(-1f, 1f, 1f);
            else if (_rigidbody2D.linearVelocityX > 0.01f) transform.localScale = new Vector3(1f, 1f, 1f);

            // CHANGE ANIMATION STATE
            _animator.SetTrigger("Run");

            CheckTargetDistance();
        }
        else
        {
            _rigidbody2D.linearVelocityX = 0f;
            _animator.SetTrigger("Idle");
        }
    }

    private void CheckTargetDistance()
    {
        float distance = Vector2.Distance(_target.transform.position, transform.position);

        if (distance > _maxChaseDistance) _playerDetected = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _playerDetected = true;
            _target = collision.gameObject;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _playerDetected = true;
            _target = collision.gameObject;
        }
    }
}