using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] private MusicType _music;
    [SerializeField, Range(0f, 1f)] private float _volume = 0.5f;

    private void Start()
    {
        AudioSystem.PlayMusic(_music, _volume);
    }
}