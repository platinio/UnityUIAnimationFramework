using UnityEngine;

namespace PlatinioUITweeen
{
    public class UIAnimator : MonoBehaviour
    {
        [SerializeField] private UIMoveAnimation m_moveAnimation = null;

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


        private void Start()
        {
            m_moveAnimation.Construct(this);
            m_moveAnimation.Play();
        }

        private void OnDrawGizmosSelected()
        {
            
            m_moveAnimation.Construct(this);

            for (int n = 0 ; n < m_moveAnimation.Path.Count - 1 ; n++)
            {
                Vector2 from = Vector2.Scale(m_moveAnimation.Path[n], m_canvas.rect.size * m_canvas.localScale.x);
                Vector2 to = Vector2.Scale(m_moveAnimation.Path[n+1], m_canvas.rect.size * m_canvas.localScale.x);

                Gizmos.DrawLine(from, to);
            }

            
        }

        }

}

