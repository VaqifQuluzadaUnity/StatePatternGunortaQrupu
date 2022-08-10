using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
  public static EnemyManager instance;

	public Transform[] patrolPoints;

	public List<Enemy> enemies;

	private void Awake()
	{
		if(instance!=null && instance != this)
		{
			Destroy(instance.gameObject);
		}

		instance = this;
	}

	private void Update()
	{
		foreach(Enemy enemy in enemies)
		{
			enemy.UpdateStates();
		}
	}
}
