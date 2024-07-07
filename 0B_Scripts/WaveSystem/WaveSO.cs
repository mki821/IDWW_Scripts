using Microsoft.Win32.SafeHandles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="SO/Wave")]
public class WaveSO : ScriptableObject
{
    #region Common Enemies Value
    [Header("�Ϲ� �� ���� ��, �� ���� Weights�� ���ƾ� ��")]
    public List<Enemy> CommonEnemies = new();
    public int[] CommonWeights;
    public int MaxCommonCount = 0;
    #endregion

    #region Elite Enemies Value
    [Header("����Ʈ �� ���� ��, �� ���� Weights�� ���ƾ� ��")]
    public List<Enemy> EliteEnemies= new();
    public int[] EliteWeights;
    public int MaxEliteCount = 0;
    #endregion

    public Enemy[] GetRandomEnemies()
    {
        List<Enemy> enemies = new();

        int spawnIdx = GetCommonEnemyIndex();
        int cnt = 0;
        while (cnt < MaxCommonCount)
        {
            enemies.Add(CommonEnemies[spawnIdx]);
            spawnIdx = GetCommonEnemyIndex();
            cnt++;
        }

        if (EliteEnemies.Count == 0) 
            return enemies.ToArray();
        else
        {
            cnt = 0;
            spawnIdx = GetEliteEnemyIndex();
            while (cnt < MaxEliteCount)
            {
                enemies.Add(EliteEnemies[spawnIdx]);
                spawnIdx = GetEliteEnemyIndex();
                cnt++;
            }
            return enemies.ToArray();
        }
    }

    private int GetCommonWeights()
    {
        int total = 0;
        for(int i = 0; i < CommonWeights.Length; i++)
        {
            total += CommonWeights[i];
        }
        return total;
    }

    private int GetCommonEnemyIndex()
    {
        int chance = Random.Range(0, GetCommonWeights());
        for (int i = 0; i < CommonWeights.Length; ++i)
        {
            chance -= CommonWeights[i];
            if (chance < 0)
            {
                return i;
            }
        }
        return 0;
    }

    private int GetEliteWeights()
    {
        int total = 0;
        for (int i = 0; i < EliteWeights.Length; i++)
        {
            total += EliteWeights[i];
        }
        return total;
    }

    private int GetEliteEnemyIndex()
    {
        int chance = Random.Range(0, GetEliteWeights());
        for (int i = 0; i < EliteWeights.Length; ++i)
        {
            chance -= CommonWeights[i];
            if (chance < 0)
            {
                return i;
            }
        }
        return 0;
    }
}
