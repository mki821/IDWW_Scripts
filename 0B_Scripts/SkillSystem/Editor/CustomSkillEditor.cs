using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SkillSO))]
public class CustomSkillEditor : Editor
{
    private SerializedProperty codeProp;
    private SerializedProperty iconProp;
    private SerializedProperty skillNameProp;
    private SerializedProperty commandProp;
    private SerializedProperty skillTypeProp;

    private void OnEnable() {
        GUIUtility.keyboardControl = 0;
        codeProp = serializedObject.FindProperty("code");
        iconProp = serializedObject.FindProperty("icon");
        skillNameProp = serializedObject.FindProperty("skillName");
        commandProp = serializedObject.FindProperty("command");
        skillTypeProp = serializedObject.FindProperty("skillType");
    }

    public override void OnInspectorGUI() {
        serializedObject.Update();

        EditorGUILayout.Space(10f);
        EditorGUILayout.BeginVertical("HelpBox"); {
            EditorGUILayout.BeginHorizontal(); {
                iconProp.objectReferenceValue
                 = EditorGUILayout.ObjectField(GUIContent.none, iconProp.objectReferenceValue, typeof(Sprite), false, GUILayout.Width(65f));

                EditorGUILayout.BeginVertical(); {
                    EditorGUI.BeginChangeCheck();
                    string prevName = codeProp.stringValue;
                    EditorGUILayout.BeginHorizontal(); {
                        EditorGUILayout.LabelField("Code", GUILayout.Width(90f));
                        EditorGUILayout.DelayedTextField(codeProp, GUIContent.none);
                    }
                    EditorGUILayout.EndHorizontal();

                    if(EditorGUI.EndChangeCheck()) {
                        string assetPath = AssetDatabase.GetAssetPath(target);
                        string newName = $"Skill_{codeProp.stringValue}";

                        serializedObject.ApplyModifiedProperties();

                        string msg = AssetDatabase.RenameAsset(assetPath, newName);

                        if(string.IsNullOrEmpty(msg)) {
                            target.name = newName;

                            EditorGUILayout.EndHorizontal();
                            EditorGUILayout.EndVertical();
                            return;
                        }
                        codeProp.stringValue = prevName;
                    }
                    EditorGUILayout.BeginHorizontal(); {
                        EditorGUILayout.LabelField("Name", GUILayout.Width(90f));
                        skillNameProp.stringValue = EditorGUILayout.TextField(skillNameProp.stringValue);
                    }
                    EditorGUILayout.EndHorizontal();
                }
                EditorGUILayout.EndVertical();
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.PropertyField(commandProp);
            EditorGUILayout.PropertyField(skillTypeProp);

        }
        EditorGUILayout.EndVertical();

        serializedObject.ApplyModifiedProperties();
    }
}
