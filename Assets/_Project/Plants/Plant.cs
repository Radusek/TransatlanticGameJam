using UnityEngine;

[CreateAssetMenu(fileName = "Plant", menuName = "Custom/Plant")]
public class Plant : ScriptableObject
{
    [SerializeField]
    private LayerMask whereMightGrow;
    public LayerMask WhereMightGrow => whereMightGrow;

    [SerializeField]
    private GameObject plantPrefab;
    public GameObject PlantPrefab => plantPrefab;

    [SerializeField]
    private float growingTime = 5f;
    public float GrowingTime => growingTime;
}
