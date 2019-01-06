using System.Collections.Generic;
using UnityEngine;


namespace PlatinioUITweeen
{
    [System.Serializable]
    public class UIAnimation 
    {
        [SerializeField] private string             m_name              = null;
        [SerializeField] private UIMoveAnimation    m_moveAnimation     = null;
        [SerializeField] private bool               m_useMoveAnimation  = false;
        [SerializeField] private bool               m_playOnAwake       = false;
        [SerializeField] private bool               m_loop              = false;

        public string Name                          { get { return m_name; } set { m_name = value; } }
        public UIMoveAnimation  MoveAnimation       { get { return m_moveAnimation;     } set { m_moveAnimation = value; } }
        public bool             UseMoveAnimation    { get { return m_useMoveAnimation;  } set {m_useMoveAnimation = value; } }
        public bool             PlayOnAwake         { get { return m_playOnAwake;       } set { m_playOnAwake = value; } }
        public bool             Loop                { get { return m_loop;              } set { m_loop = value; } }

        public void Play()
        {
            if(m_useMoveAnimation)
                m_moveAnimation.Play(Loop);

            Debug.Log("Loop " + m_loop);
        }
    }

}

