using UnityEngine;
using UnityEditor;
using Platinio.UIAnimation;

namespace Platinio.EditorCode
{
    [CustomEditor( typeof( UIMoveAnimationAsset ) )]
    public class I_UIMoveAnimationAsset : I_UIAnimationAsset
    {
        private SerializedProperty startPosition = null;
        private SerializedProperty endPosition = null;
        private SerializedProperty pivotPreset = null;
        private SerializedProperty pivot = null;


        protected override void Awake()
        {
            base.Awake();

            startPosition = serializedObject.FindProperty( "startPosition" );
            endPosition = serializedObject.FindProperty( "endPosition" );
            pivotPreset = serializedObject.FindProperty( "pivotPreset" );
            pivot = serializedObject.FindProperty( "pivot" );
        }


        protected override void CustomInspector()
        {
            EditorTools.PropertyField( pivotPreset );

            if (pivotPreset.enumValueIndex == (int) PivotPreset.Custom)
            {
                EditorTools.PropertyField( pivot );
            }

            EditorTools.PropertyField( startPosition );
            EditorTools.PropertyField( endPosition );

        }

        protected override void OnSceneGUI(SceneView sceneView)
        {
            if (( target as UIMoveAnimationAsset ).Canvas == null)
                return;

            if (this.startPosition == null)
                Awake();

            Vector3 startPosition = this.startPosition.vector2Value;
            startPosition.z = 0.0f;
            startPosition = Vector3.Scale( startPosition, ( target as UIMoveAnimationAsset ).Canvas.rect.size );

            Vector3 endPosition = this.endPosition.vector2Value;
            endPosition.z = 0.0f;
            endPosition = Vector3.Scale( endPosition, ( target as UIMoveAnimationAsset ).Canvas.rect.size );

            Handles.DrawLine( startPosition, endPosition );
        }
       
    }
}

