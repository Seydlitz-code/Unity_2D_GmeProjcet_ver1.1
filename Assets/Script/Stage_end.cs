using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage_end : MonoBehaviour
{
	// �÷��̾���� �浹 ����
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			// �÷��̾���� �浹�� ������ ��� �޽����� ����Ѵ�
			Debug.Log("���������� Ŭ�����߽��ϴ�.");
		}
	}
}
