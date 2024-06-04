using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;
using UnityEngine.XR.Interaction.Toolkit;

public class BreadSlicer : MonoBehaviour
{
    public Material crossSectionMaterial; // Material to use for the cut surfaces
    public GameObject knife; // Reference to the knife object
    public Transform choppingBoard; // Reference to the chopping board
    public float smoothingFactor = 0.1f; // Smoothing factor for position and rotation
    public AudioSource slicingAudioSource; // Reference to the AudioSource component

    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private bool shouldTransformWithBoard = true;

    private bool isSliced = false;

    private void Start()
    {
        if (choppingBoard != null)
        {
            initialPosition = choppingBoard.InverseTransformPoint(transform.position);
            initialRotation = Quaternion.Inverse(choppingBoard.rotation) * transform.rotation;
        }

        if (slicingAudioSource != null)
        {
            slicingAudioSource.loop = true; // Ensure the audio source is set to loop
        }
    }

    private void Update()
    {
        if (choppingBoard != null && !isSliced)
        {
            float boardXRotation = choppingBoard.eulerAngles.x;
            shouldTransformWithBoard = !((boardXRotation >= 20f && boardXRotation <= 180f) || (boardXRotation >= 340f));

            if (shouldTransformWithBoard)
            {
                Vector3 targetPosition = choppingBoard.TransformPoint(initialPosition);
                Quaternion targetRotation = choppingBoard.rotation * initialRotation;

                // Smoothly interpolate position and rotation
                transform.position = Vector3.Lerp(transform.position, targetPosition, smoothingFactor);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, smoothingFactor);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == knife && !isSliced)
        {
            Debug.Log("Knife has collided with the bread.");
            // Start playing the slicing sound
            if (slicingAudioSource != null && !slicingAudioSource.isPlaying)
            {
                slicingAudioSource.Play();
            }
            // Perform the slice
            SliceBread();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == knife)
        {
            Debug.Log("Knife has exited the bread.");
            // Stop playing the slicing sound
            if (slicingAudioSource != null && slicingAudioSource.isPlaying)
            {
                slicingAudioSource.Stop();
            }
        }
    }
    private void SliceBread()
    {
        // Mark as sliced to prevent further slicing
        isSliced = true;

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
            //Destroy(gameObject);

            // Disable the original object's mesh and collider
            MeshRenderer originalMeshRenderer = GetComponent<MeshRenderer>();
            if (originalMeshRenderer != null)
            {
                originalMeshRenderer.enabled = false;
            }

            Collider originalCollider = GetComponent<Collider>();
            if (originalCollider != null)
            {
                originalCollider.enabled = false;
            }
        }
        else
        {
            Debug.Log("SlicedHull is null, slicing failed.");
            isSliced = false; // Reset the sliced flag if slicing failed
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

        // Add the XRGrabInteractable component
        XRGrabInteractable grabInteractable = hull.AddComponent<XRGrabInteractable>();

        // Add the BreadSlicer script to allow further slicing
        BreadSlicer breadSlicer = hull.AddComponent<BreadSlicer>();
        breadSlicer.crossSectionMaterial = crossSectionMaterial;
        breadSlicer.knife = knife;
        breadSlicer.choppingBoard = choppingBoard; // Pass the chopping board reference
        breadSlicer.slicingAudioSource = slicingAudioSource; // Pass the AudioSource reference

        // Store the initial transformation relative to the chopping board
        breadSlicer.initialPosition = choppingBoard.InverseTransformPoint(hull.transform.position);
        breadSlicer.initialRotation = Quaternion.Inverse(choppingBoard.rotation) * hull.transform.rotation;
    }
}
