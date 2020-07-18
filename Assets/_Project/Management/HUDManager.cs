using UnityEngine;
using TMPro;

public class HUDManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI seedsText;


    private void OnEnable()
    {
        SowingManager.OnSeedsChanged += SetSeedsText;
    }

    private void OnDisable()
    {
        SowingManager.OnSeedsChanged -= SetSeedsText;
    }

    private void SetSeedsText()
    {
        seedsText.text = SowingManager.Instance.Seeds.ToString();
    }
}
