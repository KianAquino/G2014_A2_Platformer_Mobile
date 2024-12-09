using UnityEngine;

public class SpeedBoost : MonoBehaviour
{
    [SerializeField] float _speedMultipler = 2f;
    [SerializeField] float _duration = 4f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            AudioSystem.PlaySFX(SFXType.COLLECT, 0.25f);
            GameController.SpeedBoostPlayer(_speedMultipler, _duration);
            Destroy(gameObject);
        }
    }
}
