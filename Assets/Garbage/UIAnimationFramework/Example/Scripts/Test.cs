using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlatinioUITweeen;

public class Test : MonoBehaviour
{

    public string m_animationName = null;

    private void Start()
    {
        GetComponent<UIAnimator>().Play(m_animationName);
    }
}
