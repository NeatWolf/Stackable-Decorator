using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace StackableDecorator
{
    public class MaxValueAttribute : StackableDecoratorAttribute
    {
#if UNITY_EDITOR
        private float m_Value;
#endif
        public MaxValueAttribute(float value)
        {
#if UNITY_EDITOR
            m_Value = value;
#endif
        }
#if UNITY_EDITOR
        public override float GetHeight(SerializedProperty property, GUIContent label, float height)
        {
            return height;
        }

        public override bool BeforeGUI(ref Rect position, ref SerializedProperty property, ref GUIContent label, ref bool includeChildren, bool visible)
        {
            if (!IsVisible()) return visible;
            if (!visible) return false;

            return true;
        }

        public override void AfterGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (!IsVisible()) return;

            float value = 0;
            switch (property.propertyType)
            {
                case SerializedPropertyType.Integer:
                    value = property.intValue;
                    break;
                case SerializedPropertyType.Float:
                    value = property.floatValue;
                    break;
            }
            var newvalue = Mathf.Min(value, m_Value);
            if (newvalue != value)
            {
                if (property.propertyType == SerializedPropertyType.Integer)
                    property.intValue = (int)newvalue;
                if (property.propertyType == SerializedPropertyType.Float)
                    property.floatValue = newvalue;
            }
        }
#endif
    }
}