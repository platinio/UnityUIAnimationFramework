using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class LightControllerBehaivour : PlayableBehaviour
{
    public Light light = null;
    public Color color = Color.black;
    public float intensity = 1.0f;

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        if (light != null)
        {
            light.color = color;
            light.intensity = intensity;
        }
    }

}
