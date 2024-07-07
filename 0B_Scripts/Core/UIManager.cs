using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class UIManager : MonoSingleton<UIManager>
{
    [SerializeField] private Transform _canvasTrm;

    [Header("Parent UI")]
    [SerializeField] private GameObject _playUI;
    [SerializeField] private GameObject _talkUI;

    [Space]


    [Header("Conversation")]
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _chatText;

    [Space]

    [SerializeField] private SkillIllustratedBook _skillIllubookPrefab;
    private SkillIllustratedBook _skillIllubook;

    [Space]

    [SerializeField] private StatWindow _statWindowPrefab;
    private StatWindow _statWindow;

    [Header("Current Skill")]
    [SerializeField] private Image _currentSkillIcon;
    [SerializeField] private TextMeshProUGUI _currentSkillName;
    [SerializeField] private Sprite _noneSkill;

    [Space]

    [SerializeField] private GameObject _minimapExp;

    [SerializeField] private Transform _interactBox;
    [SerializeField] private GameObject _clearPanel;

    [SerializeField] private GameObject _deadScreen;

    [Header("Input Setting")]
    [SerializeField] private InputReader _inputReader;

    private void OnEnable() {
        _inputReader.TabAction += MinimapHandle;
    }

    private void OnDisable() {
        _inputReader.TabAction -= MinimapHandle;
    }

    private void Update() {
        if(Keyboard.current.kKey.wasPressedThisFrame) {
            ToggleSkillIlluBook();
        }
        if(Keyboard.current.lKey.wasPressedThisFrame) {
            ToggleStatWindow();
        }
    }

    public void TogglePlayAndTalk() {
        _playUI.SetActive(_talkUI.activeSelf);
        _talkUI.SetActive(!_playUI.activeSelf);
    }

    private void ToggleSkillIlluBook() {
        if(_skillIllubook == null)
            _skillIllubook = Instantiate(_skillIllubookPrefab, _canvasTrm);
        else
            _skillIllubook.gameObject.SetActive(!_skillIllubook.gameObject.activeSelf);
    }

    private void ToggleStatWindow() {
        if(_statWindow == null)
            _statWindow = Instantiate(_statWindowPrefab, _canvasTrm);
        else
            _statWindow.gameObject.SetActive(!_statWindow.gameObject.activeSelf);
    }

    public void SetCurrentSkill(Sprite icon, string name) {
        if(icon == null) _currentSkillIcon.sprite = _noneSkill;
        else _currentSkillIcon.sprite = icon;
        _currentSkillName.text = name;
    }

    private void MinimapHandle() {
        _minimapExp.SetActive(!_minimapExp.activeSelf);
    }

    public void ShowChatBox(bool flag) {
        _interactBox.gameObject.SetActive(flag);
    }

    public void MoveChatBox(Vector2 pos) {
        _interactBox.position = pos;
    }

    public void ChangeConversationContent(string name, string content) {
        _nameText.text = name;
        _chatText.text = content;
    }

    public void SetConversationFunc(Action action) {
        _inputReader.InteractAction += action;
        _inputReader.CompletionAction += action;
    }

    public void RemoveConversationFunc(Action action) {
        _inputReader.InteractAction -= action;
        _inputReader.CompletionAction -= action;
    }

    public void SetClear() {
        _clearPanel.SetActive(true);
    }

    public void ShowDead() {
        _deadScreen.SetActive(true);
    }
}
