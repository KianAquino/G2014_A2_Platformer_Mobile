using UnityEngine;

public class DifficultrySelectable : MonoBehaviour
{
    [SerializeField] Difficulty _difficulty = Difficulty.NONE;

    public void OnClick()
    {
        GameController.SetDifficulty(_difficulty);
        // GameController.LoadLevel();
    }
}
