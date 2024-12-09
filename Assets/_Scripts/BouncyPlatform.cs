using UnityEngine;

public class BouncyPlatform : MonoBehaviour
{
    [SerializeField] Rigidbody2D _rigidbody2D;
    [SerializeField] float _bounceStrength = 10f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            AudioSystem.PlaySFX(SFXType.BOUNCE, 0.3f);
            Rigidbody2D rigidbody2D = collision.gameObject.GetComponent<Rigidbody2D>();
            rigidbody2D.AddForce(Vector2.up *  _bounceStrength, ForceMode2D.Impulse);
        }
    }
}
