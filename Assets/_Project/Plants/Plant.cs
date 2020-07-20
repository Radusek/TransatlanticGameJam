using UnityEngine;

[CreateAssetMenu(fileName = "Plant", menuName = "Custom/Plant")]
public class Plant : ScriptableObject
{
    [SerializeField]
    private GameObject plantPrefab;
    public GameObject PlantPrefab => plantPrefab;

    [SerializeField]
    private int seedCost;
    public int SeedCost => seedCost;

    [SerializeField]
    private LayerMask whereMightGrow;
    public LayerMask WhereMightGrow => whereMightGrow;

    [SerializeField]
    private float growingTime = 5f;
    public float GrowingTime => growingTime;

    [SerializeField] [TextArea]
    private string description;
    public string Description => $"{description}\nSeed cost: {SeedCost}\nGrowing time: {GrowingTime:n0}s";
}
