using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GravityObject : MonoBehaviour {
	public Rigidbody body;
	public GravityObject target;
	public static List<GravityObject> gravityObjects;
	public static double scale = 1;
	public Vector3 initialVelocity;


	
	// Use this for initialization
	void Start () {
		if (gravityObjects == null) {
			gravityObjects = new List<GravityObject>();
		}
		gravityObjects.Add (this);
		body = GetComponent<Rigidbody> ();
		//body.centerOfMass = gameObject.transform.position;
		body.velocity = initialVelocity;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 appliedForce = new Vector3 ();
		//appliedForce = FindGravityForce (target);
		for (int i = 0; i < gravityObjects.Count; i++) {
			if (!GravityObject.gravityObjects [i].Equals(this)) {
				appliedForce = appliedForce + FindGravityForce (gravityObjects[i]);
			}
		}
		//body.AddForceAtPosition (appliedForce, gameObject.transform.position);
		body.AddForce (appliedForce);
	}
	

	Vector3 FindGravityForce(GravityObject m2) {
		return FindGravityForce (this, m2);
	}

	public static Vector3 FindGravityForce(GravityObject m1, GravityObject m2) {
		Vector3 direction =  (m2.gameObject.transform.position - m1.gameObject.transform.position);
		direction.Normalize ();
		double magnitude = 0.001 * m1.body.mass*m2.body.mass/(scale*scale*(m2.gameObject.transform.position - m1.gameObject.transform.position).sqrMagnitude);
		return (float)magnitude *  direction;
	}
	
}

