using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class Casting : MonoBehaviour
{
    [Header("Magic Circle Setting")]
    [SerializeField] private MagicCircleSO[] _magicCircles;
    [SerializeField] private MeshRenderer _magicCirclePrefab;
    [SerializeField] private Transform _magicCircleTrm;
    private List<GameObject> _magicCircleList = new List<GameObject>();

    [Header("Skill Setting")]
    [SerializeField] private int _maxCount = 10;

    [SerializeField] private Transform _pressedKey;
    [SerializeField] private GameObject[] _spellIcons;

    [Header("Input Setting")]
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private LayerMask _whatIsGround;
    
    public bool castable = true;

    private int _currentCount = 0;
    private bool _isCasting = false;
    private StringBuilder _casted = new StringBuilder();
    private Player _player;

    private readonly int _texHash = Shader.PropertyToID("_MainTex");
    private readonly int _colorHash = Shader.PropertyToID("_Color");
    private readonly int _mulHash = Shader.PropertyToID("_Multiply");

    private void Awake() {
        _player = GetComponent<Player>();
    }

    private void OnEnable() {
        _inputReader.QAction += Append;
        _inputReader.WAction += Append;
        _inputReader.EAction += Append;
        _inputReader.RAction += Append;

        _inputReader.CompletionAction += Completion;
    }

    private void OnDisable() {
        _inputReader.QAction -= Append;
        _inputReader.WAction -= Append;
        _inputReader.EAction -= Append;
        _inputReader.RAction -= Append;

        _inputReader.CompletionAction -= Completion;
    }

    private void Append(char ch) {
        if(_player.isDead || !castable) return;

        if(!_isCasting) {
            _isCasting = true;
            CameraManager.Instance.ScreenToWorld(_inputReader.AimPosition, out PlayerManager.Instance.CastingStartPosition);
            transform.rotation = Quaternion.LookRotation(GetMouseVector());
            _player.StateMachine.ChangeState(PlayerStateEnum.Cast);
        }
        
        if(_currentCount >= _maxCount) return;

        _casted.Append(ch);

        ++_currentCount;

        int selectSlot = 0;
        switch(ch) {
            case 'W':
                selectSlot = 1;
                break;
            case 'E':
                selectSlot = 2;
                break;
            case 'R':
                selectSlot = 3;
                break;
        }

        CheckCurrentSkill();

        Instantiate(_spellIcons[selectSlot], _pressedKey);

        SoundManager.Instance.PlaySFX("makeMagicCircle");
        
        MagicCircleSO magicCircle = _magicCircles[selectSlot];

        Vector3 direction = new Vector3(0f, 0.3f, 1f).normalized;
        Quaternion rotation = Quaternion.LookRotation(direction);
        
        MeshRenderer obj = Instantiate(_magicCirclePrefab, transform.position, rotation, _magicCircleTrm);
        obj.transform.localScale = Vector3.one * _currentCount * 1.3f;
        obj.transform.localPosition += direction * (0.7f + 0.3f * _currentCount) + Vector3.up * 1.5f;
        obj.material.SetTexture(_texHash, magicCircle.texture);
        obj.material.SetColor(_colorHash, magicCircle.color);
        obj.material.SetInt(_mulHash, magicCircle.multiply ? 1 : 0);

        MagicCircle mc = obj.GetComponent<MagicCircle>();
        mc.angularSpeed = 20 * _currentCount;
        mc.isRight = _currentCount % 2 == 0;

        _magicCircleList.Add(obj.gameObject);

        _player.stamina.CurrentStamina -= 1;
    }

    private Vector3 GetMouseVector() {
        Vector3 direction = PlayerManager.Instance.CastingStartPosition - transform.position;
        direction.y = 0f;
        return direction;
    }

    private void CheckCurrentSkill() {
        if(SkillManager.Instance.ContainsSkill(_casted.ToString(), out PlayerSkill type)) {
            SkillSO so = SkillManager.Instance.GetSkillSO(type);
            UIManager.Instance.SetCurrentSkill(so.icon, so.skillName);
        }
        else 
            UIManager.Instance.SetCurrentSkill(null, "");
    }

    public void Completion() {
        if(_player.isDead || !_isCasting || !castable) return;
        
        if(SkillManager.Instance.ContainsSkill(_casted.ToString(), out PlayerSkill type)) {
            SkillManager.Instance.GetSkill(type).UseSkill();
                
            GotoIdle();
        }
        else {
            Boom();
        }
    }

    private void Boom() {
        SoundManager.Instance.PlaySFX("castingFail");
        GotoIdle();
    }

    private void GotoIdle() {
        _currentCount = 0;
        _magicCircleList.ForEach(m => Destroy(m));
        _casted.Clear();

        int count = _pressedKey.childCount;
        for(int i = 0; i < count; ++i) {
            Destroy(_pressedKey.GetChild(i).gameObject);
        }

        if(!_player.isDead)
            _player.StateMachine.ChangeState(PlayerStateEnum.Idle);
        
        _isCasting = false;
    }
}
