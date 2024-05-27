using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_type_01_move : MonoBehaviour
{
	Rigidbody2D rigid;  // ������ Rigidbody2D ������Ʈ
	public int nextMove;    // ������ ���� �̵� ���� ���� ����
	BoxCollider2D boxCollider;  // ������ BoxCollider2D ������Ʈ

	void Awake()
	{
		rigid = GetComponent<Rigidbody2D>();
		boxCollider = GetComponent<BoxCollider2D>();
		Invoke("Think", 2);
	}

	void FixedUpdate()
	{
		// ���� �̵� ó��
		rigid.velocity = new Vector2(nextMove, rigid.velocity.y);
	}

	//������ �̵� ������ �����ϱ� ���� ����Լ�
	void Think()
	{
		nextMove = Random.Range(-1, 2); //��,��, ���� 3���� ���� �� ������ ���� �ϳ��� �����Ѵ�.
		Invoke("Think", 2); // 2�ʸ��� ������ ���� ����
	}

	//�÷��̾���� �浹 ����
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			//�÷��̾���� �浹�� ������ ��� ������ �ݶ��̴��� ����, ���͸� �ʵ忡�� �����Ѵ�.
			Destroy(boxCollider);
		}
	}
}