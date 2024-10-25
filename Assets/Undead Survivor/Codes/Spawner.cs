using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoint;
    public SpawnData[] spawnData;

    int level;
    float timer;

    void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>();    
    }

    void Update()
    {
        timer += Time.deltaTime;
        level = Mathf.Min(Mathf.FloorToInt(GameManager.instance.gameTime / 10f), spawnData.Length - 1);

        if (timer > spawnData[level].spawnTime) {            
            timer = 0;
            Spawn();
        }
    }

    void Spawn() {
        GameObject enemy = GameManager.instance.pool.Get(0);
        enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;
        enemy.GetComponent<Enemy>().Init(spawnData[level]);
    }
}

[System.Serializable]
public class SpawnData 
{
    public int spriteType; // 스프라이트 타입
    public float spawnTime; // 스폰 타이밍 (몬스터 스폰 간 간격)
    public int health; // 몬스터 체력
    public float speed; // 몬스터 스피드
}