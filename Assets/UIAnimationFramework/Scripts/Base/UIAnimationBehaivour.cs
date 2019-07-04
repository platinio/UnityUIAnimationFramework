using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using Platinio.TweenEngine;

namespace Platinio.UIAnimation
{
    public abstract class UIAnimationBehaivour : PlayableBehaviour
    {
        public RectTransform rect = null;
        public RectTransform canvas = null;
        public Ease ease = Ease.Linear;
        public float duration = 0.0f;
        public float passedTime = 0.0f;

        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            duration = (float) playable.GetDuration();
            passedTime = (float) playable.GetTime();

            float t = duration - passedTime;

            EvaluteAtTime(t);

        }
        

        protected abstract void EvaluteAtTime(float t);

    }
}

