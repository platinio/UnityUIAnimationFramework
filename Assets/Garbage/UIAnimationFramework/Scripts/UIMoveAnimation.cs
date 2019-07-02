using UnityEngine;
using System.Collections.Generic;
using Platinio.TweenEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace PlatinioUITweeen
{
    [System.Serializable]
    public class UIMoveAnimation : Animation
    {       
        [SerializeField] private List<Vector2>      m_path      = null;
        [SerializeField] private List<float>        m_pathTime  = null;

        private List<Vector2>   m_processedPath         = null;        
        private int             m_currentPathIndex      = 0;
        private List<float>     m_pathDistance          = new List<float>();
        private float           m_totalPathDistance      = 0.0f;
        private Vector2         m_startPosition         = Vector2.zero;

        public List<Vector2> ProcessedPath  { get { return ProcessPath(m_path); }                           }
        public List<Vector2> Path           { get { return m_path;              } set { m_path = value; }   }

       
        public override void Construct(UIAnimator animator)
        {
            base.Construct(animator);

            //process the path
            m_processedPath = ProcessPath(m_path);

            //first get path distance
            m_pathDistance      = new List<float>();
            m_totalPathDistance = 0.0f;

            for (int n = 0; n < m_processedPath.Count - 1; n++)
            {
                float d = Vector2.Distance(m_processedPath[n], m_processedPath[n + 1]);

                m_pathDistance.Add( d );
                m_totalPathDistance += d;
            }

            m_pathTime = new List<float>();

            for (int n = 0 ; n < m_processedPath.Count - 1 ; n++)
            {
                m_pathTime.Add( (m_pathDistance[n] / m_totalPathDistance) * m_length );
                Debug.Log(m_pathTime[n]);
            }
        }

        public override void Play(bool loop , float t)
        {
            m_startPosition = m_rect.anchoredPosition;
            m_length = t;

            base.Play(loop , t);

            m_currentPathIndex = 0;
            GoToNextNode();
        }

        private void GoToNextNode()
        {
            float distanceToNextNode = Vector2.Distance(m_processedPath[m_currentPathIndex], m_processedPath[m_currentPathIndex + 1]);

            PlatinioTween.instance.Vector3Tween(m_processedPath[m_currentPathIndex], m_processedPath[m_currentPathIndex+1] , (distanceToNextNode / m_totalPathDistance) * m_length ).SetOnUpdate(delegate(Vector3 v) 
            {
                m_rect.anchoredPosition = v;
            }).SetOnComplete(delegate 
            {
                m_currentPathIndex++;

                if (m_currentPathIndex < m_path.Count - 1)
                    GoToNextNode();
                else
                {
                    #if UNITY_EDITOR
                    if (EditorApplication.isPlaying)
                    {
                        if (m_loop)
                            Play(m_loop, m_length);
                    }
                    else
                        Reset();
                                        
                    #else
                    if(m_loop)
                        Play(m_loop , m_length);
                    #endif

                }
            }).SetEase(m_ease);
        }

        public void EvaluateAtTime(float t)
        {
            t *= Length;

            int index = GetIndexAtTime(t);

            Debug.Log("index: " + index);

            float distanceToNextNode = Vector2.Distance(m_processedPath[index], m_processedPath[index + 1]);

            float passTime = 0.0f;

            for (int n = 0; n < index; n++)
            {
                passTime += m_pathTime[n];
            }

            t -= passTime;

            Vector3 change = m_processedPath[index + 1] - m_processedPath[index];
            m_rect.anchoredPosition = Equations.ChangeVector(t, m_processedPath[index], change , (distanceToNextNode / m_totalPathDistance) * m_length , m_ease);
        }

        private int GetIndexAtTime(float t)
        {
            float time = 0.0f;

            for (int n = 0; n < m_pathTime.Count ; n++)
            {
                time += m_pathTime[n];

                if (time >= t)
                    return n;
            }

            return 0;
        }

        private List<Vector2> ProcessPath(List<Vector2> path)
        {
            List<Vector2> processedPath = new List<Vector2>();

            for (int n = 0; n < m_path.Count; n++)
            {
                processedPath.Add(Vector2.Scale(m_path[n] - m_rect.anchorMin, m_animator.Canvas.sizeDelta));

            }

            return processedPath;
        }

        protected override void Reset()
        {            
            m_rect.anchoredPosition = m_startPosition;
        }
    }

}

