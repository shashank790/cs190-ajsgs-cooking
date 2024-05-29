using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;

public class BreadSlicer : MonoBehaviour
{
    public Material crossSectionMaterial; // Material to use for the cut surfaces
    public GameObject knife; // Reference to the knife object

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == knife)
        {
            Debug.Log("Knife has collided with the bread.");
            // Perform the slice
            SliceBread();
        }
    }

    private void SliceBread()
    {
        // Get the plane of the slice (you might need to adjust this depending on your setup)
        Vector3 knifeDirection = knife.transform.up; // Assuming the knife cuts along its up axis
        Vector3 knifePosition = knife.transform.position;

        // Debugging information
        Debug.Log($"Knife Position: {knifePosition}");
        Debug.Log($"Knife Direction: {knifeDirection}");

        // Use EzySlice to slice the object
        SlicedHull slicedHull = gameObject.Slice(knifePosition, knifeDirection, crossSectionMaterial);

        if (slicedHull != null)
        {
            Debug.Log("SlicedHull is not null, creating sliced objects.");

            // Create the sliced halves
            GameObject upperHull = slicedHull.CreateUpperHull(gameObject, crossSectionMaterial);
            GameObject lowerHull = slicedHull.CreateLowerHull(gameObject, crossSectionMaterial);

            // Set the position and rotation of the halves
            upperHull.transform.position = transform.position;
            lowerHull.transform.position = transform.position;

            upperHull.transform.rotation = transform.rotation;
            lowerHull.transform.rotation = transform.rotation;

            // Add necessary components to the sliced halves
            AddComponentsToHull(upperHull);
            AddComponentsToHull(lowerHull);

            // Destroy the original object
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("SlicedHull is null, slicing failed.");
        }
    }

    private void AddComponentsToHull(GameObject hull)
    {
        // Add a MeshCollider
        MeshCollider meshCollider = hull.AddComponent<MeshCollider>();
        meshCollider.convex = true; // Convex is necessary for rigidbodies

        // Add a Rigidbody
        Rigidbody rb = hull.AddComponent<Rigidbody>();
        rb.mass = 1f; // Adjust mass as necessary

        // Optionally, add any other components you need, such as scripts for further interactions
    }
}
