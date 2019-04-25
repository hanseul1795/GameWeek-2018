using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorTools : MonoBehaviour
{
    public static Color CalculateAverageColor(Sprite p_sprite, bool p_includeAlpha = false)
    {
        Vector4 average = Vector4.zero;
        foreach (var color in p_sprite.texture.GetPixels())
        {
            average.x += color.r;
            average.y += color.g;
            average.z += color.b;
            average.w += (p_includeAlpha) ? color.a : 255.0f;

        }
        average /= p_sprite.texture.GetPixels().Length;

        return new Color(average.x, average.y, average.z, average.w);
    }
}
