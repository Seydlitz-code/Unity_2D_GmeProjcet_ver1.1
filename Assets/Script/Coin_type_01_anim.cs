using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin_type_01_anim : MonoBehaviour
{
	// �÷��̾���� �浹 ����
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			// �÷��̾���� �浹�� ������ ��� ���� ������Ʈ�� �����Ѵ�
			Destroy(gameObject);
		}
	}
}
