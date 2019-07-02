using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using Platinio.TweenEngine;

public class UIMoveControllerBehaivour : PlayableBehaviour
{
    public Transform trasnform = null;
    public Vector3 position = Vector2.zero;
    public Vector3 initialPosition = Vector3.zero;
    public float currentTime = 0.0f;
    public BaseTween baseTween = null;

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        float duration = (float) playable.GetDuration();
        float passedTime = (float) playable.GetTime();

        Debug.Log("Playable get time " + playable.GetTime());
        Debug.Log("Playable duration " + playable.GetDuration());

        float t = duration - passedTime;

        Vector3 change = position - initialPosition;
        trasnform.position = Equations.ChangeVector( t, position, change, duration , Ease.Linear);

    }

}
