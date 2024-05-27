using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_type_02_move : MonoBehaviour
{
	public float movePower = 1f;    //몬스터 이동속도 변수
	Animator animator;  // Animator 컴포넌트 변수
	Vector3 movement;   // 이동 방향 변수
	int movementFlag = 0;   // 이동 상태 변수 (0: 정지, 1: 왼쪽, 2: 오른쪽)
	BoxCollider2D boxCollider;  // 몬스터의 BoxCollider2D 컴포넌트 변수

	void Start()
	{
		animator = gameObject.GetComponentInChildren<Animator>();
		boxCollider = GetComponent<BoxCollider2D>();
		StartCoroutine("ChangeMovement");
	}

	// 일정 시간마다 이동 상태를 변경하는 코루틴
	IEnumerator ChangeMovement()
	{
		movementFlag = Random.Range(0, 3); // -1, 0, 1 중 하나를 무작위로 선택하여 movementFlag에 저장

		// 애니메이션 설정
		if (movementFlag == 0)
			animator.SetBool("isMoving", false); // 정지 애니메이션
		else
			animator.SetBool("isMoving", true); // 이동 애니메이션

		// 1초 동안 대기
		yield return new WaitForSeconds(1f);

		// 코루틴 재시작
		StartCoroutine("ChangeMovement");
	}
	void FixedUpdate()
	{
		Move();
	}

	// 몬스터 이동 처리
	void Move()
	{
		Vector3 moveVelocity = Vector3.zero;

		// 이동 방향 설정
		if (movementFlag == 1)
		{
			moveVelocity = Vector3.left; // 왼쪽으로 이동
			transform.localScale = new Vector3(1, 1, 1); // 스프라이트 좌우 반전
		}
		else if (movementFlag == 2)
		{
			moveVelocity = Vector3.right; // 오른쪽으로 이동
			transform.localScale = new Vector3(-1, 1, 1); // 스프라이트 좌우 반전
		}

		// 이동 속도에 따라 위치 변경
		transform.position += moveVelocity * movePower * Time.deltaTime;
	}

	// 플레이어 오브젝트와의 충돌 감지
	void OnTriggerEnter2D(Collider2D other)
	{
		// 플레이어 오브젝트와 충돌한 경우
		if (other.gameObject.tag == "Player")
		{
			// 몬스터 제거
			Destroy(boxCollider);
		}
	}
}
