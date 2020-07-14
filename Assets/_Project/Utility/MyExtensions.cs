using UnityEngine;

public static class MyExtensions
{
    public static bool IsInLayer(this int thisInt, LayerMask layerMask)
    {
        return (layerMask.value & (1 << thisInt)) != 0;
    }
}
