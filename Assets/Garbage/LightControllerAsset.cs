using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class LightControllerAsset : PlayableAsset
{
    public ExposedReference<Light> light;
    public Color color = Color.white;
    public float intensity = 1.0f;

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<LightControllerBehaivour>.Create(graph);

        var lightControllerBehaivour = playable.GetBehaviour();
        lightControllerBehaivour.light = light.Resolve(graph.GetResolver());
        lightControllerBehaivour.color = color;
        lightControllerBehaivour.intensity = intensity;

        return playable;
    }
}
