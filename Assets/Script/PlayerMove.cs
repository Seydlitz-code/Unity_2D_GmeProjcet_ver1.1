using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
	public float movePower = 1f;  // �⺻ �̵� �ӵ� ����
	public float runSpeed = 2f;  // �� �̵� �ӵ� ����
	public float jumpPower = 1f;  // ���� �Ŀ� ����
	Rigidbody2D rigid;
	SpriteRenderer renderer;  // SpriteRenderer ������Ʈ�� ������ ����
	Animator animator;  // Animator ������Ʈ�� ������ ����
	Vector3 movement;  // �̵����� ���� ����
	bool isJumping = false;  // ���� ���� Ȯ�� ����
	bool isRunning = false;  // �� ���� ���� Ȯ�� ����
	Collider2D playerCollider;  // �÷��̾��� �ݶ��̴� ����

	void Start()
	{
		rigid = gameObject.GetComponent<Rigidbody2D>();
		renderer = gameObject.GetComponentInChildren<SpriteRenderer>();
		animator = gameObject.GetComponentInChildren<Animator>();  // Animator ������Ʈ �ʱ�ȭ
		playerCollider = gameObject.GetComponent<Collider2D>();  // �÷��̾��� �ݶ��̴� �ʱ�ȭ
	}

	void Update()
	{
		// �ȱ�
		if (Input.GetAxisRaw("Horizontal") == 0)
		{
			animator.SetBool("isMoving", false);
			animator.SetBool("isRuning", false);
		}
		else if (Input.GetAxisRaw("Horizontal") < 0)
		{
			animator.SetInteger("Direction", -1);
			animator.SetBool("isMoving", true);
			// �޸���
			if (Input.GetKey(KeyCode.LeftShift))  // �÷��̾ ����Ʈ ��ư�� ������ �ִٸ�?
			{
				isRunning = true;
				animator.SetBool("isRuning", true);  // ����Ʈ Ű�� ������ ���� ���� isMoving�� true�� ����
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
			// �޸���
			if (Input.GetKey(KeyCode.LeftShift))  // �÷��̾ ����Ʈ ��ư�� ������ �ִٸ�?
			{
				isRunning = true;
				animator.SetBool("isRuning", true);  // ����Ʈ Ű�� ������ ���� ���� isMoving�� true�� ����
			}
			else
			{
				isRunning = false;
				animator.SetBool("isRuning", false);
			}
		}

		// ����
		if (Input.GetButtonDown("Jump"))
		{
			isJumping = true;
			animator.SetTrigger("doJumping");
		}
	}

	void FixedUpdate()
	{
		Move();  // �̵� �Լ� ȣ��
		Jump();  // ���� �Լ� ȣ��
	}

	void Move()
	{
		Vector3 moveVelocity = Vector3.zero;
		float currentMovePower = isRunning ? runSpeed : movePower;  // ���� �̵� �ӵ� ����

		if (Input.GetAxisRaw("Horizontal") < 0)  // ���� �̵� �Է��� �ִٸ�?
		{
			moveVelocity = Vector3.left;
			renderer.flipX = true;  // �������� ��������Ʈ ������
		}
		else if (Input.GetAxisRaw("Horizontal") > 0)  // ���� �̵� �Է��� �ִٸ�?
		{
			moveVelocity = Vector3.right;
			renderer.flipX = false;  // �������� ��������Ʈ ������
		}

		transform.position += moveVelocity * currentMovePower * Time.deltaTime;
	}

	void Jump()
	{
		if (!isJumping)  // ���� ���� �ƴ϶��?
			return;

		rigid.velocity = Vector2.zero;

		Vector2 jumpVelocity = new Vector2(0, jumpPower);
		rigid.AddForce(jumpVelocity, ForceMode2D.Impulse);

		isJumping = false;  // ���� ���¸� false�� �����Ѵ�.
	}

	//�÷��̾�� ���Ͱ� �������� �浹�ߴ��� ���θ� �Ǵ��ϴ� �Լ�
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Monster")
		{
			//�÷��̾�� ���Ͱ� ���鿡�� �浹�ߴٸ� �÷��̾� �ݶ��̴��� ����, �÷��̾� ���� ���� ó��
			Destroy(playerCollider);
		}
	}
}
