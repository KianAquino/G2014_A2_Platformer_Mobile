using System.Collections;
using UnityEngine;

public class End : MonoBehaviour
{
    [SerializeField] Animator _animator;
    private bool _triggered = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !_triggered)
            StartCoroutine(EndSequence());
    }

    private IEnumerator EndSequence()
    {
        _triggered = true;

        _animator.SetTrigger("Triggered");

        Destroy(GameObject.Find("< PLAYER HUD >"));

        yield return new WaitForSeconds(4f);

        GameController.GameOver(GameOverState.WON);
    }
}
