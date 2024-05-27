using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_type_03_move : MonoBehaviour
{
	public float movePower = 1f;	//���� �̵� �ӵ� ����
	Animator animator;  //Animator ������Ʈ ����
	Vector3 movement;	//�̵����� ����
	int movementFlag = 0;   //�̵� ���� ���� (0: ����, 1: ����, 2: ������)
	bool isTracing = false;	//�������� �Ǵ� ����
	GameObject traceTarget;	//���� ��� ������Ʈ ���� ����
	
	void Start()
	{
		animator = gameObject.GetComponentInChildren<Animator>();
		StartCoroutine("ChangeMovement");
	}

	// ���� �ð����� �̵� ���¸� �����ϴ� �ڷ�ƾ
	IEnumerator ChangeMovement()
	{
		movementFlag = Random.Range(0, 3); // 0, 1, 2 �� �ϳ��� �������� �����Ͽ� movementFlag�� ����

		// �ִϸ��̼� ����
		if (movementFlag == 0)
			animator.SetBool("isMoving", false); // ���� �ִϸ��̼�
		else
			animator.SetBool("isMoving", true); // �̵� �ִϸ��̼�

		// 1�� ���� ���
		yield return new WaitForSeconds(1f);
		// �ڷ�ƾ �����
		StartCoroutine("ChangeMovement");
	}
	void FixedUpdate()
	{
		Move();
	}

	// ���� �̵� ó��
	void Move()
	{
		Vector3 moveVelocity = Vector3.zero;
		string dist = "";
		// ���� ���¿� ���� �̵� ���� ����
		if (isTracing)
		{
			Vector3 playerPos = traceTarget.transform.position;
			if (playerPos.x < transform.position.x)
				dist = "Left"; // �÷��̾ ���ʿ� ���� ��
			else if (playerPos.x > transform.position.x)
				dist = "Right"; // �÷��̾ �����ʿ� ���� ��
		}
		else
		{
			if (movementFlag == 1)
				dist = "Left"; // ������ �̵�: ����
			if (movementFlag == 2)
				dist = "Right"; // ������ �̵�: ������
		}

		// �̵� ���⿡ ���� �̵� ���� ���� �� ��������Ʈ ����
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

		// �̵� �ӵ��� ���� ��ġ ����
		transform.position += moveVelocity * movePower * Time.deltaTime;
	}

	// �÷��̾���� �浹 ����
	void OnTriggerEnter2D(Collider2D other)
	{
		// �浹�� ������Ʈ�� �±װ� "Player"�� ���
		if (other.gameObject.tag == "Player")
		{
			traceTarget = other.gameObject; // ������ ����� �÷��̾�� ����
			isTracing = true; // ���� ���¸� true�� ����
			StopCoroutine("ChangeMovement"); // ChangeMovement �ڷ�ƾ ����
		}
	}

	// Ʈ���� �浹�� �����Ǵ� ���� ȣ��Ǵ� �Լ�
	void OnTriggerStay2D(Collider2D other)
	{
		// �浹�� ������Ʈ�� �±װ� "Player"�� ���
		if (other.gameObject.tag == "Player")
		{
			isTracing = true; // ���� ���¸� true�� ����
			animator.SetBool("isMoving", true); // �̵� �ִϸ��̼� ����
		}
	}

	// Ʈ���� �浹�� ������ �� ȣ��Ǵ� �Լ�
	void OnTriggerExit2D(Collider2D other)
	{
		// �浹�� ������Ʈ�� �±װ� "Player"�� ���
		if (other.gameObject.tag == "Player")
		{
			isTracing = false; // ���� ���¸� false�� ����
			StartCoroutine("ChangeMovement"); // ChangeMovement �ڷ�ƾ �����
		}
	}
}
