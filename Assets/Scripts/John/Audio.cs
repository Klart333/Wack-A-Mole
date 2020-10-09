using UnityEngine;

public class Audio : MonoBehaviour
{
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip[] audioClips;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        GameManager.Instance.OnSharkKilled += AudioOnSharkKill;
    }

    private void AudioOnSharkKill(float timeToKillShark)
    {
        if (timeToKillShark < 0.5)
        {
            foreach (var clip in audioClips)
            {
                if (clip.name == "ArcadeBlip1")
                {
                    audioSource.PlayOneShot(clip);
                }
            }
        }
    }
}
