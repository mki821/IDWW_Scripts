using System;
using System.Text;
using System.IO;
using UnityEngine;
using UnityEditor;
using Object = UnityEngine.Object;

public class SkillWindow : EditorWindow
{
    private static Vector2 _viewScrollPosition = Vector2.zero;
    private static Vector2 _scrollPosition;
    private static Object _selectedItem;

    private readonly string _directory = "Assets/0I_SO/Skill";
    private SkillListSO _skillList = null;

    private Editor _cachedEditor;
    private Texture2D _selectedBoxTexture;
    private GUIStyle _selectedBoxStyle;

    private void OnEnable() {
        SetUpWindow();
    }

    private void OnDisable() {
        DestroyImmediate(_cachedEditor);
        DestroyImmediate(_selectedBoxTexture);
    }

    private void SetUpWindow() {
        _selectedBoxTexture = new Texture2D(1, 1);
        _selectedBoxTexture.SetPixel(0, 0, new Color(0.31f, 0.40f, 0.50f));
        _selectedBoxTexture.Apply();

        _selectedBoxStyle = new GUIStyle();
        _selectedBoxStyle.normal.background = _selectedBoxTexture;

        _selectedBoxTexture.hideFlags = HideFlags.DontSave;

        if(_skillList == null) {
            _skillList = AssetDatabase.LoadAssetAtPath<SkillListSO>($"{_directory}/table.asset");

            if(_skillList == null) {
                _skillList = CreateInstance<SkillListSO>();

                string fileName = AssetDatabase.GenerateUniqueAssetPath($"{_directory}/table.asset");
                AssetDatabase.CreateAsset(_skillList, fileName);
                Debug.Log($"SpellList Data Generated at {fileName}!");
            }
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    [MenuItem("Tools/Skill")]
    private static void OpenWindow() {
        SkillWindow window = GetWindow<SkillWindow>("Skill Utility");
        window.minSize = new Vector3(700, 500);
        window.Show();
    }

    private void OnGUI() {
        DrawSkillMenu();
    }

    private void DrawSkillMenu() {
        EditorGUILayout.BeginHorizontal(); {
            GUI.color = new Color(0.19f, 0.76f, 0.08f);
            if(GUILayout.Button("New Skill")) {
                Guid guid = Guid.NewGuid();
                SkillSO newData = CreateInstance<SkillSO>();

                newData.code = guid.ToString();

                AssetDatabase.CreateAsset(newData, $"{_directory}/Skill_{newData.code}.asset");
                _skillList.list.Add(newData);

                EditorUtility.SetDirty(_skillList);
                AssetDatabase.SaveAssets();
            }

            GUI.color = new Color(0.81f, 0.13f, 0.18f);
            if(GUILayout.Button("Generate Enum File")) {
                GeneratorPlayerSkillEnumFile();
            }
        }
        EditorGUILayout.EndHorizontal();

        GUI.color = Color.white;

        EditorGUILayout.BeginHorizontal(); {
            EditorGUILayout.BeginVertical(EditorStyles.helpBox, GUILayout.Width(300)); {
                EditorGUILayout.LabelField("Skill List");
                EditorGUILayout.Space(2f);

                _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition, false, true, GUIStyle.none, GUI.skin.verticalScrollbar, GUIStyle.none); {
                    foreach(SkillSO so in _skillList.list) {
                        GUIStyle style = _selectedItem == so ? _selectedBoxStyle : GUIStyle.none;

                        EditorGUILayout.BeginHorizontal(style); {
                            EditorGUILayout.LabelField(so.code, GUILayout.Width(200f), GUILayout.Height(40f));

                            EditorGUILayout.BeginVertical(); {
                                EditorGUILayout.Space(10f);
                                GUI.color = Color.red;
                                if(GUILayout.Button("X", GUILayout.Width(20f))) {
                                    _skillList.list.Remove(so);
                                    AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(so));
                                    EditorUtility.SetDirty(_skillList);
                                    AssetDatabase.SaveAssets();
                                }
                                GUI.color = Color.white;
                            }
                            EditorGUILayout.EndVertical();
                        }
                        EditorGUILayout.EndHorizontal();

                        if(so == null) break;

                        Rect lastRect = GUILayoutUtility.GetLastRect();

                        if(Event.current.type == EventType.MouseDown && lastRect.Contains(Event.current.mousePosition)) {
                            _selectedItem = so;
                            Event.current.Use();
                        }
                    }
                }
                EditorGUILayout.EndScrollView();
            }
            EditorGUILayout.EndVertical();

            if(_selectedItem != null) {
                _viewScrollPosition = EditorGUILayout.BeginScrollView(_viewScrollPosition); {
                    EditorGUILayout.Space(2f);
                    Editor.CreateCachedEditor(_selectedItem, null, ref _cachedEditor);
                    _cachedEditor.OnInspectorGUI();
                }
                EditorGUILayout.EndScrollView();
            }
        }
        EditorGUILayout.EndHorizontal();
    }

    private void GeneratorPlayerSkillEnumFile() {
        StringBuilder codeBuilder = new StringBuilder();
        
        foreach(SkillSO s in SkillManager.Instance.SkillList) {
            codeBuilder.Append(s.code);
            codeBuilder.Append(", ");
        }

        string code = string.Format(CodeFormat.PlayerSkillFormat, codeBuilder.ToString());
        string path = $"{Application.dataPath}/0B_Scripts/SkillSystem/PlayerSkill.cs";

        File.WriteAllText(path, code);
        AssetDatabase.Refresh();
    }
}
