using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI seedsText;

    [SerializeField]
    private TextMeshProUGUI livesText;

    [SerializeField]
    private Image treeDescriptionBackground;
    [SerializeField]
    private TextMeshProUGUI treeDescriptionText;

    [SerializeField]
    private GameObject loseTextObject;

    [SerializeField]
    private GameObject winTextObject;

    [SerializeField]
    private TextMeshProUGUI treesCountText;

    [SerializeField]
    private GameObject helpObject;


    protected virtual void OnEnable()
    {
        SowingManager.OnSeedsChanged += SetSeedsText;
        GameManager.OnLivesChanged += SetLivesText;
        GameManager.OnLivesChanged += ShowLoseText;
        Tree.OnTreesCountChanged += SetTreesCountText;
        Tree.OnTreesCountChanged += ShowWinText;
    }

    private void OnDisable()
    {
        SowingManager.OnSeedsChanged -= SetSeedsText;
        GameManager.OnLivesChanged -= SetLivesText;
        GameManager.OnLivesChanged -= ShowLoseText;
        Tree.OnTreesCountChanged -= SetTreesCountText;
        Tree.OnTreesCountChanged -= ShowWinText;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
            SwitchHelpObjectActive();
    }

    private void SetSeedsText()
    {
        seedsText.text = SowingManager.Instance.Seeds.ToString();
    }

    private void SetLivesText()
    {
        livesText.text = GameManager.Instance.Lives.ToString();
    }

    public void ShowTreeDescription(int index)
    {
        treeDescriptionBackground.gameObject.SetActive(true);
        treeDescriptionText.text = SowingManager.Instance.Plants[index].Description;
        treeDescriptionText.ForceMeshUpdate();
        Vector2 textSize = treeDescriptionText.GetRenderedValues(false);
        Vector2 padding = 25f * Vector2.one;
        treeDescriptionBackground.rectTransform.sizeDelta = textSize + padding;
    }

    public void HideTreeDescription()
    {
        treeDescriptionBackground.gameObject.SetActive(false);
    }

    private void ShowLoseText()
    {
        if (GameManager.Instance.Lives <= 0)
            loseTextObject.SetActive(true);
    }

    private void ShowWinText()
    {
        if (Tree.Trees >= 100)
            winTextObject.SetActive(true);
    }

    private void SetTreesCountText()
    {
        treesCountText.text = $"Trees: {Tree.Trees}/100";
    }

    public void SwitchHelpObjectActive()
    {
        if (Time.timeScale == 0f && !helpObject.activeSelf)
            return;

        helpObject.SetActive(!helpObject.activeSelf);
        Time.timeScale = Time.timeScale == 0f ? 1f : 0f;
    }
}
