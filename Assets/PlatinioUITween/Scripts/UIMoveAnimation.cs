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
        private float m_pathDistance = 0.0f;

        public List<Vector2> ProcessedPath
        {
            get
            {
                return ProcessPath(m_path);
            }
        }

        public List<Vector2> Path { get { return m_path; } set { m_path = value; } }

       
        public override void Construct(UIAnimator animator)
        {
            base.Construct(animator);

            m_processedPath = ProcessPath(m_path);

            //first get path distance
            m_pathDistance = 0.0f;

            for (int n = 0; n < m_processedPath.Count - 1; n++)
            {
                m_pathDistance += Vector2.Distance(m_processedPath[n], m_processedPath[n + 1]);
            }
        }

        public override void Play(bool loop = false)
        {            
            base.Play(loop);
            m_currentPathIndex = 0;

            GoToNextNode();
        }

        private void GoToNextNode()
        {
            float distanceToNextNode = Vector2.Distance(m_processedPath[m_currentPathIndex], m_processedPath[m_currentPathIndex + 1]);

            PlatinioTween.instance.Vector3Tween(m_processedPath[m_currentPathIndex], m_processedPath[m_currentPathIndex+1] , (distanceToNextNode / m_pathDistance) * m_length ).SetOnUpdate(delegate(Vector3 v) 
            {
                m_rect.anchoredPosition = v;
            }).SetOnComplete(delegate 
            {
                m_currentPathIndex++;

                if (m_currentPathIndex < m_path.Count - 1)
                    GoToNextNode();
                else if(m_loop)
                {
                    Play(false);
                }
            }).SetEase(m_ease);
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
       
    }

}

