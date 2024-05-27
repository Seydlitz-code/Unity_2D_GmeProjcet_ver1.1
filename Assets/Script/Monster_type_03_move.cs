using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_type_03_move : MonoBehaviour
{
	public float movePower = 1f;	//몬스터 이동 속도 변수
	Animator animator;  //Animator 컴포넌트 변수
	Vector3 movement;	//이동방향 변수
	int movementFlag = 0;   //이동 상태 변수 (0: 정지, 1: 왼쪽, 2: 오른쪽)
	bool isTracing = false;	//추적여부 판단 변수
	GameObject traceTarget;	//추적 대상 오브젝트 저장 변수
	
	void Start()
	{
		animator = gameObject.GetComponentInChildren<Animator>();
		StartCoroutine("ChangeMovement");
	}

	// 일정 시간마다 이동 상태를 변경하는 코루틴
	IEnumerator ChangeMovement()
	{
		movementFlag = Random.Range(0, 3); // 0, 1, 2 중 하나를 무작위로 선택하여 movementFlag에 저장

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
		string dist = "";
		// 추적 상태에 따라 이동 방향 결정
		if (isTracing)
		{
			Vector3 playerPos = traceTarget.transform.position;
			if (playerPos.x < transform.position.x)
				dist = "Left"; // 플레이어가 왼쪽에 있을 때
			else if (playerPos.x > transform.position.x)
				dist = "Right"; // 플레이어가 오른쪽에 있을 때
		}
		else
		{
			if (movementFlag == 1)
				dist = "Left"; // 무작위 이동: 왼쪽
			if (movementFlag == 2)
				dist = "Right"; // 무작위 이동: 오른쪽
		}

		// 이동 방향에 따른 이동 벡터 설정 및 스프라이트 반전
		if (dist == "Left")
		{
			moveVelocity = Vector3.left;
			transform.localScale = new Vector3(1, 1, 1);
		}
		else if (dist == "Right")
		{
			moveVelocity = Vector3.right;
			transform.localScale = new Vector3(-1, 1, 1);
		}

		// 이동 속도에 따라 위치 변경
		transform.position += moveVelocity * movePower * Time.deltaTime;
	}

	// 플레이어와의 충돌 감지
	void OnTriggerEnter2D(Collider2D other)
	{
		// 충돌한 오브젝트의 태그가 "Player"인 경우
		if (other.gameObject.tag == "Player")
		{
			traceTarget = other.gameObject; // 추적할 대상을 플레이어로 설정
			isTracing = true; // 추적 상태를 true로 설정
			StopCoroutine("ChangeMovement"); // ChangeMovement 코루틴 중지
		}
	}

	// 트리거 충돌이 유지되는 동안 호출되는 함수
	void OnTriggerStay2D(Collider2D other)
	{
		// 충돌한 오브젝트의 태그가 "Player"인 경우
		if (other.gameObject.tag == "Player")
		{
			isTracing = true; // 추적 상태를 true로 설정
			animator.SetBool("isMoving", true); // 이동 애니메이션 설정
		}
	}

	// 트리거 충돌이 끝났을 때 호출되는 함수
	void OnTriggerExit2D(Collider2D other)
	{
		// 충돌한 오브젝트의 태그가 "Player"인 경우
		if (other.gameObject.tag == "Player")
		{
			isTracing = false; // 추적 상태를 false로 설정
			StartCoroutine("ChangeMovement"); // ChangeMovement 코루틴 재시작
		}
	}
}
