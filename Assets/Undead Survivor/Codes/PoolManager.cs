using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    // 프리팹들을 보관할 변수
    public GameObject[] prefabs;

    // 풀 담당을 하는 리스트들
    List<GameObject>[] pools;

    void Awake()
    {
        pools = new List<GameObject>[prefabs.Length];

        for (int index = 0; index < prefabs.Length; index++) {
            pools[index] = new List<GameObject>();
        }

        Debug.Log(pools.Length);
    }

    // 게임 오브젝트 반환 함수, index는 어떤 프리팹인지
    public GameObject Get(int index)
    {
        GameObject select = null;

        // 선택한 pool에 놀고(비활성화 된) 있는 게임 오브젝트 접근
        foreach (GameObject item in pools[index]) {
            // 발견하면 select 변수에 할당
            if (!item.activeSelf) {
                select = item;
                select.SetActive(true);
                break;
            }
        }

        // 못 찾았으면?
        if (!select) {
            // 새롭게 생성하고 select에 할당
            select = Instantiate(prefabs[index], transform);
            pools[index].Add(select);
        }


        return select;
    }
}
