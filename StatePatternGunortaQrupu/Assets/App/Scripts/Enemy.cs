using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
  public EnemyState currentState;
	
	public int health;

  public int moveSpeed;

  public int escapeSpeed;

  public int attackSpeed;

  public float detectionDistance;

  public float attackDistance;

	public GameObject player;

	//public void InitializeEnemy(EnemyState initialState,)
	//{
	//	currentState = initialState;
	//}

	private void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player");
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.yellow;

		Gizmos.DrawWireSphere(transform.position, detectionDistance);

		Gizmos.color = Color.red;

		Gizmos.DrawWireSphere(transform.position, attackDistance);
	}

	public virtual void UpdateStates()
	{
		Debug.Log("Updating states");
	}

	public void DoAction()
	{
		switch (currentState)
		{
			case EnemyState.PATROL:
				StopAttack();
				StopFlee();
				StopFollowPlayer();
				DoPatrol();
				break;
			case EnemyState.FOLLOW_PLAYER:
				DoFollowPlayer();
				StopAttack();
				StopFlee();
				StopPatrol();
				break;
			case EnemyState.ATTACK:
				//StopFollowPlayer();
				//StopFlee();
				//StopPatrol();
				//DoAttack();
				break;
			case EnemyState.FLEE:
				StopFollowPlayer();
				StopAttack();
				StopPatrol();
				DoFlee();
				break;
		}
	}

	public virtual void DoPatrol()
	{
		
	}

	public virtual void StopPatrol()
	{

	}
	public virtual void DoFollowPlayer()
	{

	}

	public virtual void StopFollowPlayer()
	{

	}
	public virtual void DoAttack()
	{

	}
	public virtual void StopAttack()
	{

	}
	public virtual void DoFlee()
	{

	}
	public virtual void StopFlee()
	{

	}
}

public enum EnemyState {PATROL,FOLLOW_PLAYER,ATTACK, FLEE}