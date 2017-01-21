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

    private float currentPulseCooldown = 0;
    private Material material;

    void Awake()
    {
        material = GetComponent<Renderer>().material;
    }

    void Start()
    {
    }

    public void Pulse()
    {
        canPulse = false;
        currentPulseCooldown = pulseCooldown;

        foreach(RaycastHit hit in Physics.SphereCastAll(transform.position, shockwaveRange, Vector3.forward))
        {           
            Deformer deformer = hit.collider.gameObject.GetComponent<Deformer>();
            if(deformer != null)
            {
                deformer.AddShockwave(transform.position, shockwaveIntesity, shockwaveDuration, shockwaveRange);
            }
        }           
    }

    void Update()
    {
        float emissiveIntensity = 1 - (currentPulseCooldown / pulseCooldown);
        material.SetColor("_EmissionColor", new Color(emissiveIntensity, emissiveIntensity, emissiveIntensity));

        if(currentPulseCooldown <= 0)
        {
            canPulse = true;
        }
        else
        {
            currentPulseCooldown -= Time.deltaTime;
        }

        if(gm.hitted && canPulse)
        {
            gm.hitted = false;
            Pulse();
        }
    }
}
