using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using Platinio.TweenEngine;
using Platinio;

namespace Platinio.UIAnimation
{
    public class UIMoveAnimationBehaivour : UIAnimationBehaivour
    {
        public Vector2 startPosition = Vector2.zero;
        public Vector2 finalPosition = Vector2.zero;
        public UIAnchor UIAnchor = UIAnchor.UpperLeft;

        protected override void EvaluteAtTime(float t)
        {
            if (rect != null)
            {
                Vector3 change = startPosition - finalPosition;
                //rect.anchoredPosition = PlatinioTween.FromAbsolutePositionToCanvasPosition( Equations.ChangeVector( t, finalPosition, change, duration, ease ) , rect , canvas , UIAnchor);    
                rect.anchoredPosition = rect.FromAbsolutePositionToCanvasPosition( Equations.ChangeVector( t, finalPosition, change, duration, ease ) , canvas , UIAnchor);
                Debug.Log("Time " + t);
            }
            
        }
       
    }

}

