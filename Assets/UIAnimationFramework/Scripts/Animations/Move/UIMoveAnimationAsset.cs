using UnityEngine;
using UnityEngine.Playables;

namespace Platinio.UIAnimation
{
    public class UIMoveAnimationAsset : UIAnimationAsset
    {
        [SerializeField] private PivotPreset pivotPreset = PivotPreset.LowerCenter;
        [SerializeField] private Vector2 pivot = Vector2.zero;
        [SerializeField] private Vector2 startPosition = Vector3.zero;
        [SerializeField] private Vector2 endPosition = Vector3.zero;
        
        

        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            var playable = ScriptPlayable<UIMoveAnimationBehaivour>.Create( graph );

            var controllerBehaivour = playable.GetBehaviour();

            Construct(controllerBehaivour , graph);

            controllerBehaivour.startPosition = startPosition;
            controllerBehaivour.endPosition = endPosition;
            controllerBehaivour.pivot = pivot;
            controllerBehaivour.pivotPreset = pivotPreset;

           
            return playable;
        }
                
       
    }

}

