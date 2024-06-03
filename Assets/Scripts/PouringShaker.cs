using UnityEngine;

public class PouringShaker : MonoBehaviour
{
    public ParticleSystem saltParticleSystem; // Reference to the particle system
    public Transform shakerEnd; // Reference to the end of the shaker
    public float pourAngleThreshold = 45f; // Angle threshold to start pouring

    private ParticleSystem.EmissionModule emissionModule;

    void Start()
    {
        if (saltParticleSystem == null)
        {
            Debug.LogError("Salt particle system not assigned!");
            return;
        }

        // Get the emission module of the particle system
        emissionModule = saltParticleSystem.emission;
        emissionModule.enabled = false; // Start with emission disabled
    }

    void Update()
    {
        // Calculate the angle of the shaker
        float angle = Vector3.Angle(Vector3.down, transform.up);

        // Check if the angle exceeds the threshold
        if (angle < pourAngleThreshold)
        {
            // Enable particle emission
            emissionModule.enabled = true;
        }
        else
        {
            // Disable particle emission
            emissionModule.enabled = false;
        }

        // Update the position of the particle system to the end of the shaker
        saltParticleSystem.transform.position = shakerEnd.position;
        saltParticleSystem.transform.rotation = shakerEnd.rotation;
    }
}
