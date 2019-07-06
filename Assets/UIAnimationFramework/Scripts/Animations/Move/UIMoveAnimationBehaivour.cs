using UnityEngine;
using Platinio.TweenEngine;

namespace Platinio.UIAnimation
{
    public class UIMoveAnimationBehaivour : UIAnimationBehaivour
    {
        public Vector2 startPosition = Vector2.zero;
        public Vector2 endPosition = Vector2.zero;
        public Vector2 pivot = Vector2.zero;
        public PivotPreset pivotPreset = PivotPreset.UpperLeft;
        public bool useCustomPivot = false;

        protected override void EvaluteAtTime(float t)
        {
            if (rect != null)
            {
                Vector3 change = startPosition - endPosition;
                Vector3 targetPosition = Equations.ChangeVector( t, endPosition, change, duration, ease );

                if (!useCustomPivot)
                {
                    rect.anchoredPosition = rect.FromAbsolutePositionToAnchoredPosition( targetPosition, canvas, pivotPreset );
                }
                else
                {
                    rect.anchoredPosition = rect.FromAbsolutePositionToAnchoredPosition( targetPosition, canvas, pivot );
                }
                            
            }
            
        }
       
    }

}

