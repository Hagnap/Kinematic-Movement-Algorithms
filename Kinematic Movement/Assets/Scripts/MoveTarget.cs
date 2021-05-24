using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTarget : MonoBehaviour
{
	#region Variables
	[SerializeField] private float speed;

	private Rigidbody rb;
	#endregion

	#region Properties
	public float Speed
	{
		get { return this.speed; }
		set { this.speed = value; }
	}
	#endregion

	#region Unity Methods
	// Start is called before the first frame update
	void Start()
	{
		rb = this.GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	void Update()
	{
		rb.velocity = Vector3.forward * speed;
	}
	#endregion
}
