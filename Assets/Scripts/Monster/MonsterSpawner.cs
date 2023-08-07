using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public GameObject spawner;          // ���� ���� ��ġ
    private int spawnersLength;         // ���� ��� ����
    private Wave currentWave;           // ���� ���̺� �ܰ� ����

    private void Start()
    {
        spawnersLength = spawner.transform.childCount;
    }

    public void StartWave(Wave wave)
    {
        currentWave = wave; // ���̺� ���� ����
        StartCoroutine("SpawnMonster"); // ���̺� ����
    }

    private IEnumerator SpawnMonster()
    {
        int spawnMonsterCount = 0; // ���� ���̺� ���� ���� ����

        while( spawnMonsterCount < currentWave.maxMonsterCount)
        {
            int monsterIndex = Random.Range(0, currentWave.monsterPrefabs.Length);
            int randomPosition = Random.Range(0, spawnersLength);

            // ���� �������� ���� ��ġ�� ����
            Instantiate(currentWave.monsterPrefabs[monsterIndex], spawner.transform.GetChild(randomPosition).transform.position, spawner.transform.GetChild(randomPosition).transform.rotation);
            spawnMonsterCount++;

            yield return new WaitForSeconds(currentWave.spawnTime);
        }
    }
}

