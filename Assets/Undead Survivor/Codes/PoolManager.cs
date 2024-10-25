using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    // �����յ��� ������ ����
    public GameObject[] prefabs;

    // Ǯ ����� �ϴ� ����Ʈ��
    List<GameObject>[] pools;

    void Awake()
    {
        pools = new List<GameObject>[prefabs.Length];

        for (int index = 0; index < prefabs.Length; index++) {
            pools[index] = new List<GameObject>();
        }

        Debug.Log(pools.Length);
    }

    // ���� ������Ʈ ��ȯ �Լ�, index�� � ����������
    public GameObject Get(int index)
    {
        GameObject select = null;

        // ������ pool�� ���(��Ȱ��ȭ ��) �ִ� ���� ������Ʈ ����
        foreach (GameObject item in pools[index]) {
            // �߰��ϸ� select ������ �Ҵ�
            if (!item.activeSelf) {
                select = item;
                select.SetActive(true);
                break;
            }
        }

        // �� ã������?
        if (!select) {
            // ���Ӱ� �����ϰ� select�� �Ҵ�
            select = Instantiate(prefabs[index], transform);
            pools[index].Add(select);
        }


        return select;
    }
}
