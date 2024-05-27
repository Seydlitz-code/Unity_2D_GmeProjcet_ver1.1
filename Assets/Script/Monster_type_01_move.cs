using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_type_01_move : MonoBehaviour
{
	Rigidbody2D rigid;  // 몬스터의 Rigidbody2D 컴포넌트
	public int nextMove;    // 몬스터의 차기 이동 방향 지정 변수
	BoxCollider2D boxCollider;  // 몬스터의 BoxCollider2D 컴포넌트

	void Awake()
	{
		rigid = GetComponent<Rigidbody2D>();
		boxCollider = GetComponent<BoxCollider2D>();
		Invoke("Think", 2);
	}

	void FixedUpdate()
	{
		// 몬스터 이동 처리
		rigid.velocity = new Vector2(nextMove, rigid.velocity.y);
	}

	//몬스터의 이동 방향을 변경하기 위한 재귀함수
	void Think()
	{
		nextMove = Random.Range(-1, 2); //좌,우, 정지 3가지 상태 중 랜덤한 상태 하나를 지정한다.
		Invoke("Think", 2); // 2초마다 몬스터의 상태 변경
	}

	//플레이어와의 충돌 감지
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			//플레이어와의 충돌이 감지된 경우 몬스터의 콜라이더를 제거, 몬스터를 필드에서 제거한다.
			Destroy(boxCollider);
		}
	}
}