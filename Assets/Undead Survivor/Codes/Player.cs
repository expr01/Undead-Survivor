using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Vector2 inputVec;
    public float speed;

    Rigidbody2D rigid;
    SpriteRenderer spriter;
    Animator anim;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // 키값 받아옴
        inputVec.x = Input.GetAxisRaw("Horizontal");    
        inputVec.y = Input.GetAxisRaw("Vertical");    
    }

    void FixedUpdate()
    {
        // 위치 이동
        Vector2 nextVec = inputVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
    }

    void LateUpdate()
    {
        // 애니메이션의 Speed 파라미터를 바꿈 (벡터의 크기로)
        anim.SetFloat("Speed", inputVec.magnitude);

        // 캐릭터를 좌우 반전 시킴.
        if (inputVec.x != 0) {
            spriter.flipX = inputVec.x < 0;
        }
    }
}
