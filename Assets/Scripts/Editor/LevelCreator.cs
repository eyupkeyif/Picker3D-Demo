using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class LevelCreator : EditorWindow {
    protected SerializedObject serializedObject;
    protected SerializedProperty serializedProperty;
    protected SerializedProperty levelNumber;
    protected SerializedProperty TileDatas;

    protected LevelData[] levels;
    public LevelData newLevel;

    private void OnGUI() {

        levels = GetAllInstances<LevelData>();
        serializedObject = new SerializedObject(newLevel);
        levelNumber = serializedObject.FindProperty("levelNumber");
        TileDatas = serializedObject.FindProperty("tileDatas");
        DrawProperties();

        if (GUILayout.Button("Save"))
        {
            if (newLevel.levelNumber.ToString()==null || newLevel.levelNumber==0)
            {
                newLevel.levelNumber = levels.Length+1;
            }

            AssetDatabase.CreateAsset(newLevel, "Assets/Resources/Levels/Level" + (levels.Length+1) + ".asset");
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            Close();
        }

        Apply();

    }

    protected void DrawProperties()
    {

        EditorGUILayout.LabelField("Level Number",EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(levelNumber);

        if (levelNumber.intValue<=0)
        {
            EditorGUILayout.HelpBox("Please enter a valid Level number",MessageType.Warning);
        }

        EditorGUILayout.PropertyField(TileDatas);
        LevelTileInfo();
        

    }
    private void LevelTileInfo()
    {
        TileDatas.arraySize=7;
        for (int i = 0; i < TileDatas.arraySize; i++)
        {
            if (i==TileDatas.arraySize-1)
            {
                serializedProperty = TileDatas.GetArrayElementAtIndex(i).FindPropertyRelative("tileType");
                serializedProperty.enumValueIndex=2;
            }
            else if (i%2==0 && i!=TileDatas.arraySize-1)
            {
                serializedProperty = TileDatas.GetArrayElementAtIndex(i).FindPropertyRelative("tileType");
                serializedProperty.enumValueIndex=0;
            }
            else
            {
                serializedProperty = TileDatas.GetArrayElementAtIndex(i).FindPropertyRelative("tileType");
                serializedProperty.enumValueIndex=1;
            } 
        }
       
    }

    public static T[] GetAllInstances<T>() where T:LevelData
    {
        string[] guids = AssetDatabase.FindAssets("t:" + typeof(T).Name);
        T[] a = new T[guids.Length];
        for (int i = 0; i < guids.Length; i++)
        {
            string path = AssetDatabase.GUIDToAssetPath(guids[i]);
            a[i]=AssetDatabase.LoadAssetAtPath<T>(path);
        }

        return a;
    }

    protected void Apply()
    {
        serializedObject.ApplyModifiedProperties();
    }
}
