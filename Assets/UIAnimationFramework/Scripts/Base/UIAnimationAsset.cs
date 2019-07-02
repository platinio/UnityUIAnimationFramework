using UnityEngine;
using UnityEngine.Playables;
using Platinio.TweenEngine;

namespace Platinio.UIAnimation
{
    public abstract class UIAnimationAsset: PlayableAsset
    {
        [SerializeField] protected ExposedReference<RectTransform> rect;
        [SerializeField] protected ExposedReference<RectTransform> canvas;
        [SerializeField] protected Ease ease = Ease.Linear;


        protected void Construct(UIAnimationBehaivour controller , PlayableGraph graph)
        {
            controller.rect = rect.Resolve( graph.GetResolver() );
            controller.ease = ease;
            controller.canvas = canvas.Resolve( graph.GetResolver() );
        }       
    }
}

