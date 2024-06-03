using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class StoveContact : MonoBehaviour
{
    public GameObject stoveOn; // Drag the stove GameObject here
    public XRBaseInteractor leftHandController; // Drag the Left Controller here
    public XRBaseInteractor rightHandController; // Drag the Right Controller here
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private XRInteractorLineVisual leftControllerLineVisual;
    private XRInteractorLineVisual rightControllerLineVisual;
    private Renderer stoveRenderer;
    private XRGrabInteractable stoveGrabInteractable;

    void Start()
    {
        // Ensure the stove is initially off (visually hidden)
        stoveRenderer = stoveOn.GetComponent<Renderer>();
        if (stoveRenderer != null)
        {
            stoveRenderer.enabled = false;
        }

        // Store the original position and rotation
        originalPosition = stoveOn.transform.position;
        originalRotation = stoveOn.transform.rotation;

        // Get the XRGrabInteractable component
        stoveGrabInteractable = stoveOn.GetComponent<XRGrabInteractable>();
        if (stoveGrabInteractable == null)
        {
            stoveGrabInteractable = stoveOn.AddComponent<XRGrabInteractable>();
        }

        // Subscribe to the select events
        stoveGrabInteractable.selectEntered.AddListener(OnSelectEntered);
        stoveGrabInteractable.selectExited.AddListener(OnSelectExited);

        // Add hover listeners
        stoveGrabInteractable.hoverEntered.AddListener(OnHover);
        stoveGrabInteractable.hoverExited.AddListener(OnHoverExit);

        // Get the line visuals
        leftControllerLineVisual = leftHandController.GetComponent<XRInteractorLineVisual>();
        rightControllerLineVisual = rightHandController.GetComponent<XRInteractorLineVisual>();
    }

    private void OnSelectEntered(SelectEnterEventArgs args)
    {
        // Ensure the interaction is with the stoveOn object
        if (args.interactableObject.transform == stoveOn.transform)
        {
            // Enable the stove (make it visible)
            if (stoveRenderer != null)
            {
                stoveRenderer.enabled = !stoveRenderer.enabled;
                // Reset the position and rotation
                stoveOn.transform.position = originalPosition;
                stoveOn.transform.rotation = originalRotation;
            }
        }
    }

    private void OnSelectExited(SelectExitEventArgs args)
    {
        // Ensure the interaction is with the stoveOn object
        if (args.interactableObject.transform == stoveOn.transform)
        {
            // Optionally, you could turn the stove off (make it invisible) when the object is released
            // if (stoveRenderer != null)
            // {
            //     stoveRenderer.enabled = false;
            // }
            // Reset the position and rotation
            stoveOn.transform.position = originalPosition;
            stoveOn.transform.rotation = originalRotation;
        }
    }

    private void OnHover(HoverEnterEventArgs args)
    {
        // Ensure the interaction is with the stoveOn object
        if (args.interactableObject.transform == stoveOn.transform)
        {
            // Enable the line interactor visual for the hovering controller
            if (args.interactorObject.transform == leftHandController.transform)
            {
                EnableLineVisual(leftHandController);
            }
            else if (args.interactorObject.transform == rightHandController.transform)
            {
                EnableLineVisual(rightHandController);
            }
        }
    }

    private void OnHoverExit(HoverExitEventArgs args)
    {
        // Ensure the interaction is with the stoveOn object
        if (args.interactableObject.transform == stoveOn.transform)
        {
            // Disable the line interactor visual for the controller that was hovering
            if (args.interactorObject.transform == leftHandController.transform)
            {
                DisableLineVisual(leftHandController);
            }
            else if (args.interactorObject.transform == rightHandController.transform)
            {
                DisableLineVisual(rightHandController);
            }
        }
    }

    private void EnableLineVisual(IXRInteractor interactor)
    {
        var lineVisual = interactor.transform.GetComponent<XRInteractorLineVisual>();
        if (lineVisual != null)
        {
            lineVisual.enabled = true;
        }
    }

    private void DisableLineVisual(IXRInteractor interactor)
    {
        var lineVisual = interactor.transform.GetComponent<XRInteractorLineVisual>();
        if (lineVisual != null)
        {
            lineVisual.enabled = false;
        }
    }

    void OnDestroy()
    {
        // Unsubscribe from the select events to prevent memory leaks
        if (stoveGrabInteractable != null)
        {
            stoveGrabInteractable.selectEntered.RemoveListener(OnSelectEntered);
            stoveGrabInteractable.selectExited.RemoveListener(OnSelectExited);
            stoveGrabInteractable.hoverEntered.RemoveListener(OnHover);
            stoveGrabInteractable.hoverExited.RemoveListener(OnHoverExit);
        }
    }
}
