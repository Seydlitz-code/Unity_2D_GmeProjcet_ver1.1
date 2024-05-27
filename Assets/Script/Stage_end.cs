using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage_end : MonoBehaviour
{
	// 플레이어와의 충돌 감지
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			// 플레이어와의 충돌이 감지된 경우 메시지를 출력한다
			Debug.Log("스테이지를 클리어했습니다.");
		}
	}
}
