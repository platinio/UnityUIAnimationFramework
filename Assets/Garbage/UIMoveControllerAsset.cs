using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using Platinio.TweenEngine;

public class UIMoveControllerAsset : PlayableAsset
{
    public ExposedReference<Transform> trasnform;
    public Vector3 position = Vector3.zero;
    public Vector3 startPosition = Vector3.zero;



    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<UIMoveControllerBehaivour>.Create(graph);

        var controllerBehaivour = playable.GetBehaviour();
        controllerBehaivour.trasnform = trasnform.Resolve(graph.GetResolver());
        controllerBehaivour.position = position;
        controllerBehaivour.initialPosition = controllerBehaivour.trasnform.position;

        
        return playable;
    }

}
