using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(menuName = "SO/Stat/AgentStat")]
public class AgentStat : ScriptableObject
{
    public Stat damage;
    public Stat maxHealth;
    public Stat criticalChance;
    public Stat criticalDamage;
    public Stat armor;
    public Stat evasion;
    public Stat moveSpeed;
    public Stat attackSpeed;

    protected Agent _owner;
    protected Dictionary<StatType, Stat> _statDictionary;

    //public Stat stat;

    public virtual void SetOwner(Agent owner)
    {
        _owner = owner;
    }

    protected virtual void OnEnable()
    {
        _statDictionary = new Dictionary<StatType, Stat>();
        Type agentStatType = typeof(AgentStat);

        foreach (StatType statEnum in Enum.GetValues(typeof(StatType)))
        {
            try
            {
                //ù���� �ҹ��ڷ� ����
                string fieldName = LowerFirstChar(statEnum.ToString());

                FieldInfo statField = agentStatType.GetField(fieldName);
                Stat statInstance = statField.GetValue(this) as Stat;

                _statDictionary.Add(statEnum, statInstance);
            }
            catch (Exception ex)
            {
                Debug.LogError($"There is no stat - {statEnum.ToString()} {ex.Message}");
            }
        }
    }

    private string LowerFirstChar(string input) => char.ToLower(input[0]) + input.Substring(1);

    public virtual void IncreaseStatFor(int modifyValue, float duration, Stat statToModify)
    {
        _owner.StartCoroutine(StatModifyCoroutine(modifyValue, duration, statToModify));
    }

    protected IEnumerator StatModifyCoroutine(int value, float duration, Stat statToModify)
    {
        statToModify.AddModifier(value);
        yield return new WaitForSeconds(duration);
        statToModify.RemoveModifier(value);
    }

    public float GetDamage()
    {
        //min max�� ������ ����
        return damage.GetValue();
    }

    public bool CanEvasion()
    {
        return IsHitPercent(evasion.GetValue());
    }

    public float ArmoredDamage(float incomingDamage)
    {
        return Mathf.Max(1, incomingDamage - armor.GetValue());
    }

    public bool IsCritical(ref float incomingDamage)
    {
        if (IsHitPercent(criticalChance.GetValue()))
        {
            //���� ����
            float percent = GetIntToPercent(criticalDamage.GetValue());
            incomingDamage = Mathf.CeilToInt(incomingDamage * percent);
            return true;
        }
        return false;
    }

    public void AddModifier(StatType type, float value)
    {
        _statDictionary[type].AddModifier(value);
    }

    public void RemoveModifier(StatType type, float value)
    {
        _statDictionary[type].RemoveModifier(value);
    }


    protected bool IsHitPercent(float statValue) => Random.Range(1, 10000) < statValue;
    //100 * 0.0001f = 0.01 == �ۼ�Ʈ��
    protected float GetIntToPercent(float statValue) => statValue * 0.0001f;
}