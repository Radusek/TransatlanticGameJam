using UnityEngine;

public class WeatherManager : Singleton<WeatherManager>
{
    [SerializeField]
    private ParticleSystem rainParticles;

    public bool IsRaining => rainParticles.isPlaying;


    public void SetRaining(bool state)
    {
        if (state)
            rainParticles.Play();
        else
            rainParticles.Stop();
    }
}
