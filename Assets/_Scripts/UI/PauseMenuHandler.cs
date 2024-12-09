using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuHandler : MonoBehaviour
{
    public void OnResumeClicked()
    {
        AudioSystem.PlaySFX(SFXType.CLICK, 0.3f);
        GameController.ResumeGame();
        Destroy(gameObject);
    }

    public void OnMainMenuClicked()
    {
        AudioSystem.PlaySFX(SFXType.CLICK, 0.3f);
        GameController.MainMenu();
    }
}
