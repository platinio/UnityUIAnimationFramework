﻿
using UnityEngine;
using System.Collections.Generic;
using System;

namespace Platinio.TweenEngine
{    
    
    /// <summary>
    /// Tween engine
    /// </summary>
    public class PlatinioTween : Singleton<PlatinioTween>
    {
        #region PRIVATE
        private List<BaseTween> m_tweens    = null;
        private int             m_counter   = 0;
        #endregion

        #region UNITY_EVENTS
        protected override void Awake()
        {
            base.Awake();
            m_tweens = new List<BaseTween>();
        }

        private void Update()
        {
            for (int n = 0 ; n < m_tweens.Count ; n++)
            {                
                m_tweens[n].Update();
            }
        }
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

    }

}

