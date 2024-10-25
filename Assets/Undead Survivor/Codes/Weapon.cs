using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int id; // 무기ID
    public int prefabId; // 프리펩 ID
    public float damage; // 데미지
    public int count; // 개수
    public float speed; // 속도

    void Start()
    {
        Init();    
    }

    void Update()
    {
        switch (id)
        {
            case 0:
                transform.Rotate(Vector3.back * speed * Time.deltaTime);
                break;

            default:
                break;
        }

        // Test Code..
        if (Input.GetButtonDown("Jump")) {
            LevelUp(20, 5);
        }
    }

    public void LevelUp(float damage, int count)
    {
        this.damage = damage;
        this.count += count;

        if (id == 0) {
            Batch();
        }
    }

    public void Init()
    {
        switch (id) {
            case 0:
                speed = 150;
                Batch();
                break;

            default:
                break;
        }
    }

    void Batch()
    {
        for (int index = 0; index < count; index++) {
            Transform bullet;
            // 기존 오브젝트 먼저 활용하고 모자란 것은 풀링에서 가져오기
            if (index < transform.childCount)
            {
                bullet = transform.GetChild(index);
            }
            else {
                bullet = GameManager.instance.pool.Get(prefabId).transform;
                bullet.parent = transform;
            }


            bullet.localPosition = Vector3.zero;
            bullet.localRotation = Quaternion.identity;

            Vector3 rotVec = Vector3.forward * 360 * index / count;
            bullet.Rotate(rotVec);
            bullet.Translate(bullet.up * 1.5f, Space.World);

            bullet.GetComponent<Bullet>().Init(damage, -1); // -1 is Infinity Per.
        }
    }
}
