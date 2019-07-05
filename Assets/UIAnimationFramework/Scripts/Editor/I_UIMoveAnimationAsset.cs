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

        private UIMoveAnimationAsset inspectedScript = null;

        private Vector2 saveRectPosition = Vector2.zero;

        protected override void Awake()
        {
            base.Awake();

            startPosition = serializedObject.FindProperty( "startPosition" );
            endPosition = serializedObject.FindProperty( "endPosition" );
            pivotPreset = serializedObject.FindProperty( "pivotPreset" );
            pivot = serializedObject.FindProperty( "pivot" );

            inspectedScript = target as UIMoveAnimationAsset;
            
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

        protected override void OnDisable()
        {
            base.OnDisable();
          
            if (inspectedScript.Rect != null)
            {
                inspectedScript.Rect.anchoredPosition = saveRectPosition;               
            }
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            if (inspectedScript.Rect != null)
            {
                saveRectPosition = inspectedScript.Rect.anchoredPosition;
                
            }
                
        }

        protected override void OnSceneGUI(SceneView sceneView)        
        {

            if (( target as UIMoveAnimationAsset ).Canvas == null)
                return;

            if (this.startPosition == null)
                Awake();

            Vector3 startPosition = this.startPosition.vector2Value;
            startPosition.z = 0.0f;
            startPosition = Vector3.Scale( startPosition, inspectedScript.Canvas.rect.size );

            Vector3 endPosition = this.endPosition.vector2Value;
            endPosition.z = 0.0f;
            endPosition = Vector3.Scale( endPosition, inspectedScript.Canvas.rect.size );

            Handles.DrawLine( startPosition, endPosition );

            //position handle
          
            EditorGUI.BeginChangeCheck();
            Vector3 newTargetPosition = Handles.PositionHandle( startPosition, Quaternion.identity );
           
            this.startPosition.vector2Value = new Vector2( newTargetPosition.x / inspectedScript.Canvas.rect.size.x, newTargetPosition.y / inspectedScript.Canvas.rect.size.y );

            serializedObject.ApplyModifiedProperties();

        }

       
       
    }
}

