using UnityEngine;
using System.Collections.Generic;
using Platinio.TweenEngine;

namespace PlatinioUITweeen
{
    [System.Serializable]
    public class UIMoveAnimation : Animation
    {       
        [SerializeField] private List<Vector2> m_path = null;

        private List<Vector2> m_processedPath = null;
        private int m_currentPathIndex = 0;

        public List<Vector2> ProcessedPath
        {
            get
            {
                return ProcessPath(m_path);
            }
        }

        public List<Vector2> Path { get { return m_path; } }

        public override void Play()
        {
            m_processedPath = ProcessPath(m_path);
            m_currentPathIndex = 0;

            GoToNextNode();
        }

        private void GoToNextNode()
        {
            PlatinioTween.instance.Vector3Tween(m_processedPath[m_currentPathIndex], m_processedPath[m_currentPathIndex+1] , m_length / m_path.Count).SetOnUpdate(delegate(Vector3 v) 
            {
                m_rect.anchoredPosition = v;
            }).SetOnComplete(delegate 
            {
                m_currentPathIndex++;

                if(m_currentPathIndex < m_path.Count - 1)
                    GoToNextNode();
            });
        }

        private List<Vector2> ProcessPath(List<Vector2> path)
        {
            List<Vector2> processedPath = new List<Vector2>();

            for (int n = 0; n < m_path.Count; n++)
            {
                processedPath.Add(Vector2.Scale(m_path[n] - m_rect.anchorMin, m_animator.Canvas.sizeDelta));

                Debug.Log(processedPath[n]);
            }

            Debug.Log(m_animator.Canvas.sizeDelta);

            return processedPath;
        }
       
    }

}

