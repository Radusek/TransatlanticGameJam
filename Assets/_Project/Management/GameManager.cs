using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public Camera MainCamera { get; private set; }


    protected override void Awake()
    {
        base.Awake();
        MainCamera = Camera.main;
    }

    void Update()
    {
        HandleSowing();

        if (Input.GetKeyDown(KeyCode.Space))
            WeatherManager.Instance.SetRaining(!WeatherManager.Instance.IsRaining);
    }

    private void HandleSowing()
    {
        SowingManager.Instance.TryChangeCurrentPlant();

        if (!SowingManager.Instance.IsSowing)
            return;

        SowingManager.Instance.ShootRaycast();

        if (Input.GetButtonDown("Fire1"))
            SowingManager.Instance.TrySow();
    }

}
