using UnityEngine;
using System.Collections.Generic;

namespace PlatinioUITweeen
{
    public class UIAnimator : MonoBehaviour
    {
        [SerializeField] private List<UIAnimation> m_animationList = new List<UIAnimation>();

       
        private RectTransform m_canvas = null;

        
        public RectTransform Canvas
        {
            get
            {
                if(m_canvas == null)
                    m_canvas = transform.root.GetComponent<RectTransform>();


                return m_canvas;
            }
        }

        public List<UIAnimation> AnimationList { get { return m_animationList; } }


        private void Awake()
        {
            Construct();

            for (int n = 0 ; n < m_animationList.Count ; n++)
            {                
                if (m_animationList[n].PlayOnAwake)
                {
                    m_animationList[n].Play();                    
                }
            }
            
        }

        private void OnDrawGizmosSelected()
        {

            for (int n = 0; n < m_animationList.Count; n++)
            {

                if (m_animationList[n].UseMoveAnimation)
                {
                    m_animationList[n].MoveAnimation.Construct(this);

                    for (int j = 0; j < m_animationList[n].MoveAnimation.Path.Count - 1; j++)
                    {
                        Debug.Log("drawing line");
                        Vector2 from = Vector2.Scale(m_animationList[n].MoveAnimation.Path[j], Canvas.rect.size * Canvas.localScale.x);
                        Vector2 to = Vector2.Scale(m_animationList[n].MoveAnimation.Path[j + 1], Canvas.rect.size * Canvas.localScale.x);

                        Gizmos.DrawLine(from, to);
                    }
                }


            }


        }

        public void Construct()
        {
            for (int n = 0; n < m_animationList.Count; n++)
            {
                m_animationList[n].MoveAnimation.Construct(this);
            }
        }

        private UIAnimation FindAnimationByName(string animationName)
        {
            for (int n = 0 ; n < m_animationList.Count ; n++)
            {
                if (m_animationList[n].Name == animationName)
                {
                    return m_animationList[n];
                }
            }

            return null;
        }

        public void Play(string animationName)
        {
            UIAnimation animation = FindAnimationByName(animationName);
            
            if(animation != null)
                animation.Play();
        }

       

    }

}

