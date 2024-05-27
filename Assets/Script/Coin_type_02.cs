using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin_type_02 : MonoBehaviour
{
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			// 플레이어와의 충돌이 감지된 경우 코인 오브젝트를 삭제한다
			Destroy(gameObject);
		}
	}
}
