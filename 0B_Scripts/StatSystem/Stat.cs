using System;
using System.Collections.Generic;
using UnityEngine;

public enum StatType
{
    Damage,
    MaxHealth,
    CriticalChance,
    CriticalDamage,
    Armor,
    Evasion,
    MoveSpeed,
    AttackSpeed
}

[Serializable]
public class Stat
{
    [SerializeField] private int _baseValue;

    public List<float> modifiers;
    public bool _isPercent;

    public float GetValue()
    {
        float finalValue = _baseValue;
        foreach (float value in modifiers)
            finalValue += value;

        return finalValue;
    }

    public void AddModifier(float value)
    {
        if (value != 0)
            modifiers.Add(value);
    }

    public void RemoveModifier(float value)
    {
        if (value != 0)
            modifiers.Remove(value);
    }

    public void SetDefaulValue(int value)
    {
        _baseValue = value;
    }
}
