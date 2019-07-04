using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platinio
{
    public enum PivotPreset
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
        public static Vector2 FromAbsolutePositionToCanvasPosition(this RectTransform rect, Vector2 v, RectTransform canvas, PivotPreset anchor = PivotPreset.MiddleCenter)
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

        private static Vector2 GetAnchorOffSet(PivotPreset anchor)
        {
            switch (anchor)
            {
                case PivotPreset.UpperLeft:
                return new Vector2( 0.5f, -0.5f );
                case PivotPreset.UpperCenter:
                return new Vector2( 0.0f, -0.5f );
                case PivotPreset.UpperRight:
                return new Vector2( -0.5f, -0.5f );
                case PivotPreset.MiddleLeft:
                return new Vector2( 0.5f, 0.0f );
                case PivotPreset.MiddleCenter:
                return new Vector2( 0.0f, 0.0f );
                case PivotPreset.MiddleRight:
                return new Vector2( -0.5f, 0.0f );
                case PivotPreset.LowerLeft:
                return new Vector2( 0.5f, 0.5f );
                case PivotPreset.LowerCenter:
                return new Vector2( 0.0f, 0.5f );
                case PivotPreset.LowerRight:
                return new Vector2( -0.5f, 0.5f );

            }

            return Vector2.zero;

        }

        
    }
}

