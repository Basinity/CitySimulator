using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public enum GeneratorToolTabs
{
    GeneralSettings,
    TileSettings
}

public class GeneratorWindow : EditorWindow
{
    private int selectedTabIndex;
    private int generatorSettingsIndex;
    private int tileSettingsIndex;

    private static List<GeneratorSettings> generatorSettingsList;
    private static List<TileSettings> tileSettingsList;
    
    private static EditorWindow window;
    private Rect upRect;
    private Rect leftRect;
    private Rect rightRect;
    private Vector2 leftRectScrollPosition;
    private Vector2 rightRectScrollPosition;

    [MenuItem("Tools/City Generator")]
    private static void ShowWindow()
    {
        window = GetWindow<GeneratorWindow>();

        GetAllGeneratorSettings();
        GetAllTileSettings();
    }

    private void OnGUI()
    {
        GUILayout.BeginArea(upRect);
        selectedTabIndex = GUILayout.Toolbar(selectedTabIndex, new[] { "General Settings", "Tile Settings" });
        GUILayout.EndArea();
        
        RecalculateRects();
        switch (selectedTabIndex)
        {
            case (int)GeneratorToolTabs.GeneralSettings:
                DrawGeneralSettings();
                break;
            case (int)GeneratorToolTabs.TileSettings:
                DrawTileSettings();
                break;
        }
    }

    private void DrawGeneralSettings()
    {
        GUILayout.BeginArea(leftRect);
        EditorGUILayout.BeginVertical();
        EditorGUILayout.Space(5f);
        leftRectScrollPosition = EditorGUILayout.BeginScrollView(leftRectScrollPosition);
        for (var i = 0; i < generatorSettingsList.Count; i++)
        {
            if (GUILayout.Button(generatorSettingsList[i].name))
            {
                generatorSettingsIndex = i;
            }
        }

        EditorGUILayout.EndScrollView();
        EditorGUILayout.Space(5f);
        EditorGUILayout.EndVertical();
        GUILayout.EndArea();
        GUILayout.BeginArea(rightRect);
        
        rightRectScrollPosition = EditorGUILayout.BeginScrollView(rightRectScrollPosition);
        var serializedObject = new SerializedObject(generatorSettingsList[generatorSettingsIndex]);
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("generatorSettingName"), new GUIContent("Name"));
        if (GUILayout.Button("Rename")) GetAllGeneratorSettings();
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("width"), new GUIContent("Width"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("height"), new GUIContent("Height"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("buildingTile"), new GUIContent("Building Spawn Tile"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("tiles"), new GUIContent("Tiles"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("buildings"), new GUIContent("Buildings"));
        serializedObject.ApplyModifiedProperties();

        EditorGUILayout.Space(20);
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("New")) NewGeneratorSettings();
        if (GUILayout.Button("Delete")) DeleteGeneratorSettings();
        if (GUILayout.Button("Apply")) ApplyGeneratorSettings();
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndScrollView();
        GUILayout.EndArea();
    }

    private void DrawTileSettings()
    {
        GUILayout.BeginArea(leftRect);
        EditorGUILayout.BeginVertical();
        EditorGUILayout.Space(5f);
        leftRectScrollPosition = EditorGUILayout.BeginScrollView(leftRectScrollPosition);
        for (var i = 0; i < tileSettingsList.Count; i++)
        {
            if (GUILayout.Button(tileSettingsList[i].name))
            {
                tileSettingsIndex = i;
            }
        }

        EditorGUILayout.EndScrollView();
        EditorGUILayout.Space(5f);
        EditorGUILayout.EndVertical();
        GUILayout.EndArea();
        GUILayout.BeginArea(rightRect);

        rightRectScrollPosition = EditorGUILayout.BeginScrollView(rightRectScrollPosition);
        var serializedObject = new SerializedObject(tileSettingsList[tileSettingsIndex]);
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("tileName"), new GUIContent("Name"));
        if (GUILayout.Button("Rename")) GetAllTileSettings();
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("tile"), new GUIContent("Tile"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("upNeighbours"), new GUIContent("Up Neighbours"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("downNeighbours"), new GUIContent("Down Neighbours"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("leftNeighbours"), new GUIContent("Left Neighbours"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("rightNeighbours"), new GUIContent("Right Neighbours"));
        serializedObject.ApplyModifiedProperties();

        EditorGUILayout.Space(20);
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("New")) NewTileSettings();
        if (GUILayout.Button("Delete")) DeleteTileSettings();
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndScrollView();
        GUILayout.EndArea();
    }

    private static void GetAllGeneratorSettings()
    {
        var generatorIDs = AssetDatabase.FindAssets("t:" + nameof(GeneratorSettings), new[] {"Assets/ScriptableObjects"});
        generatorSettingsList = new List<GeneratorSettings>();

        for (var i = 0; i < generatorIDs.Length; i++)
        {
            var path = AssetDatabase.GUIDToAssetPath(generatorIDs[i]);
            generatorSettingsList.Add(AssetDatabase.LoadAssetAtPath<GeneratorSettings>(path));
            AssetDatabase.RenameAsset(path, generatorSettingsList[i].generatorSettingName);
        }
    }

    private static void GetAllTileSettings()
    {
        var tileIDs = AssetDatabase.FindAssets("t:" + nameof(TileSettings));
        tileSettingsList = new List<TileSettings>();

        for (var i = 0; i < tileIDs.Length; i++)
        {
            var path = AssetDatabase.GUIDToAssetPath(tileIDs[i]);
            tileSettingsList.Add(AssetDatabase.LoadAssetAtPath<TileSettings>(path));
            AssetDatabase.RenameAsset(path, tileSettingsList[i].tileName);
        }
    }

    private void NewGeneratorSettings()
    {
        var generatorSettings = CreateInstance<GeneratorSettings>();
        AssetDatabase.CreateAsset(generatorSettings, "Assets/ScriptableObjects/GeneratorSettings/NewSetting.asset");
        generatorSettingsList.Add(generatorSettings);
        generatorSettingsIndex = generatorSettingsList.Count - 1;
    }

    private void DeleteGeneratorSettings()
    {
        AssetDatabase.DeleteAsset($"Assets/ScriptableObjects/GeneratorSettings/{generatorSettingsList[generatorSettingsIndex].name}.asset");
        generatorSettingsList.RemoveAt(generatorSettingsIndex);
        generatorSettingsIndex = generatorSettingsList.Count - 1;
    }

    private void ApplyGeneratorSettings()
    {
        AssetDatabase.CopyAsset(
            $"Assets/ScriptableObjects/GeneratorSettings/{generatorSettingsList[generatorSettingsIndex].name}.asset",
            "Assets/Scripts/Generation/GeneratorSettings.asset");
    }

    private void NewTileSettings()
    {
        var tileSettings = CreateInstance<TileSettings>();
        AssetDatabase.CreateAsset(tileSettings, "Assets/ScriptableObjects/TileSettings/NewSetting.asset");
        tileSettingsList.Add(tileSettings);
        tileSettingsIndex = tileSettingsList.Count - 1;
    }

    private void DeleteTileSettings()
    {
        AssetDatabase.DeleteAsset($"Assets/ScriptableObjects/TileSettings/{tileSettingsList[tileSettingsIndex].name}.asset");
        tileSettingsList.RemoveAt(tileSettingsIndex);
        tileSettingsIndex = tileSettingsList.Count - 1;
    }

    private void RecalculateRects()
    {
        leftRect.size = new Vector2(window.position.width * 0.2f, window.position.height * 0.95f);
        rightRect.size = new Vector2(window.position.width * 0.8f, window.position.height * 0.95f);
        upRect.size = new Vector2(window.position.width, window.position.height * 0.05f);

        rightRect.x = leftRect.width;
        rightRect.y = upRect.height;
        leftRect.y = upRect.height;
    }
}