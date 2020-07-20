using UnityEngine;

public class WeatherManager : Singleton<WeatherManager>
{
    [SerializeField]
    private GameObject meteorPrefab;
    [SerializeField]
    private Vector2 meteorSpeedRange;

    private float nextMeteorSpawnTime;

    [SerializeField]
    private ParticleSystem rainParticles;

    public bool IsRaining => rainParticles.isPlaying;
    public float RainMultiplier => 1.1f + Leafy.RainBonus;

    [SerializeField]
    private float rainDuration = 20f;

    [SerializeField]
    private Vector2 rainInterval;

    private float nextRainStateTime;


    protected override void Awake()
    {
        base.Awake();
        nextMeteorSpawnTime = Time.time + 5f;
        nextRainStateTime = Time.time + NextRainInterval;
    }

    private void Update()
    {
        SpawnMeteors();
        HandleRain();
    }

    private void SpawnMeteors()
    {
        if (Time.time < nextMeteorSpawnTime)
            return;
        
        SpawnMeteor();
        nextMeteorSpawnTime = Time.time + Random.Range(3f, 6f);
    }

    private void SpawnMeteor()
    {
        Vector3 spawnPosition = 50f * Random.insideUnitSphere.normalized;
        GameObject go = Instantiate(meteorPrefab, spawnPosition, Quaternion.identity);
        Vector3 movingDirection = -spawnPosition.normalized;
        float meteorSpeed = Random.Range(meteorSpeedRange.x, meteorSpeedRange.y);
        go.GetComponent<Rigidbody>().velocity = meteorSpeed * movingDirection;
    }

    private void HandleRain()
    {
        if (Time.time < nextRainStateTime)
            return;

        nextRainStateTime = Time.time + (IsRaining ? NextRainInterval : rainDuration);
        SetRaining(!IsRaining);
    }

    private float NextRainInterval => Random.Range(rainInterval.x, rainInterval.y);

    private void SetRaining(bool state) => rainParticles.SetPlaying(state);
}
