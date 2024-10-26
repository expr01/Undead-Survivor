using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{

    RectTransform rect;

    void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    void FixedUpdate()
    {
        // 월드 상 좌표계 위치를 스크린 좌표계 위치로 변환
        rect.position = Camera.main.WorldToScreenPoint(GameManager.instance.player.transform.position);    
    }
}
