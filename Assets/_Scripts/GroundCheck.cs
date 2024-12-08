using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField] PlayerController _playerController;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _playerController.IsGrounded(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _playerController.IsGrounded(false);
    }
}
