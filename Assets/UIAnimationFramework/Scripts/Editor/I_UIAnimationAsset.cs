using UnityEngine;
using UnityEditor;

namespace Platinio.EditorCode
{
    public abstract class I_UIAnimationAsset : Editor
    {
        protected SerializedProperty rect = null;
        protected SerializedProperty canvas = null;
        protected SerializedProperty ease = null;

        protected virtual void Awake()
        {
            rect = serializedObject.FindProperty("rect");
            canvas = serializedObject.FindProperty("canvas");
            ease = serializedObject.FindProperty("ease");
        }

        public void OnEnable()
        {
            SceneView.onSceneGUIDelegate += OnSceneGUI;
        }

        public void OnDisable()
        {
            SceneView.onSceneGUIDelegate -= OnSceneGUI;
        }

        public override void OnInspectorGUI()
        {
            EditorTools.PropertyField(rect);
            EditorTools.PropertyField( canvas );
            EditorTools.PropertyField(ease);

            CustomInspector();

            serializedObject.ApplyModifiedProperties();
        }
       

        protected virtual void OnSceneGUI(SceneView sceneView)
        {
            
        }

        protected abstract void CustomInspector();
    }

}

