using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using Platinio.TweenEngine;

namespace Platinio.UIAnimation
{
    public class UIMoveAnimationBehaivour : UIAnimationBehaivour
    {
        public Vector2 startPosition = Vector2.zero;
        public Vector2 finalPosition = Vector2.zero;
        public PlatinioTween.UIAnchor UIAnchor = PlatinioTween.UIAnchor.UpperLeft;

        protected override void EvaluteAtTime(float t)
        {
            if (rect != null)
            {
                Vector3 change = startPosition - finalPosition;

                //Vector2 pos = Vector2.Scale( m_path[n] - m_rect.anchorMin, m_animator.Canvas.sizeDelta )
                //Debug.Log(rect.anchoredPosition);
                //rect.anchoredPosition = PlatinioTween.FromAbsolutePositionToCanvasPosition( Equations.ChangeVector( t, finalPosition, change, duration, ease ) , rect , canvas , UIAnchor);
                Vector2 anchoredPosition = PlatinioTween.FromAbsolutePositionToCanvasPosition( finalPosition, rect, canvas, UIAnchor );
                Debug.Log("Anchored Position " + anchoredPosition);
                rect.anchoredPosition = anchoredPosition;
            }
            
        }
       
    }

}

