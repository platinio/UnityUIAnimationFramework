using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platinio
{
    public enum UIAnchor
    {
        UpperLeft,
        UpperCenter,
        UpperRight,
        MiddleLeft,
        MiddleCenter,
        MiddleRight,
        LowerLeft,
        LowerCenter,
        LowerRight,
        Custom
    }

    public static class RectTransformUtility 
    {
        public static Vector2 FromAbsolutePositionToCanvasPosition(this RectTransform rect, Vector2 v, RectTransform canvas, UIAnchor anchor = UIAnchor.MiddleCenter)
        {
            Vector2 centerAnchor = ( rect.anchorMax + rect.anchorMin ) * 0.5f;           
            return Vector2.Scale( v - centerAnchor, canvas.sizeDelta ) + Vector2.Scale( rect.rect.size, GetAnchorOffSet( anchor ) );
        }

        public static Vector2 FromAbsolutePositionToCanvasPosition(this RectTransform rect, Vector2 v, RectTransform canvas, Vector2 pivot)
        {
            Vector2 centerAnchor = ( rect.anchorMax + rect.anchorMin ) * 0.5f;
            return Vector2.Scale( v - centerAnchor, canvas.rect.size ) + Vector2.Scale( rect.rect.size, pivot * 2.0f );
        }

        public static Vector2 FromCanvasPositionToAbsolutePosition(this RectTransform rect, RectTransform canvas)
        {
            return new Vector2( rect.anchoredPosition.x / canvas.sizeDelta.x, rect.anchoredPosition.y / canvas.sizeDelta.y ) + rect.anchorMin;
        }

        private static Vector2 GetAnchorOffSet(UIAnchor anchor)
        {
            switch (anchor)
            {
                case UIAnchor.UpperLeft:
                return new Vector2( 0.5f, -0.5f );
                case UIAnchor.UpperCenter:
                return new Vector2( 0.0f, -0.5f );
                case UIAnchor.UpperRight:
                return new Vector2( -0.5f, -0.5f );
                case UIAnchor.MiddleLeft:
                return new Vector2( 0.5f, 0.0f );
                case UIAnchor.MiddleCenter:
                return new Vector2( 0.0f, 0.0f );
                case UIAnchor.MiddleRight:
                return new Vector2( -0.5f, 0.0f );
                case UIAnchor.LowerLeft:
                return new Vector2( 0.5f, 0.5f );
                case UIAnchor.LowerCenter:
                return new Vector2( 0.0f, 0.5f );
                case UIAnchor.LowerRight:
                return new Vector2( -0.5f, 0.5f );

            }

            return Vector2.zero;

        }

        
    }
}

