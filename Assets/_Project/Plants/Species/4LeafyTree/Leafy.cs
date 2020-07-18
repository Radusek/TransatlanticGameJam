using UnityEngine;

public class Leafy : Tree
{
    public static int LeafyTrees { get; private set; }
    public static float RainBonus => LeafyTrees * 0.02f;

    private void OnEnable() => LeafyTrees++;
    private void OnDisable() => LeafyTrees--;
}
