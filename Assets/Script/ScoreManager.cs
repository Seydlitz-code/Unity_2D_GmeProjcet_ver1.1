using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
	static int score = 0;

	public static void setScore(int value)
	{
		score += value;
	}

	public static int getScore()
	{
		return score;
	}
}
