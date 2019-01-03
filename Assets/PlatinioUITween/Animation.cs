using Platinio.TweenEngine;
using UnityEngine;

namespace PlatinioUITweeen
{
    public abstract class Animation
    {
        [SerializeField] protected float       m_length    = 0.0f;
        protected bool        m_loop      = false;
        protected Ease        m_ease      = Ease.Linear;
        protected BaseTween   m_tween     = null;
        protected RectTransform m_rect = null;
        protected UIAnimator m_animator = null;

        public void Construct(UIAnimator animator)
        {
            m_animator = animator;
            m_rect = m_animator.GetComponent<RectTransform>();
        }

        public abstract void Play();
    }
}


