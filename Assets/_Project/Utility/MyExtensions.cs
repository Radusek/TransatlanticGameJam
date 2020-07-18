using UnityEngine;

public static class MyExtensions
{
    public static float Sqr(this float thisFloat) => thisFloat * thisFloat;

    public static bool IsInLayer(this int thisInt, LayerMask layerMask) => (layerMask.value & (1 << thisInt)) != 0;

    public static void SetPlaying(this ParticleSystem ps, bool enable)
    {
        if (enable)
            ps.Play();
        else
            ps.Stop();
    }
}
