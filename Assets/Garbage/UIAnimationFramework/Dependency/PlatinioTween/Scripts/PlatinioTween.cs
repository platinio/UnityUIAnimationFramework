
using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEditor;

namespace Platinio.TweenEngine
{    
    
    /// <summary>
    /// Tween engine
    /// </summary>

    public class PlatinioTween : Singleton<PlatinioTween>
    {
        #region PRIVATE
        [SerializeField] private List<BaseTween> m_tweens    = new List<BaseTween>();
        private int             m_counter   = 0;
        #endregion

        #region UNITY_EVENTS
        protected override void Awake()
        {
            base.Awake();
            m_tweens = new List<BaseTween>();
        }

        private float m_lastEditorTime = float.MinValue;

        private void Update()
        {
           

            for (int n = 0 ; n < m_tweens.Count ; n++)
            {               
                m_tweens[n].Update(Time.deltaTime);
            }
        }

        #if UNITY_EDITOR
        private void EditorUpdate()
        {
            if (EditorApplication.isPlaying)
                return;

            float deltaTime = (float)UnityEditor.EditorApplication.timeSinceStartup - m_lastEditorTime;
            m_lastEditorTime = (float)UnityEditor.EditorApplication.timeSinceStartup;

            for (int n = 0; n < m_tweens.Count; n++)
            {
                m_tweens[n].Update(deltaTime);
            }

        }
        #endif


        #endregion

        private int GenerateId()
        {
            try
            {
                m_counter++;
            }
            catch (OverflowException)
            {
                m_counter = 0;
            }

            return m_counter;
        }

        private BaseTween ProcessTween(BaseTween tween)
        {
            tween.SetOnComplete( delegate { m_tweens.Remove(tween); } );
            m_tweens.Add(tween);

            EditorApplication.update = EditorUpdate;

            return tween;
        }

        public void CancelTween(int id)
        {            

            for (int n = 0; n < m_tweens.Count; n++)
            {
                if (m_tweens[n].id == id)
                {
                    m_tweens.RemoveAt(n);
                    break;
                }
            }
        }

        public void CancelAllTweens()
        {
            m_tweens = new List<BaseTween>();
        }


        #region TWEENS
        public BaseTween Vector3Tween(Vector3 from , Vector3 to , float t)
        {
            Vector3Tween tween = new Vector3Tween(from , to , t , GenerateId() );
            return ProcessTween(tween);           
        }

        public BaseTween ValueTween(float from , float to , float t)
        {
            ValueTween tween = new ValueTween(from , to , t , GenerateId());
            return ProcessTween(tween);
        }

        #region MOVE
        public BaseTween Move(Transform obj , Transform to , float t)
        {
            MoveTween tween = new MoveTween(obj , to , t , GenerateId());
            return ProcessTween(tween);
        }

        public BaseTween Move(Transform obj , Vector3 to , float t)
        {
            Vector3Tween tween = new Vector3Tween(obj.position , to , t , GenerateId());
            tween.SetOnUpdate((Vector3 pos) => 
            {
                obj.position = pos;
            });
            return ProcessTween(tween);
        }

        public BaseTween Move(GameObject obj , Transform to , float t)
        {
            return Move(obj.transform , to , t);
        }

        public BaseTween Move(GameObject obj, Vector3 to, float t)
        {
            return Move(obj.transform, to, t);
        }

        public BaseTween Move(GameObject obj, GameObject to, float t)
        {
            return Move(obj.transform, to.transform, t);
        }

        public BaseTween Move(Transform obj, GameObject to, float t)
        {
            return Move(obj, to.transform, t);
        }

        public BaseTween Move(RectTransform rect , Vector2 pos , float t)
        {
            return Vector3Tween(new Vector3(rect.position.x , rect.position.y , 0.0f) , new Vector3(pos.x, pos.y , 0.0f) , t).SetOnUpdate((Vector3 value) =>
            {
                rect.anchoredPosition = new Vector2(value.x , value.y);
            });
        }

        #endregion

        #region SCALE
        public BaseTween ScaleX(Transform obj , float value , float t)
        {
            return ValueTween(obj.localScale.x , value , t).SetOnUpdate((float v) => 
            {
                Vector3 currentScale = obj.localScale;
                currentScale.x = v;
                obj.localScale = currentScale;
            });
        }

        public BaseTween ScaleX(GameObject obj, float value, float t)
        {
            return ScaleX(obj.transform , value , t);
        }

        public BaseTween ScaleY(Transform obj, float value, float t)
        {
            return ValueTween(obj.localScale.y, value, t).SetOnUpdate((float v) =>
            {
                Vector3 currentScale = obj.localScale;
                currentScale.y = v;
                obj.localScale = currentScale;
            });
        }

        public BaseTween ScaleY(GameObject obj, float value, float t)
        {
            return ScaleY(obj.transform, value, t);
        }

        public BaseTween ScaleZ(Transform obj, float value, float t)
        {
            return ValueTween(obj.localScale.z, value, t).SetOnUpdate((float v) =>
            {
                Vector3 currentScale = obj.localScale;
                currentScale.z = v;
                obj.localScale = currentScale;
            });
        }

        public BaseTween ScaleZ(GameObject obj, float value, float t)
        {
            return ScaleX(obj.transform, value, t);
        }

        #endregion

        #endregion

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
            LowerRight
        }
        
        public static Vector2 FromAbsolutePositionToCanvasPosition(Vector2 v, RectTransform rect, RectTransform canvas , UIAnchor anchor = UIAnchor.MiddleCenter)
        {
            Vector2 centerAnchor = (rect.anchorMax + rect.anchorMin) * 0.5f;
            Vector2 diff = centerAnchor - v;
            Debug.Log("Position " + v);
            Debug.Log("Center Anchor: " + centerAnchor );
            Debug.Log("Canvas Size Delta: " + canvas.sizeDelta);
            return Vector2.Scale( v - centerAnchor, canvas.sizeDelta ) + Vector2.Scale( rect.rect.size, AnchorOffSet( anchor ) );
        }

        private static Vector2 AnchorOffSet(UIAnchor anchor)
        {
            switch (anchor)
            {
                case UIAnchor.UpperLeft:
                    return new Vector2(0.5f , -0.5f);
                case UIAnchor.UpperCenter:
                    return new Vector2( 0.0f, -0.5f );
                case UIAnchor.UpperRight:
                    return new Vector2(-0.5f , -0.5f);
                case UIAnchor.MiddleLeft:
                    return new Vector2(0.5f , 0.0f);
                case UIAnchor.MiddleCenter:
                    return new Vector2(0.0f , 0.0f);
                case UIAnchor.MiddleRight:
                    return new Vector2(-0.5f , 0.0f);
                case UIAnchor.LowerLeft:
                    return new Vector2(0.5f , 0.5f);
                case UIAnchor.LowerCenter:
                    return new Vector2(0.0f, 0.5f);
                case UIAnchor.LowerRight:
                    return new Vector2(-0.5f , 0.5f);

            }

            return Vector2.zero;

        }

        public static Vector2 FromCanvasPositionToAbsolutePosition(RectTransform rect, RectTransform canvas)
        {
            return new Vector2( rect.anchoredPosition.x / canvas.sizeDelta.x, rect.anchoredPosition.y / canvas.sizeDelta.y ) + rect.anchorMin;
        }

       
    }

}

