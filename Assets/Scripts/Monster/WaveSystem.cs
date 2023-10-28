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

    private bool isStarted = false;
    public void StartWave()
    {
        if (isStarted == false)
        {
            int currentWaveIndex = GameManager.gameManager.GetCurrentWave() - 1; // ���̺� �ܰ�
            monsterSpawner.StartWave(waves[currentWaveIndex]);
            isStarted = true;
        }
        
    }
    // ���� �������� �����ϸ� �۵��ϵ��� �ؾ���
    public void ClearWave()
    {
        isStarted = false;
    }
}



[System.Serializable]
public struct Wave
{
    public float spawnTime;                    // ���̺� �� ���� �ֱ�
    public int maxMonsterCount;                // ���̺� �� ���� ����
    public GameObject[] monsterPrefabs;        // �� ���� ����
}