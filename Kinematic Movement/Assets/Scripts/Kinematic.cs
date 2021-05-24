using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kinematic : MonoBehaviour
{
	#region Variables
	private Vector3 position;
	private float orientation;
	private float velocity; // Linear Velocity
	private float rotation; // Angular Velocity

	float half_t_square;
	# endregion
	
	# region Properties
	public Vector3 Position
	{
		get { return this.transform.position; }
		set { this.transform.position = value; }
	}

	public float Orientation
	{
		get { return this.orientation; }
		set { this.orientation = value; }
	}

	public float Velocity
	{
		get { return this.velocity; }
		set { this.velocity = value; }
	}

	public float Rotation
	{
		get { return this.rotation; }
		set { this.rotation = value; }
	}
	#endregion

	#region Unity Methods
	 void Update()
	{
		
	}
	#endregion
}
