using UnityEngine;
using UnityEngine.Playables;
using Platinio.TweenEngine;
using Platinio;

namespace Platinio.UIAnimation
{
    public class UIMoveAnimationAsset : UIAnimationAsset
    {
        [SerializeField] private Vector2 startPosition = Vector3.zero;
        [SerializeField] private Vector2 finalPosition = Vector3.zero;
        [SerializeField] private UIAnchor UIAnchor = UIAnchor.LowerCenter;

        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            var playable = ScriptPlayable<UIMoveAnimationBehaivour>.Create( graph );

            var controllerBehaivour = playable.GetBehaviour();

            Construct(controllerBehaivour , graph);

            controllerBehaivour.startPosition = startPosition;
            controllerBehaivour.finalPosition = finalPosition;
            controllerBehaivour.UIAnchor = UIAnchor;

           
            return playable;
        }

       
    }

}

