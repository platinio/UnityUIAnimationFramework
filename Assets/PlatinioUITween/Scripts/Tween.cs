using UnityEngine;
using Platinio.TweenEngine;

namespace PlatinioUITweeen
{
    public enum AnimationMode
    {
        None,
        Ease,
        AnimationCurve
    }

    public abstract class Tween : MonoBehaviour
    {
        [SerializeField] protected AnimationMode    m_animationMode = AnimationMode.None;
        [SerializeField] protected Ease             m_ease          = Ease.EaseInBack;
        [SerializeField] protected bool             m_playOnAwake   = false;
        [SerializeField] protected float            m_time          = 0.0f;
        [SerializeField] protected float            m_delay         = 0.0f;

        public abstract void Play(float delay = 0.0f);
        public abstract void PlayBack();
        public abstract void Stop();
        
    }

}

