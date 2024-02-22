using UnityEngine;
using UnityEditor;

public class LevelEditor : EditorWindow {

    protected SerializedObject serializedObject;
    protected SerializedProperty serializedProperty;
    protected SerializedProperty LevelNumber;
    protected SerializedProperty TileDatas;

    protected LevelData[] levels;

    protected string selectedPropertyPatch;
    protected string selectedProperty;

    [MenuItem("Picker3D Demo/Level Editor")]
    private static void ShowWindow() {
        var window = GetWindow<LevelEditor>();
        window.titleContent = new GUIContent("Level Editor");
        window.Show();
    }

    private void OnGUI() {

        levels = GetAllInstances<LevelData>();
        serializedObject = new SerializedObject(levels[0]);
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.BeginVertical("box", GUILayout.MaxWidth(150),GUILayout.ExpandHeight(true));
        DrawSliderBar(levels);
        EditorGUILayout.EndVertical();
        EditorGUILayout.BeginVertical("box" , GUILayout.ExpandHeight(true));

        if (selectedProperty!=null)
        {
            for (int i = 0; i < levels.Length; i++)
            {
                if (levels[i].levelNumber.ToString()==selectedProperty)
                {
                    serializedObject = new SerializedObject(levels[i]);
                    LevelNumber = serializedObject.FindProperty("levelNumber");
                    TileDatas = serializedObject.FindProperty("tileDatas");
                    DrawProperties();
                }
            }
        }
        else
        {
            EditorGUILayout.LabelField("Select an item from the list");
        }
        EditorGUILayout.EndVertical();
        EditorGUILayout.EndHorizontal();

        Apply();

    }

    protected void DrawSliderBar(LevelData[] allLevels)
    {
        foreach (LevelData level in allLevels)
        {
            if (GUILayout.Button("Level " + level.levelNumber))
            {
                selectedPropertyPatch =level.levelNumber.ToString();
            }
        }

        if (!string.IsNullOrEmpty(selectedPropertyPatch))
        {
            selectedProperty = selectedPropertyPatch;
        }

        if (GUILayout.Button("Create Level"))
        {
            LevelData newLevel = ScriptableObject.CreateInstance<LevelData>();
            LevelCreator levelCreator = GetWindow<LevelCreator>("New Level");
            levelCreator.newLevel = newLevel;
        }
    }

    protected void DrawProperties()
    {
        
        EditorGUILayout.LabelField("Level Number",EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(LevelNumber);
        if (LevelNumber.intValue<=0)
        {
            EditorGUILayout.HelpBox("Please enter a valid Level number",MessageType.Warning);
        }
        EditorGUILayout.PropertyField(TileDatas);
        

        

        if (GUILayout.Button("Delete Level"))
        {
            AssetDatabase.DeleteAsset("Assets/Resources/Levels/Level" + selectedProperty + ".asset");      

            serializedObject=null;      
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
        if (serializedObject!=null)
        {
            serializedObject.ApplyModifiedProperties();
        }  
        
    }
}
