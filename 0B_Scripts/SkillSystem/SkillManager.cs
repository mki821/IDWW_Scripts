using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillIcon {
    public Image cooltime;
    public float currentTime;
    public float maxTime;
}

public class SkillManager : MonoSingleton<SkillManager>
{

    [SerializeField] private SkillListSO _skillList;

    #region Show Cooltime

    [SerializeField] private Image _skillIcon;
    [HideInInspector] public Transform skillCooltime;
    private List<SkillIcon> _usedSkillList = new List<SkillIcon>();

    #endregion

    private Dictionary<Type, Skill> _skills;
    private Dictionary<string, PlayerSkill> _skillDictionary;

    public List<SkillSO> SkillList => _skillList.list;

    protected override void Awake() {
        _skills = new Dictionary<Type, Skill>();
        _skillDictionary = new Dictionary<string, PlayerSkill>();
        
        SoundManager.Instance.PlayBGM("TheArk");

        foreach(PlayerSkill skillEnum in Enum.GetValues(typeof(PlayerSkill))) {
            if(skillEnum == PlayerSkill.None) continue;

            Skill skillCompo = GetComponent($"{skillEnum.ToString()}Skill") as Skill;
            Type type = skillCompo.GetType();
            _skills.Add(type, skillCompo);
        }
        
        foreach(var m in SkillList) {
            _skillDictionary.Add(m.command, m.skillType);
        }
    }

    private void Update() {
        UpdateSkillCooltime();
    }

    public T GetSkill<T>() where T : Skill {
        Type t = typeof(T);

        if(_skills.TryGetValue(t, out Skill target)) {
            return target as T;
        }

        return null;
    }

    public Skill GetSkill(PlayerSkill skillEnum) {
        Type type = Type.GetType($"{skillEnum.ToString()}Skill");
        if(type == null) return null;

        if(_skills.TryGetValue(type, out Skill target)) {
            return target;
        }
        return null;
    }

    public SkillSO GetSkillSO(PlayerSkill skillEnum) {
        foreach(var s in SkillList) {
            if(s.skillType == skillEnum)
                return s;
        }
        return null;
    }

    public bool ContainsSkill(string command, out PlayerSkill type) {
        return _skillDictionary.TryGetValue(command, out type);
    }

    public void StartSkillCooltime(Sprite icon, float cooltime) {
        Image obj = Instantiate(_skillIcon, skillCooltime);
        obj.sprite = icon;

        SkillIcon skillIcon = new SkillIcon {
            cooltime = obj.transform.GetChild(0).GetComponent<Image>(),
            currentTime = cooltime,
            maxTime = cooltime
        };
        _usedSkillList.Add(skillIcon);
    }

    private void UpdateSkillCooltime() {
        if(_usedSkillList.Count < 1) return;

        for(int i = 0; i < _usedSkillList.Count; ++i) {
            SkillIcon s = _usedSkillList[i];

            if(s == null) continue;

            s.currentTime -= Time.deltaTime;
            s.cooltime.fillAmount = s.currentTime / s.maxTime;

            if(s.currentTime <= 0f) {
                _usedSkillList.Remove(s);
                Destroy(s.cooltime.transform.parent.gameObject);
            }
        }
    }
}
