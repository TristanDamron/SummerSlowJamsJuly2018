using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
SOURCE: https://www.octomangames.com/unity-tutorials/unity-particle-system-tutorial-code-circles/
 */
public class CircularParticles : MonoBehaviour {
	public int frequency = 2;//repeatrate
	public float resolution = 20;//the amount of keys used to create the curves
	public float amplitude = -1.0f;// the height of the values(-1.0f,1.0f)
	public float Zvalue = 0f;//Easy access for Helix creating later

	void Start() {
		CreateCircle();
	}

	void CreateCircle() {
		ParticleSystem PS = GetComponent<ParticleSystem>();//connect to the particle system
		var vel = PS.velocityOverLifetime;//connect to the velocity over lifetime curves
		vel.enabled = true;//enable the velocity over lifetime if it hasn't
		vel.space = ParticleSystemSimulationSpace.Local;//set the particle space to local 

		PS.startSpeed = 0f;//set the speed to 0
		vel.z = new ParticleSystem.MinMaxCurve(10.0f, Zvalue); //set the Z-Axis value


		AnimationCurve curveX = new AnimationCurve();//create a new curve
		for (int i = 0; i < resolution; i++)//loop though the amount of resolution
		{
			float newtime = (i/(resolution-1)); //calculate the time where the point is set
			float myvalue = Mathf.Sin(newtime * 2 * Mathf.PI);//depending on the time calculate the value of the point
		
			curveX.AddKey(newtime,myvalue);//add the calculated point to the curve
		}
		vel.x = new ParticleSystem.MinMaxCurve(10.0f, curveX); //now create the actual curve (10.0f is the value multiplier)

		AnimationCurve curveY = new AnimationCurve();
		for (int i = 0; i < resolution; i++)
		{
			float newtime = (i/(resolution-1));
			float myvalue = Mathf.Cos(newtime * 2 * Mathf.PI);
			
			curveY.AddKey(newtime,myvalue);
		}
		vel.y = new ParticleSystem.MinMaxCurve(10.0f, curveY);	
	}

	void Update() {
		//Force rotation to 0,0,0,0
		transform.rotation = Quaternion.identity;
	}
}
