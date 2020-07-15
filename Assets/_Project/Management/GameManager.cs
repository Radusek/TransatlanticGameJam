using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    void Update()
    {
        HandleSowing();

        if (Input.GetKeyDown(KeyCode.Space))
            WeatherManager.Instance.SetRaining(!WeatherManager.Instance.IsRaining);
    }

    private void HandleSowing()
    {
        if (!SowingManager.Instance.IsSowing)
            return;

        SowingManager.Instance.ShootRaycast();

        if (Input.GetButtonDown("Fire1"))
            SowingManager.Instance.TrySow();
    }

}
