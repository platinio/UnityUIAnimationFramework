using UnityEngine;
using UnityEditor;


namespace PlatinioUITweeen
{
    [CustomEditor(typeof(MoveTween))]
    public class I_MoveTweenInspector : Editor
    {
        private MoveTween m_target = null;

        private void Awake()
        {
            m_target = (MoveTween) target;
        }

        protected virtual void OnSceneGUI()
        {
            /*
            if(!Selection.Contains(((MoveTween) target).gameObject))
                return;

            for (int n = 0; n < m_target.Path.Count; n++)
            {
                if (m_target.Path[n] == null)
                    return;                
            }

            for (int n = 0; n < m_target.Path.Count; n++)
            {
                EditorGUI.BeginChangeCheck();
                Vector3 newTargetPosition = Handles.PositionHandle(m_target.Path[n].transform.position, Quaternion.identity);
                if (EditorGUI.EndChangeCheck())
                {
                    //Undo.RecordObject(example, "Change Look At Target Position");
                    m_target.Path[n].transform.position = newTargetPosition;
                    //example.Update();
                }

            }
            */
        }

    }

}

