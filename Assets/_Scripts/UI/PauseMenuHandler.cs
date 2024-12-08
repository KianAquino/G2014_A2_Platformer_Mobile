using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuHandler : MonoBehaviour
{
    public void OnResumeClicked()
    {
        GameController.ResumeGame();
        Destroy(gameObject);
    }

    public void OnMainMenuClicked()
    {
        GameController.ResumeGame();
        SceneManager.LoadScene(0);
    }
}
