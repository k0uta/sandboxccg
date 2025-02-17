﻿#if UNITY_EDITOR
using AutoCCG;
using Mobcast.CoffeeEditor.SubAssetEditor;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

[CanEditMultipleObjects]
[CustomEditor(typeof(CardSkillModel), true)]
public class SkillEditor : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(serializedObject.FindProperty("title"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("description"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("sprite"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("color"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("phase"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("actionPriority"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("actionType"));

        var manaCost = serializedObject.FindProperty("manaCost");
        if (manaCost != null)
        {
            EditorGUILayout.PropertyField(manaCost);
        }

        var count = serializedObject.FindProperty("count");
        if (count != null)
        {
            EditorGUILayout.PropertyField(count);
        }

        CreateDynamicPropertyList<CardConditionModel>("conditions", "Condition");

        CreateDynamicPropertyList<CardEffectModel>("effects", "Effect");

        serializedObject.ApplyModifiedProperties();
    }

    void CreateDynamicPropertyList<T>(string propertyName, string propertyLabel)
    {
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

        var property = serializedObject.FindProperty(propertyName);

        EditorGUILayout.PropertyField(property);

        if (property.isExpanded)
        {

            EditorGUI.indentLevel++;

            for (int i = 0; i < property.arraySize; i++)
            {
                var subProperty = property.GetArrayElementAtIndex(i);
                var subPropertyName = subProperty.objectReferenceValue ? ObjectNames.NicifyVariableName(subProperty.objectReferenceValue.GetType().Name) : "";

                EditorGUILayout.PropertyField(subProperty, new GUIContent(subPropertyName), true);

                if (GUILayout.Button("Remove", EditorStyles.miniButton))
                {
                    RemoveFromProperty(property, i);
                }

                GUILayout.Space(10);
            }

            EditorGUI.indentLevel--;

        }

        GUILayout.Space(10);

        var availableOptions = GetScriptAssetsOfType<T>();
        availableOptions.Insert(0, string.Format("Select {0} to Add", propertyLabel));

        // TODO: Check this out http://trusteddevelopments.com/2014/10/13/custom-popups-matching-unity-style/
        GUIStyle popupStyle = new GUIStyle(EditorStyles.popup);
        popupStyle.fontSize = 12;
        popupStyle.fixedHeight = 20;
        int optionIndex = EditorGUILayout.Popup(0, availableOptions.ToArray(), popupStyle);

        if (optionIndex != 0)
        {
            AddClassToProperty(property, availableOptions[optionIndex], propertyLabel);
        }

        GUILayout.Space(10);
    }

    void RemoveFromProperty(SerializedProperty property, int index)
    {
        EditorUtility.SetDirty(serializedObject.targetObject);

        var target = property.GetArrayElementAtIndex(index).objectReferenceValue;
        var targetPath = AssetDatabase.GetAssetPath(target);

        if (targetPath == AssetDatabase.GetAssetPath(serializedObject.targetObject))
        {
            DestroyImmediate(target, true);
            AssetDatabase.SaveAssets();
        }

        for (int i = index + 1; i < property.arraySize; i++)
        {
            property.GetArrayElementAtIndex(i - 1).objectReferenceValue = property.GetArrayElementAtIndex(i).objectReferenceValue;
        }
        property.arraySize--;

        ForceSubAssetEditorReload();
    }

    void AddClassToProperty(SerializedProperty property, string className, string prefix)
    {
        EditorUtility.SetDirty(serializedObject.targetObject);

        var classType = GetClassType(className);

        var newIndex = property.arraySize;
        property.InsertArrayElementAtIndex(newIndex);
        var newEntry = property.GetArrayElementAtIndex(newIndex);

        var newClass = Activator.CreateInstance(classType) as UnityEngine.Object;
        var newClassName = ObjectNames.NicifyVariableName(classType.Name);
        newClass.name = string.Format("({0}) {1} ", prefix, newClassName);
        AssetDatabase.AddObjectToAsset(newClass, AssetDatabase.GetAssetPath(serializedObject.targetObject));
        AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(newClass));

        newEntry.objectReferenceValue = newClass;

        property.isExpanded = true;
        newEntry.isExpanded = true;

        ForceSubAssetEditorReload();
    }

    public static List<string> GetScriptAssetsOfType<T>()
    {
        MonoScript[] scripts = (MonoScript[])Resources.FindObjectsOfTypeAll(typeof(MonoScript));

        List<string> result = new List<string>();

        foreach (MonoScript m in scripts)
        {
            if (m.GetClass() != null && m.GetClass().IsSubclassOf(typeof(T)))
            {
                result.Add(m.GetClass().ToString());
            }
        }
        return result;
    }

    [MenuItem("Assets/Delete Sub-Object")]
    public static void RemoveSubObject()
    {
        var target = Selection.activeObject;
        if (!AssetDatabase.IsMainAsset(target))
        {
            DestroyImmediate(target, true);
            AssetDatabase.SaveAssets();
        }
        else
        {
            throw new InvalidOperationException();
        }
    }

    [MenuItem("Assets/Delete Sub-Object", true)]
    public static bool DeleteSubObjectValidation()
    {
        return !AssetDatabase.IsMainAsset(Selection.activeObject);
    }

    public static Type GetClassType(string typeName)
    {
        var type = Type.GetType(typeName);
        if (type != null) return type;
        foreach (var a in AppDomain.CurrentDomain.GetAssemblies())
        {
            type = a.GetType(typeName);
            if (type != null)
                return type;
        }
        return null;
    }

    [MenuItem("Assets/Create/AutoCCG/Skill")]
    public static void CreateSkillObject()
    {
        CreateSkillAsset<CardSkillModel>("Skill");
    }

    [MenuItem("Assets/Create/AutoCCG/Mana Skill")]
    public static void CreateManaSkillObject()
    {
        CreateSkillAsset<CardManaSkillModel>("Mana Skill");
    }

    static void CreateSkillAsset<T>(string objectName)
    {
        var selectionObject = Selection.activeObject;
        var selectionCard = selectionObject as CardModel;

        var newObject = Activator.CreateInstance(typeof(T)) as UnityEngine.Object;
        newObject.name = objectName;


        if (selectionCard != null)
        {
            AssetDatabase.AddObjectToAsset(newObject, AssetDatabase.GetAssetPath(selectionCard));
            AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(newObject));
        }
        else
        {
            var assetPath = AssetDatabase.GetAssetPath(selectionObject);

            if (File.Exists(assetPath))
            {
                assetPath = Path.GetDirectoryName(assetPath);
            }

            assetPath = Path.Combine(assetPath, string.Format("{0}.asset", objectName));
            ProjectWindowUtil.CreateAsset(newObject, assetPath);
        }
        AssetDatabase.Refresh();
        ForceSubAssetEditorReload();
    }

    static void ForceSubAssetEditorReload()
    {
        var subAssetEditors = Resources.FindObjectsOfTypeAll<SubAssetEditor>();
        foreach (var subAssetEditor in subAssetEditors)
        {
            subAssetEditor.ForceReload();
        }
    }
}
#endif