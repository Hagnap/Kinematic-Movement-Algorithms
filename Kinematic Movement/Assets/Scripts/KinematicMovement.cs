using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// What are Kinematic Movement Algorithms?
///		- Algorithms that are used to move an autonomous agent. These algorithms use three things
///			1) Position
///			2) Orientation
///			3) Velocity
///			
///			Linear Velocity & Angular Velocity are also used
/// </summary>
public class KinematicMovement : MonoBehaviour
{
	#region Variables
	private AgentController agent;
	private Rigidbody rb;

	private Quaternion rotation;
	private float distance;
	private Vector3 direction;
	# endregion
	
	# region Properties
	
	# endregion
	
	# region Unity Methods
	// Start is called before the first frame update
    void Start()
    {
		agent = this.GetComponent<AgentController>();
		rb = this.GetComponent<Rigidbody>();
	}

    // Update is called once per frame
    void Update()
    {
        
    }
	#endregion

	#region Kinematic Movement Methods

	/// <summary>
	/// Calculates the direction to the target and moves to the target. Best used when the target is moving. 
	/// </summary>
	/// <param name="target">The target is what the agent move towards.</param>
	public void runKinematicSeek(GameObject target)
	{
		// Gets the direction to the target & its distance from the agent
		direction = target.transform.position - this.transform.position;
		distance = direction.magnitude;

		// Face the direction we want to move in
		rotation = Quaternion.LookRotation(direction);
		this.transform.rotation = Quaternion.Lerp(this.transform.rotation, rotation, agent.TurnSpeed * Time.deltaTime);

		// Sets the velocity, at full speed
		direction.Normalize();
		direction *= agent.MaxSpeed;

		Debug.DrawLine(this.transform.position, target.transform.position);

		rb.velocity = direction;
	}

	/// <summary>
	/// Similar to seek however it will move away from the target.
	/// </summary>
	/// <param name="target">The target is what the agent moves away from.</param>
	public void runKinematicFlee(GameObject target)
	{
		// Gets the direction to the target & its distance from the agent
		direction = this.transform.position - target.transform.position;
		distance = direction.magnitude;

		// Sets the velocity, at full speed
		direction.Normalize();
		direction *= agent.MaxSpeed;

		// Face the direction we want to move in
		rotation = Quaternion.LookRotation(direction);
		this.transform.rotation = Quaternion.Lerp(this.transform.rotation, rotation, agent.TurnSpeed * Time.deltaTime);

		Debug.DrawLine(this.transform.position, target.transform.position);

		rb.velocity = direction;
	}

	/// <summary>
	/// Calculates the direction to the target and moves to the target. Best used when the target is stationary. 
	/// 
	///	Incorporates a Radius of Satisfaction and modifies the speed with respect to the agent's distance from the target. Other than that, the algorithms are fairly similar.
	/// `timeToTarget` Helps the agent slow down (Higher values → More gentle deceleration & Lower values → More abrupt deceleration).
	/// </summary>
	/// <param name="target">The target is what the agent moves towards.</param>
	public void runKinematicArrive(GameObject target)
	{
		// Gets the direction to the target & its distance from the agent
		direction = target.transform.position - this.transform.position;
		distance = direction.magnitude;

		// Check if the agent is 'close enough' to the target
		if (direction.magnitude > agent.RadiusOfSatisfaction)
		{
			///Debug.Log(direction.magnitude + " : " + agent.RadiusOfSatisfaction);

			// Makes the agent get to the target in `timeToTarget` seconds
			direction /= agent.TimeToTarget;

			// Face the direction we want the agent to move towards
			rotation = Quaternion.LookRotation(direction);
			this.transform.rotation = Quaternion.Lerp(this.transform.rotation, rotation, agent.TurnSpeed * Time.deltaTime);

			// If the agent is moving too fast → Limit it to maxSpeed
			if (direction.magnitude > agent.RadiusOfSatisfaction)
			{
				direction.Normalize();
				direction *= agent.MaxSpeed;
			}

			Debug.DrawLine(this.transform.position, target.transform.position);

			// Sets the agent's velocity
			rb.velocity = direction;
		}
	}

	/// <summary>
	/// 
	/// </summary>
	public void runKinematicWander()
	{
		// Moves the agent forward via setting the agent's velocity
		rb.velocity = agent.MaxSpeed * this.transform.forward;
		rb.velocity.Normalize();

		rb.angularVelocity = new Vector3(rb.angularVelocity.x, rb.angularVelocity.y + (agent.generateRandomBinomial() * agent.TurnSpeed), rb.angularVelocity.z);
	}

	/// <summary>
	/// An exetension of Kinematic-Seek. Approximates the target's future position and then passes that position into Seek so that the agent moves to that position.
	/// </summary>
	/// <param name="target">A moving target that the agent is pursuing.</param>
	public void runKinematicPursue(GameObject target)
	{
		float predictionTime = 0f;

		// Gets the direction to the target & its distance from the agent
		direction = target.transform.position - this.transform.position;
		distance = direction.magnitude;

		// Calculates the agent's current speed
		float speed = rb.velocity.magnitude;

		// Ensures there's enough time to make a prediction given the agent's speed
		if (speed <= distance / agent.MaxPredictionTime)
		{
			predictionTime = agent.MaxPredictionTime;
		}
		else
		{
			predictionTime = distance / speed;
		}

		// Calculates the movingTarget's future position given its current position, velocity, and its time to make a prediction
		Vector3 calculatedPosition = target.transform.position + target.GetComponent<Rigidbody>().velocity * predictionTime;

		// Face the direction we want the agent to move towards
		rotation = Quaternion.LookRotation(calculatedPosition);
		this.transform.rotation = Quaternion.Lerp(this.transform.rotation, rotation, agent.TurnSpeed * Time.deltaTime);

		Debug.DrawLine(agent.transform.position, calculatedPosition);

		// Set the target to be the calculated position
		agent.Target.transform.position = calculatedPosition;

		// Delegates to Seek
		runKinematicSeek(agent.Target);
	}

	/// <summary>
	/// An exetension of Kinematic-Flee. Approximates the target's future position and then passes that position into Flee so that the agent moves away from that position.
	/// </summary>
	/// <param name="target">A moving target that the agent is getting away from.</param>
	public void runKinematicEvade(GameObject target)
	{
		float predictionTime = 0f;

		// Gets the direction to the target & its distance from the agent
		direction = target.transform.position - this.transform.position;
		distance = direction.magnitude;

		// Calculates the agent's current speed
		float speed = rb.velocity.magnitude;

		// Ensures there's enough time to make a prediction given the agent's speed
		if (speed <= distance / agent.MaxPredictionTime)
		{
			predictionTime = agent.MaxPredictionTime;
		}
		else
		{
			predictionTime = distance / speed;
		}

		// Calculates the movingTarget's future position given its current position, velocity, and its time to make a prediction
		Vector3 calculatedPosition = target.transform.position + target.GetComponent<Rigidbody>().velocity * predictionTime;

		// Face the direction we want the agent to move towards
		rotation = Quaternion.LookRotation(calculatedPosition);
		this.transform.rotation = Quaternion.Lerp(this.transform.rotation, rotation, agent.TurnSpeed * Time.deltaTime);

		Debug.DrawLine(agent.transform.position, calculatedPosition);

		// Set the target to be the calculated position
		agent.Target.transform.position = calculatedPosition;

		// Delegates to Seek
		runKinematicFlee(agent.Target);
	}
	#endregion
}
