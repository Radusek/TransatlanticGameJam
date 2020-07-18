using UnityEngine;
using DG.Tweening;
using System;

public class SowingManager : Singleton<SowingManager>
{
    [SerializeField]
    private Plant[] plants;

    [SerializeField]
    private ParticleSystem sowingHintParticles;

    public bool IsSowing { get; private set; }

    private int selectedPlant;

    public Plant GetSelectedPlant => plants[selectedPlant];

    private int sowingLayerMask;
    private bool didHit;
    private bool isSuitablePlaceToGrow;
    private Vector3 plantPosition;

    [SerializeField]
    private int seeds = 20;
    public int Seeds {
        get => seeds;
        private set
        {
            seeds = value;
            OnSeedsChanged.Invoke();
        }
    }
    private bool HasEnoughSeeds => Seeds >= GetSelectedPlant.SeedCost;

    public static event Action OnSeedsChanged = delegate { };



    protected override void Awake()
    {
        base.Awake();
        sowingLayerMask = ~(1 << LayerMask.NameToLayer("Meteor"));
    }

    public void AddSeeds(int amount) => Seeds += amount;

    public void TryChangeCurrentPlant()
    {
        for (int i = 0; i < plants.Length; i++)
        {
            KeyCode key = (KeyCode)((int)KeyCode.Alpha1 + i);
            if (Input.GetKeyDown(key))
            {
                SelectPlant(i);
            }
        }
    }

    public void SelectPlant(int index)
    {
        selectedPlant = index;
        IsSowing = true;
    }

    public void ShootRaycast()
    {
        RaycastHit hit = CastRay();
        sowingHintParticles.SetPlaying(didHit);
        if (!didHit)
            return;

        PositionHint(hit);
        SetHintColor(hit);
        plantPosition = hit.point;
    }

    private RaycastHit CastRay()
    {
        Ray ray = GameManager.Instance.MainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        didHit = Physics.Raycast(ray, out hit, Mathf.Infinity, sowingLayerMask);
        return hit;
    }

    private void PositionHint(RaycastHit hit)
    {
        sowingHintParticles.gameObject.transform.position = hit.point + 0.01f * (hit.point - hit.transform.position);
        sowingHintParticles.gameObject.transform.forward = hit.point - hit.transform.position;
    }

    private void SetHintColor(RaycastHit hit)
    {
        isSuitablePlaceToGrow = hit.transform.gameObject.layer.IsInLayer(GetSelectedPlant.WhereMightGrow);
        var main = sowingHintParticles.main;
        main.startColor = isSuitablePlaceToGrow && HasEnoughSeeds ? Color.green : Color.red;
    }

    public void TrySow()
    {
        if (!isSuitablePlaceToGrow || !didHit || !HasEnoughSeeds)
            return;

        IsSowing = false;
        sowingHintParticles.SetPlaying(false);
        GameObject go = SpawnPlant();
        SetPlantGrowing(go);
    }

    private GameObject SpawnPlant()
    {
        Seeds -= GetSelectedPlant.SeedCost;
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
