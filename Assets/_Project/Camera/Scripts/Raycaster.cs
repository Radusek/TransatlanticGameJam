using UnityEngine;

public class Raycaster : MonoBehaviour
{
    private Camera camera;


    private void Awake()
    {
        camera = Camera.main;
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
            ShootRaycast();
    }

    private void ShootRaycast()
    {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
            Debug.Log($"{hit.point.magnitude} {hit.transform.gameObject.layer}");
    }
}
