using UnityEditor;
using UnityEngine;

public class InspectTextureAttribute : PropertyAttribute {}

[CustomPropertyDrawer(typeof(InspectTextureAttribute))]
public class InspectTextureDrawer : PropertyDrawer
{
    //Button settings
    private const float buttonPadding = 10f;
    
    //Messages
    private const string buttonDisplayText = "Inspect";
    private const string incorrectDrawerUsageMessage = "Type mismatch! Please use [InspectTexture] only with Texture2D!";
    
    //Cache
    private Rect buttonRect;
    private float labelWidth;
    private float buttonWidth;
    
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        if (!IsTexture2D(property))
        {
            EditorGUI.HelpBox(position,incorrectDrawerUsageMessage, MessageType.Error);
            return;
        }
        //Add property to inspector GUI
        EditorGUI.ObjectField(position, property, typeof(Texture2D), label);

        labelWidth = EditorGUIUtility.labelWidth;
        buttonWidth = labelWidth / 2;

        //Disable button UI if property doesn't have a value
        GUI.enabled = property.objectReferenceValue != null;
            
        //Add button to inspector UI
        buttonRect = new Rect((labelWidth - buttonWidth) + buttonPadding, position.y, buttonWidth, position.height);
        if (GUI.Button(buttonRect, buttonDisplayText))
        {
            InspectTextureMenu.OpenWindow((Texture2D) property.objectReferenceValue);
        }
    }
    
    private bool IsTexture2D(SerializedProperty property) => 
        property.propertyType == SerializedPropertyType.ObjectReference &&
        GetTrimmedPropertyName(property.type) == "Texture2D";
    
    private string GetTrimmedPropertyName(string property) => property.Substring(6, property.Length - 7);
}
