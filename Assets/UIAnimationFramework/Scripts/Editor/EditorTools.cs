using UnityEditor;
using UnityEngine;

namespace Platinio.EditorCode
{
    public static class EditorTools
    {
        public static void PropertyField(SerializedProperty property, string label = null, bool showChildren = false)
        {
            EditorGUILayout.PropertyField( property, new GUIContent( label == null ? property.displayName : label ), showChildren );
        }

        public static void PropertyField(SerializedProperty property, bool showChildren)
        {
            PropertyField( property, null, showChildren );
        }

        public static void MinMaxFloatSlider(SerializedProperty min, SerializedProperty max, float minLimit, float maxLimit)
        {
            PropertyField( min );
            PropertyField( max );

            float minValue = min.floatValue;
            float maxValue = max.floatValue;

            EditorGUILayout.MinMaxSlider( ref minValue, ref maxValue, minLimit, maxLimit );

            min.floatValue = minValue;
            max.floatValue = maxValue;
        }



        public static void DrawTittle(string text)
        {
            EditorGUILayout.Space();
            EditorGUILayout.LabelField( text, EditorStyles.boldLabel );
            EditorGUILayout.Space();
        }
    }

}

