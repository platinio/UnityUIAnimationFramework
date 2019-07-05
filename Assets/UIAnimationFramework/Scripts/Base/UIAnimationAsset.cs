using UnityEngine;
using UnityEngine.Playables;
using Platinio.TweenEngine;

namespace Platinio.UIAnimation
{
    public abstract class UIAnimationAsset : PlayableAsset
    {
        [SerializeField] protected ExposedReference<RectTransform> rect;
        [SerializeField] protected ExposedReference<RectTransform> canvas;
        [SerializeField] protected Ease ease = Ease.Linear;
        [SerializeField] protected RectTransform canvasRect = null;
        [SerializeField] protected RectTransform rectSaveReference = null;

        public RectTransform Canvas { get { return canvasRect; } }
        public RectTransform Rect { get { return rectSaveReference; } }

        

        protected void Construct(UIAnimationBehaivour controller , PlayableGraph graph)
        {
            controller.rect = rect.Resolve( graph.GetResolver() );
            controller.ease = ease;
            controller.canvas = canvas.Resolve( graph.GetResolver() );

            canvasRect = canvas.Resolve( graph.GetResolver() );
            rectSaveReference = rect.Resolve( graph.GetResolver() );
        }       
    }
}

