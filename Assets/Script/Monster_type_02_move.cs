using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_type_02_move : MonoBehaviour
{
	public float movePower = 1f;    //���� �̵��ӵ� ����
	Animator animator;  // Animator ������Ʈ ����
	Vector3 movement;   // �̵� ���� ����
	int movementFlag = 0;   // �̵� ���� ���� (0: ����, 1: ����, 2: ������)
	BoxCollider2D boxCollider;  // ������ BoxCollider2D ������Ʈ ����

	void Start()
	{
		animator = gameObject.GetComponentInChildren<Animator>();
		boxCollider = GetComponent<BoxCollider2D>();
		StartCoroutine("ChangeMovement");
	}

	// ���� �ð����� �̵� ���¸� �����ϴ� �ڷ�ƾ
	IEnumerator ChangeMovement()
	{
		movementFlag = Random.Range(0, 3); // -1, 0, 1 �� �ϳ��� �������� �����Ͽ� movementFlag�� ����

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

		// �̵� ���� ����
		if (movementFlag == 1)
		{
			moveVelocity = Vector3.left; // �������� �̵�
			transform.localScale = new Vector3(1, 1, 1); // ��������Ʈ �¿� ����
		}
		else if (movementFlag == 2)
		{
			moveVelocity = Vector3.right; // ���������� �̵�
			transform.localScale = new Vector3(-1, 1, 1); // ��������Ʈ �¿� ����
		}

		// �̵� �ӵ��� ���� ��ġ ����
		transform.position += moveVelocity * movePower * Time.deltaTime;
	}

	// �÷��̾� ������Ʈ���� �浹 ����
	void OnTriggerEnter2D(Collider2D other)
	{
		// �÷��̾� ������Ʈ�� �浹�� ���
		if (other.gameObject.tag == "Player")
		{
			// ���� ����
			Destroy(boxCollider);
		}
	}
}
