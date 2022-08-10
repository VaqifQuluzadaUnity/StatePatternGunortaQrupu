using System.Collections;
using UnityEngine;

public class Skeleton : Enemy
{
	private IEnumerator patrolCoroutine;

	private IEnumerator followCoroutine;

	private IEnumerator fleeCoroutine;

	private Transform currentPatrolPoint;

	public override void UpdateStates()
	{


		if (Vector3.Distance(transform.position, player.transform.position) < detectionDistance)
		{
			if (health <= 20)
			{
				currentState = EnemyState.FLEE;
			}
			else if (Vector3.Distance(transform.position, player.transform.position) < attackDistance)
			{
				currentState = EnemyState.ATTACK;
			}
			else
			{
				currentState = EnemyState.FOLLOW_PLAYER;
			}
		}
		else
		{
			currentState = EnemyState.PATROL;
		}

		DoAction();
	}

	public override void DoPatrol()
	{
		if (currentPatrolPoint != null)
		{
			return;
		}

		currentPatrolPoint = ReturnPatrolPoint();


		patrolCoroutine = PatrolCoroutine();

		StartCoroutine(patrolCoroutine);
	}

	IEnumerator PatrolCoroutine()
	{
		while (transform.position != currentPatrolPoint.position)
		{
			transform.position =
				Vector3.MoveTowards(transform.position, currentPatrolPoint.position, moveSpeed * Time.deltaTime);
			yield return new WaitForEndOfFrame();
		}

		yield return new WaitForSeconds(3f);
		currentPatrolPoint = null;
	}

	public override void StopPatrol()
	{
		if (patrolCoroutine != null)
		{
			StopCoroutine(patrolCoroutine);
		}
		currentPatrolPoint = null;
		
	}
	public override void DoFollowPlayer()
	{
		if (followCoroutine != null)
		{
			return;
		}
		followCoroutine = FollowCoroutine();

		StartCoroutine(followCoroutine);
	}
	IEnumerator FollowCoroutine()
	{
		while (transform.position != player.transform.position)
		{
			transform.position =
				Vector3.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
			yield return new WaitForEndOfFrame();
		}

		followCoroutine = null;
	}

	public override void StopFollowPlayer()
	{
		if (followCoroutine != null)
		{
			StopCoroutine(followCoroutine);
		}
		followCoroutine = null;
	}

	public override void DoFlee()
	{
		if (fleeCoroutine != null)
		{
			return;
		}
		transform.rotation = Quaternion.LookRotation(transform.position - player.transform.position);

		fleeCoroutine = FleeCoroutine();

		StartCoroutine(fleeCoroutine);
	}

	IEnumerator FleeCoroutine()
	{
		while (currentState == EnemyState.FLEE)
		{
			transform.Translate(transform.forward * escapeSpeed * Time.deltaTime);
			yield return new WaitForEndOfFrame();
		}
	}

	public override void StopFlee()
	{
		if (fleeCoroutine != null)
		{
			StopCoroutine(fleeCoroutine);
		}

		fleeCoroutine = null;
	}






	private Transform ReturnPatrolPoint()
	{
		int randomPatrolIndex = Random.Range(0, EnemyManager.instance.patrolPoints.Length);

		return EnemyManager.instance.patrolPoints[randomPatrolIndex];
	}
}
