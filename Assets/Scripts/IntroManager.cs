using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class IntroManager : MonoBehaviour
{   
    public GameObject welcomeCanvas; // Drag the WelcomeCanvas here
    public GameObject nextObject; // Drag the next object here
    public Button proceedButton; // Drag the ProceedButton here
    public Transform vrCamera; // Drag the VR camera here
    public XRBaseInteractor leftHandController; // Drag the Left Controller here
    public XRBaseInteractor rightHandController; // Drag the Right Controller here
    public Vector3 canvasPosition = new Vector3(0.45f, 1f, 1.075f); // Set your desired position here
    public Vector3 canvasScale = new Vector3(0.002f, 0.002f, 0.002f); // Set your desired scale here
    public Color highlightColor = Color.red; // Set your desired hover color here
    private Color defaultColor;
    private Image proceedButtonImage;
    private XRInteractorLineVisual leftControllerLineVisual;
    private XRInteractorLineVisual rightControllerLineVisual;

    void Start()
    {
        
        nextObject.SetActive(false);

        // Position and scale the Canvas in front of the VR camera
        PositionCanvas();

        // Add listener to the button
        proceedButton.onClick.AddListener(OnProceedButtonClicked);

        // Ensure the button is interactable
        var buttonInteractable = proceedButton.GetComponent<XRGrabInteractable>();
        if (buttonInteractable == null)
        {
            buttonInteractable = proceedButton.gameObject.AddComponent<XRGrabInteractable>();
        }
        buttonInteractable.retainTransformParent = false; // Disable retain transform parent to avoid warnings

        // Add interaction listeners
        leftHandController.selectEntered.AddListener(OnSelect);
        rightHandController.selectEntered.AddListener(OnSelect);

        // Add hover listeners
        buttonInteractable.hoverEntered.AddListener(OnHover);
        buttonInteractable.hoverExited.AddListener(OnHoverExit);

        // Get the original color of the button
        proceedButtonImage = proceedButton.GetComponent<Image>();
        if (proceedButtonImage != null)
        {
            defaultColor = proceedButtonImage.color;
        }
        // Get the XRInteractorLineVisual components
        leftControllerLineVisual = leftHandController.GetComponent<XRInteractorLineVisual>();
        rightControllerLineVisual = rightHandController.GetComponent<XRInteractorLineVisual>();
    }

    void PositionCanvas()
    {
        if (vrCamera != null && welcomeCanvas != null)
        {
            // Set the position of the canvas to the desired position
            welcomeCanvas.transform.position = canvasPosition;

            // Set the scale of the canvas
            welcomeCanvas.transform.localScale = canvasScale;

            // Ensure the canvas is facing the camera
            Vector3 direction = (welcomeCanvas.transform.position - vrCamera.position).normalized;
        }
    }

    void OnProceedButtonClicked()
    {
        // Hide the welcomeCanvas when the button is clicked
        welcomeCanvas.SetActive(false);
        // Disable the button's collider to prevent it from being interactable
        var buttonCollider = proceedButton.GetComponent<Collider>();
        if (buttonCollider != null)
        {
            buttonCollider.enabled = false;
        }
        nextObject.SetActive(true);
    }

    private void OnSelect(SelectEnterEventArgs args)
    {
        if (args.interactableObject.transform == proceedButton.transform)
        {
            OnProceedButtonClicked();
        }
    }

    private void OnHover(HoverEnterEventArgs args)
    {
        if (proceedButtonImage != null)
        {
            proceedButtonImage.color = highlightColor;
        }
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

    private void OnHoverExit(HoverExitEventArgs args)
    {
        if (proceedButtonImage != null)
        {
            proceedButtonImage.color = defaultColor;
        }
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

    private void EnableLineVisual(XRBaseInteractor controller)
    {
        var lineVisual = controller.GetComponent<XRInteractorLineVisual>();
        if (lineVisual != null)
        {
            lineVisual.enabled = true;
        }
    }

    private void DisableLineVisual(XRBaseInteractor controller)
    {
        var lineVisual = controller.GetComponent<XRInteractorLineVisual>();
        if (lineVisual != null)
        {
            lineVisual.enabled = false;
        }
    }
}
