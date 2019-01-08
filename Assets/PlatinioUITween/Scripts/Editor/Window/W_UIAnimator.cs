using UnityEngine;
using UnityEditor;
using Platinio.TweenEngine;
using System.Collections.Generic;

namespace PlatinioUITweeen
{
    public class W_UIAnimator : EditorWindow
    {
        private UIAnimator m_animator = null;
        private Vector2 m_position = new Vector2();
        private Vector2 m_space = new Vector2();
        private float m_width = 150.0f;
        private float m_height = 20.0f;
        private float m_tabSpacing = 25.0f;
        private float m_sliderValue = 0.0f;

        public static void Init(UIAnimator animator)
        {
            // Get existing open window or if none, make a new one:
            W_UIAnimator window = (W_UIAnimator)EditorWindow.GetWindow(typeof(W_UIAnimator), false, "Animator Editor");
            window.minSize = new Vector2(420, 300);
        }

        private void OnGUI()
        {
            if (m_animator == null)
            {
                m_animator = Selection.activeGameObject.GetComponent<UIAnimator>();

                if(m_animator == null)
                    return;
            }

            m_position = new Vector2(20.0f, 55.0f);
            m_space = new Vector2(95.0f , 20.0f);

            float desireX = 0.0F;

            for (int n = 0; n < m_animator.AnimationList.Count; n++)
            {
                

                desireX = m_position.x;

                DrawUIAnimation(m_animator.AnimationList[n]);

                float newSliderValue = EditorGUI.Slider(new Rect(desireX, m_position.y += m_space.y += 5.0f, m_width * 2.0f, m_height) , m_sliderValue , 0.0f , 1.0f);

                if (newSliderValue != m_sliderValue)
                {
                    m_animator.Construct();

                    m_sliderValue = newSliderValue;
                    m_animator.AnimationList[n].EvaluateAtTime(newSliderValue);
                }

                if (GUI.Button(new Rect(desireX, m_position.y += m_space.y += 5.0f, m_width, m_height) , "Play"))
                {
                    m_animator.Construct();

                    m_animator.AnimationList[n].Play();                  
                }                
            }
           

            /*
            if (GUI.Button(new Rect(m_position.x, m_position.y += m_space.y += 5.0f, 20.0f, 20.0f), "+"))
            {
                m_animator.AnimationList.Add(new UIAnimation());
            }
            */
            
        }

        private void DrawUIAnimation(UIAnimation animation)
        {            
            EditorGUI.LabelField(new Rect(m_position.x, m_position.y , m_width, m_height) , "Animation " + animation.Name , EditorStyles.boldLabel);

            if (GUI.Button(new Rect(m_position.x + m_space.x + 30.0f, m_position.y, 20.0f, 20.0f), "-"))
            {
                m_animator.AnimationList.Remove(animation);
            }

            m_position.x += m_tabSpacing;

            GUIContent cont = new GUIContent("Name:", "The animation name");
            EditorGUI.LabelField(new Rect(m_position.x, m_position.y += m_space.y, m_width, m_height), cont);
            animation.Name = EditorGUI.TextField(new Rect(m_position.x + m_space.x, m_position.y, m_width, m_height), animation.Name);

            cont = new GUIContent("Length:", "Length of this animation in seconds");
            EditorGUI.LabelField(new Rect(m_position.x, m_position.y += m_space.y, m_width, m_height), cont);
            animation.Length = EditorGUI.FloatField(new Rect(m_position.x + m_space.x, m_position.y, m_width, m_height), animation.Length);

            cont = new GUIContent("Use Move:", "This animation use movement?");
            EditorGUI.LabelField(new Rect(m_position.x, m_position.y += m_space.y, m_width, m_height), cont);
            animation.UseMoveAnimation = EditorGUI.Toggle(new Rect(m_position.x + m_space.x, m_position.y, m_width, m_height), animation.UseMoveAnimation);

            cont = new GUIContent("Play On Awake:", "This animation use movement?");
            EditorGUI.LabelField(new Rect(m_position.x, m_position.y += m_space.y, m_width, m_height), cont);
            animation.PlayOnAwake = EditorGUI.Toggle(new Rect(m_position.x + m_space.x, m_position.y, m_width, m_height), animation.PlayOnAwake);

            cont = new GUIContent("Loop:", "This animation use movement?");
            EditorGUI.LabelField(new Rect(m_position.x, m_position.y += m_space.y, m_width, m_height), cont);
            animation.Loop = EditorGUI.Toggle(new Rect(m_position.x + m_space.x, m_position.y, m_width, m_height), animation.Loop);


            if (animation.UseMoveAnimation)
            {
                
                EditorGUI.LabelField(new Rect(m_position.x, m_position.y += m_space.y, m_width, m_height), "Movement" , EditorStyles.boldLabel);

                m_position.y += 5.0f;

                m_position.x += m_tabSpacing;

                if (animation.MoveAnimation == null)
                    animation.MoveAnimation = new UIMoveAnimation();

                DrawMoveAnimation(animation.MoveAnimation);
            }

        }

        private void DrawMoveAnimation(UIMoveAnimation moveAnimation)
        {
            DrawAnimation(moveAnimation);

            EditorGUI.LabelField(new Rect(m_position.x, m_position.y += m_space.y, m_width, m_height), "Path", EditorStyles.boldLabel);

            if (moveAnimation.Path == null)
                moveAnimation.Path = new List<Vector2>();

            while(moveAnimation.Path.Count < 2)
                moveAnimation.Path.Add(new Vector2());

            m_position.x += 10.0f;

            for (int n = 0; n < moveAnimation.Path.Count; n++)
            {                
                GUIContent cont = new GUIContent("P" + (n + 1), "Length of this animation in seconds");
                EditorGUI.LabelField(new Rect(m_position.x, m_position.y += m_space.y, m_width, m_height), cont , EditorStyles.boldLabel);
                moveAnimation.Path[n] = EditorGUI.Vector2Field(new Rect(m_position.x + 10.0f , m_position.y += m_space.y, m_width , m_height), "" ,moveAnimation.Path[n]);


                if (GUI.Button(new Rect(m_position.x + 10.0f + m_width + 5.0f, m_position.y, 15.0f, 15.0f), "-"))
                {
                    moveAnimation.Path.RemoveAt(n);
                }

                if (GUI.Button(new Rect(m_position.x + 10.0f + m_width + 5.0f + 18.0f, m_position.y, 18.0f, 15.0f), "+"))
                {
                    int newIndex = n + 1;

                    if (newIndex < moveAnimation.Path.Count)
                        moveAnimation.Path.Insert(n + 1, new Vector2());
                    else
                        moveAnimation.Path.Add(new Vector2());
                }

            }
          

        }

        private void DrawAnimation(Animation animation)
        {
            GUIContent cont = new GUIContent("Ease:", "Ease of this animation");
            EditorGUI.LabelField(new Rect(m_position.x, m_position.y += m_space.y, m_width, m_height), cont);
            animation.Ease = (Ease)EditorGUI.EnumPopup(new Rect(m_position.x + m_space.x, m_position.y, m_width, m_height), animation.Ease);           
        }
    }

}

