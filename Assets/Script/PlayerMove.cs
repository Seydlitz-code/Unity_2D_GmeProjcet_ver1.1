using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
	public float movePower = 1f;  // 기본 이동 속도 변수
	public float runSpeed = 2f;  // 런 이동 속도 변수
	public float jumpPower = 1f;  // 점프 파워 변수
	Rigidbody2D rigid;
	SpriteRenderer renderer;  // SpriteRenderer 컴포넌트를 저장할 변수
	Animator animator;  // Animator 컴포넌트를 저장할 변수
	Vector3 movement;  // 이동방향 지정 변수
	bool isJumping = false;  // 점프 여부 확인 변수
	bool isRunning = false;  // 런 상태 여부 확인 변수
	Collider2D playerCollider;  // 플레이어의 콜라이더 변수

	void Start()
	{
		rigid = gameObject.GetComponent<Rigidbody2D>();
		renderer = gameObject.GetComponentInChildren<SpriteRenderer>();
		animator = gameObject.GetComponentInChildren<Animator>();  // Animator 컴포넌트 초기화
		playerCollider = gameObject.GetComponent<Collider2D>();  // 플레이어의 콜라이더 초기화
	}

	void Update()
	{
		// 걷기
		if (Input.GetAxisRaw("Horizontal") == 0)
		{
			animator.SetBool("isMoving", false);
			animator.SetBool("isRuning", false);
		}
		else if (Input.GetAxisRaw("Horizontal") < 0)
		{
			animator.SetInteger("Direction", -1);
			animator.SetBool("isMoving", true);
			// 달리기
			if (Input.GetKey(KeyCode.LeftShift))  // 플레이어가 시프트 버튼을 누르고 있다면?
			{
				isRunning = true;
				animator.SetBool("isRuning", true);  // 시프트 키를 누르고 있을 때도 isMoving을 true로 설정
			}
			else
			{
				isRunning = false;
				animator.SetBool("isRuning", false);
			}
		}
		else if (Input.GetAxisRaw("Horizontal") > 0)
		{
			animator.SetInteger("Direction", 1);
			animator.SetBool("isMoving", true);
			// 달리기
			if (Input.GetKey(KeyCode.LeftShift))  // 플레이어가 시프트 버튼을 누르고 있다면?
			{
				isRunning = true;
				animator.SetBool("isRuning", true);  // 시프트 키를 누르고 있을 때도 isMoving을 true로 설정
			}
			else
			{
				isRunning = false;
				animator.SetBool("isRuning", false);
			}
		}

		// 점프
		if (Input.GetButtonDown("Jump"))
		{
			isJumping = true;
			animator.SetTrigger("doJumping");
		}
	}

	void FixedUpdate()
	{
		Move();  // 이동 함수 호출
		Jump();  // 점프 함수 호출
	}

	void Move()
	{
		Vector3 moveVelocity = Vector3.zero;
		float currentMovePower = isRunning ? runSpeed : movePower;  // 현재 이동 속도 설정

		if (Input.GetAxisRaw("Horizontal") < 0)  // 좌측 이동 입력이 있다면?
		{
			moveVelocity = Vector3.left;
			renderer.flipX = true;  // 좌측으로 스프라이트 뒤집기
		}
		else if (Input.GetAxisRaw("Horizontal") > 0)  // 우측 이동 입력이 있다면?
		{
			moveVelocity = Vector3.right;
			renderer.flipX = false;  // 우측으로 스프라이트 뒤집기
		}

		transform.position += moveVelocity * currentMovePower * Time.deltaTime;
	}

	void Jump()
	{
		if (!isJumping)  // 점프 중이 아니라면?
			return;

		rigid.velocity = Vector2.zero;

		Vector2 jumpVelocity = new Vector2(0, jumpPower);
		rigid.AddForce(jumpVelocity, ForceMode2D.Impulse);

		isJumping = false;  // 점프 상태를 false로 설정한다.
	}

	//플레이어와 몬스터가 정면으로 충돌했는지 여부를 판단하는 함수
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Monster")
		{
			//플레이어와 몬스터가 정면에서 충돌했다면 플레이어 콜라이더를 제거, 플레이어 게임 오버 처리
			Destroy(playerCollider);
		}
	}
}
