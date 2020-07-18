using UnityEngine;

public class WeatherManager : Singleton<WeatherManager>
{
    [SerializeField]
    private GameObject meteorPrefab;
    [SerializeField]
    private Vector2 meteorSpeedRange;

    private float nextMeteorSpawnTime = 5f;


    [SerializeField]
    private ParticleSystem rainParticles;

    public bool IsRaining => rainParticles.isPlaying;
    public float RainMultiplier => 1.1f + Leafy.RainBonus;


    public void SetRaining(bool state)
    {
        rainParticles.SetPlaying(state);
    }

    private void Update()
    {
        if (Time.time > nextMeteorSpawnTime)
        {
            SpawnMeteor();
            nextMeteorSpawnTime = Time.time + Random.Range(3f, 6f);
        }
    }

    private void SpawnMeteor()
    {
        Vector3 spawnPosition = 50f * Random.insideUnitSphere.normalized;
        GameObject go = Instantiate(meteorPrefab, spawnPosition, Quaternion.identity);
        Vector3 movingDirection = -spawnPosition.normalized;
        float meteorSpeed = Random.Range(meteorSpeedRange.x, meteorSpeedRange.y);
        go.GetComponent<Rigidbody>().velocity = meteorSpeed * movingDirection;
    }
}
