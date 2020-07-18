using UnityEngine;

public class Oak : Tree
{
    [SerializeField]
    private float seedGenerationSpeed = 0.2f;
    private static float generatedValue;


    private void Update()
    {
        float multiplier = WeatherManager.Instance.IsRaining ? WeatherManager.Instance.RainMultiplier : 1f;
        generatedValue += multiplier * seedGenerationSpeed * Time.deltaTime;
        if (generatedValue >= 1f)
        {
            SowingManager.Instance.AddSeeds((int)generatedValue);
            generatedValue -= (int)generatedValue;
        }
    }
}
