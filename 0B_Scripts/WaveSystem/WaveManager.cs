using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ObjectPooling;
using Random = UnityEngine.Random;

public class WaveManager : MonoSingleton<WaveManager>
{
    public Player player;

    public WaveSO[] Waves;
    public int CurrentEnemyCount;

    [SerializeField] private BattleZone _battleZone;

    Vector3 _spawnPos;

    public void StartWaves(WaveSO[] waves, WaveCrystal nextCrystal, WaveCrystal _this, Vector3 spawnPos)
    {
        _spawnPos = spawnPos;
        if (nextCrystal == null) Debug.Log("���� ���̺갡 �����ϴ�.");
        StartCoroutine(StartWave(waves, nextCrystal, _this));
    }

    private IEnumerator StartWave(WaveSO[] waves, WaveCrystal nextCrystal, WaveCrystal _this)
    {
        Waves = waves;
        yield return StartCoroutine(WaveRoutine());
        if(nextCrystal != null)
            nextCrystal.gameObject.SetActive(true);
        _this.crystalProgressImage.SetActive(true);
        MapManager.Instance.EndBattleZone();
    }

    private IEnumerator WaveRoutine()
    {
        int waveIdx = 0;
        while (waveIdx < Waves.Length)
        {
            print($"CurrentWave : {waveIdx}");
            yield return SpawnEnemyRoutine(Waves[waveIdx]);
            yield return new WaitUntil(CanSpawnEnemy);
            waveIdx++;
        }

    }
    private IEnumerator SpawnEnemyRoutine(WaveSO currentWave)
    {
        Enemy[] enemies = currentWave.GetRandomEnemies();
        CurrentEnemyCount = currentWave.MaxCommonCount + currentWave.MaxEliteCount + CurrentEnemyCount;
        foreach(Enemy enemy in enemies)
        {
            IPoolable obj = PoolManager.Instance.Pop(enemy.PoolingType);
            Vector3 playerPos = player.transform.position;
            float x = Random.Range(_spawnPos.x - 10, _spawnPos.x + 10);
            float z = Random.Range(playerPos.z - 10, playerPos.z + 10);
            obj.GameObject.transform.position = new Vector3(x, 0, z);
            yield return null;
        }
    }

    private bool CanSpawnEnemy() => CurrentEnemyCount < 5;
}
