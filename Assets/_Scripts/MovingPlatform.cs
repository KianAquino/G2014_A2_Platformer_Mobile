using System.Collections;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] Rigidbody2D _rigidbody2D;
    private Vector2 _start;
    [SerializeField] Vector2 _end;
    [SerializeField] float _speed = .5f;
    [SerializeField] float _waitTime = 3.5f;
    [SerializeField] float _gizmoSize = 0.1f;

    private void Start()
    {
        _start = _rigidbody2D.position;
        _end += _start;

        StartCoroutine(MovePlatform());
    }

    private IEnumerator MovePlatform()
    {
        while (Vector2.Distance(_rigidbody2D.position, _end) > 0.05f)
        {
            _rigidbody2D.position = Vector2.Lerp(_rigidbody2D.position, _end, _speed * Time.deltaTime);
            yield return null;
        }

        yield return new WaitForSeconds(_waitTime);

        while (Vector2.Distance(_rigidbody2D.position, _start) > 0.05f)
        {
            _rigidbody2D.position = Vector2.Lerp(_rigidbody2D.position, _start, _speed * Time.deltaTime);
            yield return null;
        }

        yield return new WaitForSeconds(_waitTime);

        StartCoroutine(MovePlatform());
    }

    private void OnDrawGizmosSelected()
    {
        _start = _rigidbody2D.position;

        Gizmos.color = Color.green;
        Gizmos.DrawSphere(_start, _gizmoSize);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(_start + _end, _gizmoSize);
    }
}
