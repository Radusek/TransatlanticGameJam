using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
            ShootRaycast();
    }

    private void ShootRaycast()
    {
        if (SowingManager.Instance.IsSowing)
            SowingManager.Instance.TrySow();
    }
}
