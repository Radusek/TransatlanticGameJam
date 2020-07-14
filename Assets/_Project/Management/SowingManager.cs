using UnityEngine;
using DG.Tweening;

public class SowingManager : Singleton<SowingManager>
{
    [SerializeField]
    private Plant[] plants;

    private bool isSowing = true;
    public bool IsSowing => isSowing;

    private int selectedPlant;

    public Plant GetSelectedPlant => plants[selectedPlant];

    private Camera mainCamera;
    private int sowingLayerMask;


    protected override void Awake()
    {
        base.Awake();
        mainCamera = Camera.main;
        sowingLayerMask = 1 << LayerMask.NameToLayer("Planet") | 1 << LayerMask.NameToLayer("Water");
    }

    public void TrySow()
    {
        Plant plant = GetSelectedPlant;

        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, sowingLayerMask))
        {
            if (hit.transform.gameObject.layer.IsInLayer(plant.WhereMightGrow))
            {
                GameObject go = SpawnPlant(plant, hit);
                SetPlantGrowing(plant, go);
            }
        }
    }

    private static GameObject SpawnPlant(Plant plant, RaycastHit hit)
    {
        Vector3 groundedPosition = 0.99f * hit.point;
        GameObject go = Instantiate(plant.PlantPrefab, groundedPosition, Quaternion.identity);
        go.transform.up = hit.point;
        return go;
    }

    private static void SetPlantGrowing(Plant plant, GameObject go)
    {
        Vector3 finalScale = go.transform.localScale;
        go.transform.localScale = finalScale / 10f;
        go.transform.DOScale(finalScale, plant.GrowingTime);
    }
}
