using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin_type_02 : MonoBehaviour
{
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			// �÷��̾���� �浹�� ������ ��� ���� ������Ʈ�� �����Ѵ�
			Destroy(gameObject);
		}
	}
}
