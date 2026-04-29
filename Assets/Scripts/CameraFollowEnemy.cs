using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowEnemy : MonoBehaviour
{

	private Transform Enemy;
	private Vector3 offset;

	// Use this for initialization
	void Start()
	{

		Enemy = GameObject.FindGameObjectWithTag("Enemy").transform;
		offset = transform.position - Enemy.position;

	}

	// Update is called once per frame
	void LateUpdate()
	{

		Vector3 newPosition = new Vector3(transform.position.x, transform.position.y, Enemy.position.z + offset.z);
		transform.position = newPosition;

	}
}