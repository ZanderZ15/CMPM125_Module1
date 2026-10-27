using Unity.VisualScripting;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField] AudioSource bgm;
    public void Start() 
    {
        bgm.volume = GameManager.Instance.getAudio();
    }
    public void SetVolume(float volume)
    {
        EventBus.Instance.OnAudioChange(volume);
        bgm.volume = GameManager.Instance.getAudio();
        
    }
}