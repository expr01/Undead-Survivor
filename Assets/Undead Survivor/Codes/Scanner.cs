using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    public float scanRange; // 스캔 범위
    public LayerMask targetLayer; // target 레이어
    public RaycastHit2D[] targets; // 스캔 결과 배열
    public Transform nearestTarget;

    void FixedUpdate()
    {
        targets = Physics2D.CircleCastAll(transform.position, scanRange, Vector2.zero, 0, targetLayer);
        nearestTarget = GetNearest();
    }

    Transform GetNearest()
    {
        Transform result = null;
        float diff = 100;

        foreach (RaycastHit2D target in targets) {
            Vector3 myPos = transform.position;
            Vector3 targetPos = target.transform.position;
            float curDiff = Vector3.Distance(myPos, targetPos);

            // 최소의 거리를 가진 타겟을 찾음
            if (curDiff < diff) {
                diff = curDiff;
                result = target.transform;
            }
        }

        return result;
    }
}
