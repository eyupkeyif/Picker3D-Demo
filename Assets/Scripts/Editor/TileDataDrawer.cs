using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(TileData))]
public class TileDataDrawer : PropertyDrawer
{
    public SerializedProperty _tileType;
    public SerializedProperty __checkPointAmount;
    public SerializedProperty _parentType;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position,label,property);
        _tileType = property.FindPropertyRelative("tileType");
        __checkPointAmount = property.FindPropertyRelative("checkPointAmout");
        _parentType = property.FindPropertyRelative("ParentType");

        Rect foldOutBox = new Rect(position.min.x,position.min.y,position.size.x,EditorGUIUtility.singleLineHeight);
        property.isExpanded = EditorGUI.Foldout(foldOutBox,property.isExpanded,label);
        
        if (property.isExpanded)
        {
            

            DrawTileProperty(position);

            if (_tileType.enumValueIndex==0)
            {
                DrawCollectableProperty(position);
            }

            else if(_tileType.enumValueIndex==1)
            {
                DrawCheckPointAmountProperty(position);

                if (__checkPointAmount.intValue<=0)
                {   
                    Rect drawArea = new Rect(position.min.x,position.min.y + EditorGUIUtility.singleLineHeight*3f,position.size.x,EditorGUIUtility.singleLineHeight);
                    EditorGUI.HelpBox(drawArea,"Please enter an amount to pass the checkpoint",MessageType.Warning);
                    
                }
            }
        }

        property.serializedObject.ApplyModifiedProperties();
        EditorGUI.EndProperty();

        
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        int totalHeight = 1;

        if (property.isExpanded)
        {
            totalHeight+=3;
        }

        return EditorGUIUtility.singleLineHeight*totalHeight;
    }
    public void DrawTileProperty(Rect position){
        float xPos=position.min.x;
        float yPos=position.min.y+EditorGUIUtility.singleLineHeight;
        float width=position.size.x;
        float height=EditorGUIUtility.singleLineHeight;

        Rect drawArea = new Rect(xPos,yPos,width,height);
        EditorGUI.PropertyField(drawArea,_tileType,new GUIContent("Tile"));

    }
    public void DrawCollectableProperty(Rect position){

        Rect drawArea = new Rect(position.min.x,position.min.y + EditorGUIUtility.singleLineHeight*2f,position.size.x,EditorGUIUtility.singleLineHeight);
        EditorGUI.PropertyField(drawArea,_parentType,new GUIContent("Collectable Type"));
    }
    public void DrawCheckPointAmountProperty(Rect position){

        Rect drawArea = new Rect(position.min.x,position.min.y + EditorGUIUtility.singleLineHeight*2f,position.size.x,EditorGUIUtility.singleLineHeight);
        EditorGUI.PropertyField(drawArea,__checkPointAmount,new GUIContent("CheckPoint Amount"));
    }


}
