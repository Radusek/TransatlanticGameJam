using System.Collections;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{   
    [SerializeField]
    private AudioSource musicPlayer;

    [SerializeField]
    private float musicReplayInterval = 5f;
    private WaitForSecondsRealtime replayDelay;
    private bool wasPlaying = true;


    protected override void Awake()
    {
        base.Awake();
        replayDelay = new WaitForSecondsRealtime(musicReplayInterval);
    }

    private void Update()
    {
        if (!musicPlayer.isPlaying && wasPlaying)
            StartCoroutine(ReplayMusic());
        wasPlaying = musicPlayer.isPlaying;
    }

    private IEnumerator ReplayMusic()
    {
        yield return replayDelay;
        musicPlayer.Play();
    }
}
