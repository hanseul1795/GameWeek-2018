using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerTools
{
    public static bool IsInLayerMask(LayerMask p_layerMask, int p_layer)
    {
        return p_layerMask == (p_layerMask | (1 << p_layer));
    }
}
