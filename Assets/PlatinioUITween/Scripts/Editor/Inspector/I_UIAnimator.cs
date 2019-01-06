using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Platinio.TweenEngine;

namespace PlatinioUITweeen
{
    [CustomEditor(typeof(UIAnimator))]
    public class I_UIAnimator : Editor
    {
        private UIAnimator m_animator = null;
        private Vector2 position = new Vector2();
        private float spaceY = 5.0f;
        private float spaceX = 10.0f;
        private float width = 35.0f;
        private float height = 55.0f;

        public override void OnInspectorGUI()
        {
            if (GUILayout.Button("Open Animation Editor"))
            {
                W_UIAnimator.Init((UIAnimator) target);
            }

            /*
           if(m_animator == null)
                m_animator = (UIAnimator) target;
            for (int n = 0 ; n < m_animator.AnimationList.Count ; n++)
            {
                DrawUIAnimation(m_animator.AnimationList[n]);

                if (GUILayout.Button("Play"))
                {
                    m_animator.AnimationList[n].Play();
                    Debug.Log("click");
                }
            }
            */
        }

        private void DrawUIAnimation(UIAnimation animation)
        {                       
            
            
            animation.Name = EditorGUILayout.TextField( "Name" , animation.Name );
            animation.UseMoveAnimation = EditorGUILayout.Toggle("Use Move" , animation.UseMoveAnimation);           

            if (animation.UseMoveAnimation)
            {
                if(animation.MoveAnimation == null)
                    animation.MoveAnimation = new UIMoveAnimation();
                DrawMoveAnimation(animation.MoveAnimation);
            }
               
        }

        private void DrawMoveAnimation(UIMoveAnimation moveAnimation)
        {
            DrawAnimation(moveAnimation);
            EditorGUILayout.LabelField("Path");

            if(moveAnimation.Path == null)
                moveAnimation.Path = new List<Vector2>();

            for (int n = 0 ; n < moveAnimation.Path.Count ; n++)
            {
                moveAnimation.Path[n] = EditorGUILayout.Vector2Field("P" + (n + 1) , moveAnimation.Path[n]);

                if(GUILayout.Button("-"))
                    moveAnimation.Path.RemoveAt(n);
            }

            if (GUILayout.Button("+"))
                moveAnimation.Path.Add(new Vector2());

        }

        private void DrawAnimation(Animation animation)
        {
            animation.Length    = EditorGUILayout.FloatField("Length", animation.Length);
            animation.Ease      = (Ease)EditorGUILayout.EnumPopup("Ease" , animation.Ease);
        }
    }

}

