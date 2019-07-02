using UnityEngine;
using UnityEngine.Events;
using Platinio.TweenEngine;
using System.Collections.Generic;

namespace PlatinioUITweeen
{

    public class MoveTween : Tween
    {
        [SerializeField] private List<Vector2> m_path = null;
        [SerializeField] private UnityEvent m_onComplete = null;
        [SerializeField] private RectTransform m_canvas = null;

        private RectTransform m_rectTransform = null;
        private float m_pathDistance = 0.0f;
        private Vector3 m_startingPosition = Vector3.zero;
        private int m_currentPathIndex = 0;
        

        private List<Vector2> m_processedPath = null;
       
        private void Awake()
        {
            

            m_startingPosition = transform.position;

            m_rectTransform = GetComponent<RectTransform>();

            GameObject node = new GameObject("Node");
            node.transform.parent = transform.parent;

                      
            
            //first get path distance
            m_pathDistance = 0.0f;

            for (int n = 0; n < m_path.Count - 1; n++)
            {
                //m_pathDistance += Vector2.Distance(m_path[n].transform.position, m_path[n + 1].transform.position);
            }

           

           
        }

        private void Start()
        {
            m_processedPath = new List<Vector2>();
            for (int n = 0; n < m_path.Count; n++)
            {
                m_processedPath.Add(Vector2.Scale(m_path[n] - m_rectTransform.anchorMin, m_canvas.sizeDelta ) );
                Debug.Log(m_processedPath[n]);
            }

            Debug.Log("Canvas size " + m_canvas.sizeDelta);
            
            if (m_playOnAwake)
                Play(m_delay);

        }

        private void OnDrawGizmosSelected()
        {
            if(m_path.Count < 2 || m_canvas == null)
                return;

            if(m_rectTransform == null)
                m_rectTransform = GetComponent<RectTransform>();

            Vector2 from = Vector2.Scale(m_path[0], m_canvas.rect.size * m_canvas.localScale.x);
            Vector2 to = Vector2.Scale(m_path[1], m_canvas.rect.size * m_canvas.localScale.x);

            Gizmos.DrawLine(from, to);

            /*
            if(m_path.Count == 0)
                return;

            for (int n = 0; n < m_path.Count; n++)
            {
                if (m_path[n] == null)
                    return;
            }

                Gizmos.color = Color.cyan;

            Gizmos.DrawIcon(transform.position, "Point", false);
            Gizmos.DrawLine(transform.position , m_path[0].transform.position);

            for (int n = 0 ; n < m_path.Count ; n++)
            {                
                Gizmos.DrawIcon(m_path[n].transform.position, "Point", false);    
                
                if(n < m_path.Count - 1)
                    Gizmos.DrawLine(m_path[n].transform.position , m_path[n+1].transform.position  );
            }
            */

        }

        public override void Play(float delay = 0.0f)
        {
            //if (m_path.Count == 1)
           // {               
            
                BaseTween tween = PlatinioTween.instance.Vector3Tween(m_processedPath[0], m_processedPath[1], m_time).SetOnUpdate(delegate (Vector3 v)
                {
                    m_rectTransform.anchoredPosition = v;
                }).SetOnComplete(delegate { m_onComplete.Invoke(); });

                if(m_animationMode == AnimationMode.Ease)
                    tween.SetEase(m_ease);
                    
                //if(delay > 0.0f)
            //}

            //else
            //{
                //GoToNextPathNode(m_currentPathIndex);

            //}

            
            
        }

        private void GoToNextPathNode(int pathIndex , bool playback = false)
        {
            /*
            if (pathIndex == m_path.Count || pathIndex < 0)
            {
                m_onComplete.Invoke();
                return;
            }

            m_currentPathIndex = pathIndex;
            
            float distanceToNextNode = Vector2.Distance(transform.position, m_path[pathIndex].transform.position);

            BaseTween tween = PlatinioTween.instance.Vector3Tween(new Vector3(m_rectTransform.anchoredPosition.x, m_rectTransform.anchoredPosition.y, 0.0f), new Vector3(m_path[pathIndex].anchoredPosition.x, m_path[pathIndex].anchoredPosition.y, 0.0f), m_time * (distanceToNextNode / m_pathDistance)).SetOnUpdate(delegate (Vector3 v)
            {
                m_rectTransform.anchoredPosition = new Vector2(v.x, v.y);
            }).SetOnComplete(delegate { GoToNextPathNode(pathIndex + (playback? -1 : 1)); });

            if (m_animationMode == AnimationMode.Ease)
                tween.SetEase(m_ease);
                */
        }
                
        public override void PlayBack()
        {
            /*
            if (m_path.Count == 1)
            {
                BaseTween tween = PlatinioTween.instance.Vector3Tween(new Vector3(m_rectTransform.anchoredPosition.x, m_rectTransform.anchoredPosition.y, 0.0f), new Vector3(m_path[0].anchoredPosition.x, m_path[1].anchoredPosition.y, 0.0f), m_time).SetOnUpdate(delegate (Vector3 v)
                {
                    m_rectTransform.anchoredPosition = new Vector2(v.x, v.y);
                }).SetOnComplete(delegate { m_onComplete.Invoke(); });

                if (m_animationMode == AnimationMode.Ease)
                    tween.SetEase(m_ease);
                //if (delay > 0.0f)
                //    tween.SetDelay(delay);
            }

            else
            {
                GoToNextPathNode(m_currentPathIndex, true);

            }
            */
        }

        public override void Stop()
        {
            
        }
    }

}

