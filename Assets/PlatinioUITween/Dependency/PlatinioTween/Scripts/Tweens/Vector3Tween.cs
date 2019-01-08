﻿using UnityEngine;

namespace Platinio.TweenEngine
{
    
    public class Vector3Tween : BaseTween
    {
        #region PRIVATE
        private Vector3 m_from  = Vector3.zero;
        private Vector3 m_to    = Vector3.zero;
        #endregion

        #region PUBLIC
        public Vector3Tween(Vector3 from, Vector3 to, float t , int id)
        {
            m_from      = from;
            m_to        = to;
            m_duration  = t;
            m_id        = id;
        }

        /// <summary>
        /// called every frame
        /// </summary>
        public override void Update(float delta)
        {
            //wait a delay
            if (m_delay > 0.0f)
            {
                m_delay -= delta;
                return;
            }

            //start counting time
            m_currentTime += delta;

            //if time ends
            if (m_currentTime >= m_duration)
            {
                if (m_onUpdateVector3 != null)
                    m_onUpdateVector3(m_to);

                m_onComplete();                
                return;
            }

            //get new value
            Vector3 change  = m_to - m_from;
            Vector3 value   = Equations.ChangeVector(m_currentTime, m_from, change, m_duration, m_ease);

            //call update if we have it
            if (m_onUpdateVector3 != null)
                m_onUpdateVector3(value);
        }
        #endregion
    }

}
