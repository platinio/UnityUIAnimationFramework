using Platinio.TweenEngine;
using UnityEngine;

namespace PlatinioUITweeen
{
    public abstract class Animation
    {
        [SerializeField] protected float    m_length    = 0.0f;
        [SerializeField] protected Ease     m_ease      = Ease.Linear;

        protected BaseTween     m_tween     = null;
        protected RectTransform m_rect      = null;
        protected UIAnimator    m_animator  = null;
        protected bool          m_loop      = false;

        public float Length { get { return m_length; } set { m_length = value; } }
        public Ease Ease { get { return m_ease; } set { m_ease = value; } }

        public virtual void Construct(UIAnimator animator)
        {
            m_animator = animator;
            m_rect = m_animator.GetComponent<RectTransform>();
        }

        public virtual void Play(bool loop = false)
        {
            m_loop = loop;
        }
    }
}


