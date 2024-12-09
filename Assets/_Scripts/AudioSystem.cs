using UnityEngine;

public enum MusicType
{
    MAIN_MENU,
    LEVEL_SELECTOR,
    GAME,
    GAME_OVER
}
public enum SFXType
{
    CLICK,
    JUMP,
    END,
    COLLECT,
    DEAD
}

[ExecuteInEditMode]
public class AudioSystem : MonoBehaviour
{
    private static AudioSystem _instance;

    [SerializeField] private SoundList[] _musicList;
    [SerializeField] private SoundList[] _SFXList;

    private AudioSource _musicSource;
    private AudioSource _SFXSource;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;

        if (Application.isPlaying)
        {
            _musicSource = gameObject.AddComponent<AudioSource>();
            _SFXSource = gameObject.AddComponent<AudioSource>();

            _musicSource.loop = true;

            DontDestroyOnLoad(gameObject);
        }
    }

    public static void PlayMusic(MusicType sound, float volume = 0.5f)
    {
        AudioClip[] clips = _instance._musicList[(int)sound].Sounds;
        AudioClip clip = clips[Random.Range(0, clips.Length)];

        _instance._musicSource.clip = clip;
        _instance._musicSource.volume = volume;
        _instance._musicSource.Play();
    }

    public static void PlaySFX(SFXType sound, float volume = 1f)
    {
        AudioClip[] clips = _instance._SFXList[(int)sound].Sounds;
        AudioClip clip = clips[Random.Range(0, clips.Length)];

        _instance._SFXSource.PlayOneShot(clip, volume);
    }

#if UNITY_EDITOR
    private void OnEnable()
    {
        string[] names;

        // Music List
        names = System.Enum.GetNames(typeof(MusicType));
        System.Array.Resize(ref _musicList, names.Length);
        for (int i = 0; i < _musicList.Length; i++)
            _musicList[i].Name = names[i];

        // SFX List
        names = System.Enum.GetNames(typeof(SFXType));
        System.Array.Resize(ref _SFXList, names.Length);
        for (int i = 0; i < _SFXList.Length; i++)
            _SFXList[i].Name = names[i];
    }
#endif
}

[System.Serializable]
public struct SoundList
{
    [HideInInspector] public string Name;
    [SerializeField] private AudioClip[] _sounds;

    public AudioClip[] Sounds { get { return _sounds; } }
}