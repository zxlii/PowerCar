using UnityEngine;
public class Main : MonoBehaviour
{

    public AudioSource audioSource;
    public static Main Instance;

    public Game game;

    void Awake()
    {
        Instance = this;
    }

    private AudioClip GetClip(string fileName)
    {
        var path = "Sounds/" + fileName;
        AudioClip clip = Resources.Load<AudioClip>(path);
        return clip;
    }
    public void PlaySound(string _SoundName)
    {
        AudioSource.PlayClipAtPoint(GetClip(_SoundName), transform.position, 1);
    }

    public void StopBGM()
    {
        audioSource.Stop();
    }

    public void PlayBGM()
    {
        audioSource.Play();
    }
}
