using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSystem : MonoBehaviour
{
    [SerializeField]
    private Wave[] waves;
    [SerializeField]
    private MonsterSpawner monsterSpawner;
    [SerializeField]
    private int currentWaveIndex; // ���̺� �ܰ�


    public void StartWave()
    {
        monsterSpawner.StartWave(waves[currentWaveIndex]);
    }
}


[System.Serializable]
public struct Wave
{
    public float spawnTime;                    // ���̺� �� ���� �ֱ�
    public int maxMonsterCount;                // ���̺� �� ���� ����
    public GameObject[] monsterPrefabs;        // �� ���� ����
}