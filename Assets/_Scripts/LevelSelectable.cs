using UnityEngine;

public class LevelSelectable : MonoBehaviour
{
    [Tooltip("The name of the level's scene.")]
    [SerializeField] string _sceneName;

    public void OnClick()
    {
        GameController.SetLevel(_sceneName);
        MainMenuHandler.Instance.OnLevelSelectorToDifficultySelector();
    }
}
