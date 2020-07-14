using UnityEngine;
using DG.Tweening;

public class CameraOrbiting : MonoBehaviour
{
    [SerializeField]
    private float sensitivity = 1f;

    [SerializeField]
    private Vector2 orbitRange;
    private float currentOrbit;

    [SerializeField]
    private int zoomLevels = 3;
    private int MaxZoomLevel => zoomLevels - 1;
    private int currentZoomLevel;


    private void Awake()
    {
        SetZoomLevel(MaxZoomLevel);
    }

    void Update()
    {
        if (Input.GetButton("Fire2"))
            OrbitAroundPlanet();

        float deltaScroll = Input.mouseScrollDelta.y;
        if (deltaScroll != 0f)
            ChangeOrbit(-deltaScroll);

        SetCameraPosition();
    }

    private void OrbitAroundPlanet()
    {
        Vector3 mouseDeltaPosition = new Vector3(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0f);
        Vector3 rotation = transform.rotation.eulerAngles;
        rotation += mouseDeltaPosition * sensitivity;
        rotation = ClampEulerRotationX(rotation);
        transform.eulerAngles = rotation;
    }

    private static Vector3 ClampEulerRotationX(Vector3 rotation)
    {
        if (rotation.x < 180 && rotation.x > 85)
            rotation.x = 85;
        if (rotation.x >= 180 && rotation.x < 275)
            rotation.x = 275;
        return rotation;
    }

    private void SetCameraPosition()
    {
        transform.position = -transform.forward * currentOrbit;
    }

    private void ChangeOrbit(float deltaScroll)
    {
        if (deltaScroll < 0 && currentZoomLevel > 0 || deltaScroll > 0 && currentZoomLevel < MaxZoomLevel)
            SetZoomLevel(currentZoomLevel + (int)Mathf.Sign(deltaScroll));
    }

    private void SetZoomLevel(int level)
    {
        float t = (float)level / MaxZoomLevel;
        float endValue = Mathf.Lerp(orbitRange.x, orbitRange.y, t);
        DOTween.To(() => currentOrbit, x => currentOrbit = x, endValue, 0.4f);
        currentZoomLevel = level;
    }
}
