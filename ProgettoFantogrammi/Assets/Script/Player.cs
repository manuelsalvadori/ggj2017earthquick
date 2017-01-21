using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
	public float movementSpeed;

    public GameManager gm;

	public bool canPulse;
	public float pulseCooldown = 3;

	public float shockwaveIntesity = 1;
	public float shockwaveDuration = 1;
	public float shockwaveRange = 1;
	public float shockwavebounceTime = 1;
	public float shockwaveRippleOffset = 1;
	public float shockwaveRippleFrequency = 1;

	private float currentPulseCooldown = 0;
	private Material material;

	void Start ()
	{
	}

	public void Pulse (Vector3 pos)
	{
		canPulse = false;
		currentPulseCooldown = pulseCooldown;

		foreach (Collider collider in  Physics.OverlapSphere(transform.position, shockwaveRange)) {
			Deformer deformer = collider.gameObject.GetComponent<Deformer> ();

			if (deformer != null) {
				deformer.AddShockwave (
					new Shockwave (pos, shockwaveIntesity,
						shockwaveDuration, shockwaveRange, shockwavebounceTime, shockwaveRippleOffset, shockwaveRippleFrequency));
			}
		}
	}

	void Update ()
	{
		float emissiveIntensity = 1 - (currentPulseCooldown / pulseCooldown);
		//material.SetColor ("_EmissionColor", new Color (emissiveIntensity, emissiveIntensity, emissiveIntensity));

		if (currentPulseCooldown <= 0) {
			canPulse = true;
		} else {
			currentPulseCooldown -= Time.deltaTime;
		}


	}
}
