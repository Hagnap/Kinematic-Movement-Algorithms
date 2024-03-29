﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentController : MonoBehaviour
{
	#region Variables
	[SerializeField] private float maxSpeed;
	[SerializeField] private float turnSpeed;
	[SerializeField] private float timeToTarget;
	[SerializeField] private float radiusOfSatisfaction;
	[SerializeField] private float maxPredictionTime;

	[Tooltip("An empty gameobject")]
	[SerializeField] private GameObject target;
	[Tooltip("A rolling sphere")]
	[SerializeField] private GameObject movingTarget;

	private float orientation = 0f;
	private Vector3 unitVector = Vector3.one;
	private KinematicMovement kinematic;
	# endregion
	
	# region Properties
	public float Orientation
	{
		get { return this.orientation; }
		set { this.orientation = value; }
	}

	public float MaxSpeed
	{
		get { return this.maxSpeed; }
		set { this.maxSpeed = value; }
	}

	public float TurnSpeed
	{
		get { return this.turnSpeed; }
		set { this.turnSpeed = value; }
	}

	public float TimeToTarget
	{
		get { return this.timeToTarget; }
		set { this.timeToTarget = value; }
	}

	public float RadiusOfSatisfaction
	{
		get { return this.radiusOfSatisfaction; }
		set { this.radiusOfSatisfaction = value; }
	}

	public float MaxPredictionTime
	{
		get { return this.maxPredictionTime; }
		set { this.maxPredictionTime = value; }
	}

	public GameObject MovingTarget
	{
		get { return this.movingTarget; }
		set { this.movingTarget = value; }
	}

	public GameObject Target
	{
		get { return this.target; }
		set { this.target = value; }
	}
	#endregion

	#region Unity Methods
	// Start is called before the first frame update
	void Start()
    {
		kinematic = this.GetComponent<KinematicMovement>();
	}

    // Update is called once per frame
    void FixedUpdate()
    {
		updateUnitVector();

		//kinematic.runKinematicSeek(movingTarget);
		//kinematic.runKinematicFlee(movingTarget);
		//kinematic.runKinematicArrive(target);
		//kinematic.runKinematicWander();
		kinematic.runKinematicPursue(movingTarget);
		//kinematic.runKinematicEvade(movingTarget);
	}
	#endregion

	#region Custom Methods
	public void updateUnitVector()
	{
		///Debug.Log("Unit Vector");
		///Debug.Log("X: " + Mathf.Sin(Mathf.Deg2Rad * this.transform.position.x));
		///Debug.Log("Z: " + Mathf.Cos(Mathf.Deg2Rad * this.transform.position.z));

		// Right Handed Movement
		unitVector.x = Mathf.Sin(Mathf.Deg2Rad * this.transform.position.x);
		unitVector.z = Mathf.Cos(Mathf.Deg2Rad * this.transform.position.z);
		
		// Left Handed Movement
		//unitVector.x = -Mathf.Sin(Mathf.Deg2Rad * this.transform.position.x);
		//unitVector.z = Mathf.Cos(Mathf.Deg2Rad * this.transform.position.z);

		orientation = unitVector.magnitude;

		///Debug.Log("Orientation: " + orientation);
	}

	public float generateRandomBinomial()
	{
		return Random.value - Random.value;
	}
	# endregion
}
