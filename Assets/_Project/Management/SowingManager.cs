using UnityEngine;
using DG.Tweening;

public class SowingManager : Singleton<SowingManager>
{
    [SerializeField]
    private Plant[] plants;

    [SerializeField]
    private ParticleSystem sowingHintParticles;

    private bool isSowing = true;
    public bool IsSowing
    {
        get { return isSowing; }
        set
        {
            isSowing = value;
            sowingHintParticles.gameObject.SetActive(value);
        }
    }

    private int selectedPlant;

    public Plant GetSelectedPlant => plants[selectedPlant];

    private Camera mainCamera;
    private int sowingLayerMask;
    private bool didHit;
    private bool isSuitablePlaceToGrow;
    private Vector3 plantPosition;


    protected override void Awake()
    {
        base.Awake();
        mainCamera = Camera.main;
        sowingLayerMask = -1;
    }

    public void ShootRaycast()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        didHit = Physics.Raycast(ray, out hit, Mathf.Infinity, sowingLayerMask);
        sowingHintParticles.gameObject.SetActive(didHit);
        if (!didHit)
            return;

        sowingHintParticles.gameObject.transform.position = hit.point * 1.01f;
        sowingHintParticles.gameObject.transform.forward = hit.point;

        isSuitablePlaceToGrow = hit.transform.gameObject.layer.IsInLayer(GetSelectedPlant.WhereMightGrow);
        var main = sowingHintParticles.main;
        main.startColor = isSuitablePlaceToGrow ? Color.green : Color.red;
        plantPosition = hit.point;
    }

    public void TrySow()
    {
        if (!isSuitablePlaceToGrow)
            return;

        GameObject go = SpawnPlant();
        SetPlantGrowing(go);
    }

    private GameObject SpawnPlant()
    {
        Vector3 groundedPosition = 0.99f * plantPosition;
        GameObject go = Instantiate(GetSelectedPlant.PlantPrefab, groundedPosition, Quaternion.identity);
        go.transform.up = groundedPosition;
        return go;
    }

    private void SetPlantGrowing(GameObject go)
    {
        Transform treeGraphics = go.transform.GetChild(0);
        Vector3 finalScale = treeGraphics.localScale;
        treeGraphics.localScale = finalScale / 10f;
        treeGraphics.DOScale(finalScale, GetSelectedPlant.GrowingTime);
    }
}
