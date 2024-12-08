using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField] int _points = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameController.PlayerStats.ModifyPointsBy(_points);
            Destroy(gameObject);
        }
    }
}
