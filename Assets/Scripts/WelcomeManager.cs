using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class WelcomeManager : MonoBehaviour
{
    public GameObject welcomeCanvas; // Drag the WelcomeCanvas here
    public GameObject nextObj; // Drag the next here
    public Button continueButton; // Drag the ContinueButton here
    public Transform vrCamera; // Drag the VR camera here
    public XRBaseInteractor leftController; // Drag the Left Controller here
    public XRBaseInteractor rightController; // Drag the Right Controller here
    public Vector3 desiredPosition = new Vector3(0.45f, 1f, 1.075f); // Set your desired position here
    public Vector3 desiredScale = new Vector3(0.002f, 0.002f, 0.002f); // Set your desired scale here
    public Color hoverColor = Color.red; // Set your desired hover color here
    private Color originalColor;
    private Image continueButtonImage;
    private XRInteractorLineVisual leftLineVisual;
    private XRInteractorLineVisual rightLineVisual;

    void Start()
    {
        // Ensure the welcomeCanvas is active at the start
        welcomeCanvas.SetActive(true);
        //continueButton.gameObject.SetActive(true);
        nextObj.SetActive(false);

        // Position and scale the Canvas in front of the VR camera
        PositionCanvasInFrontOfCamera();

        // Add listener to the button
        continueButton.onClick.AddListener(OnContinueButtonClicked);

        // Ensure the button is interactable
        var interactable = continueButton.GetComponent<XRGrabInteractable>();
        if (interactable == null)
        {
            interactable = continueButton.gameObject.AddComponent<XRGrabInteractable>();
        }
        interactable.retainTransformParent = false; // Disable retain transform parent to avoid warnings

        // Add interaction listeners
        leftController.selectEntered.AddListener(OnSelectEnter);
        rightController.selectEntered.AddListener(OnSelectEnter);

        // Add hover listeners
        interactable.hoverEntered.AddListener(OnHoverEnter);
        interactable.hoverExited.AddListener(OnHoverExit);

        // Get the original color of the button
        continueButtonImage = continueButton.GetComponent<Image>();
        if (continueButtonImage != null)
        {
            originalColor = continueButtonImage.color;
        }
        // Get the XRInteractorLineVisual components
        leftLineVisual = leftController.GetComponent<XRInteractorLineVisual>();
        rightLineVisual = rightController.GetComponent<XRInteractorLineVisual>();
    }

    void PositionCanvasInFrontOfCamera()
    {
        if (vrCamera != null && welcomeCanvas != null)
        {
            // Set the position of the canvas to the desired position
            welcomeCanvas.transform.position = desiredPosition;

            // Set the scale of the canvas
            welcomeCanvas.transform.localScale = desiredScale;

            // Ensure the canvas is facing the camera
            Vector3 direction = (welcomeCanvas.transform.position - vrCamera.position).normalized;
        }
    }

    void OnContinueButtonClicked()
    {
        // Hide the welcomeCanvas when the button is clicked
        welcomeCanvas.SetActive(false);
        // Disable the button's collider to prevent it from being interactable
        var buttonCollider = continueButton.GetComponent<Collider>();
        if (buttonCollider != null)
        {
            buttonCollider.enabled = false;
        }
        nextObj.SetActive(true);
    }

    private void OnSelectEnter(SelectEnterEventArgs args)
    {
        if (args.interactableObject.transform == continueButton.transform)
        {
            OnContinueButtonClicked();
        }
    }

    private void OnHoverEnter(HoverEnterEventArgs args)
    {
        if (continueButtonImage != null)
        {
            continueButtonImage.color = hoverColor;
        }
        // Enable the line interactor visual for the hovering controller
        if (args.interactorObject.transform == leftController.transform)
        {
            EnableLineInteractorVisual(leftController);
        }
        else if (args.interactorObject.transform == rightController.transform)
        {
            EnableLineInteractorVisual(rightController);
        }
    }

    private void OnHoverExit(HoverExitEventArgs args)
    {
        if (continueButtonImage != null)
        {
            continueButtonImage.color = originalColor;
        }
        // Disable the line interactor visual for the controller that was hovering
        if (args.interactorObject.transform == leftController.transform)
        {
            DisableLineInteractorVisual(leftController);
        }
        else if (args.interactorObject.transform == rightController.transform)
        {
            DisableLineInteractorVisual(rightController);
        }
    }

    private void EnableLineInteractorVisual(XRBaseInteractor controller)
    {
        var lineVisual = controller.GetComponent<XRInteractorLineVisual>();
        if (lineVisual != null)
        {
            lineVisual.enabled = true;
        }
    }

    private void DisableLineInteractorVisual(XRBaseInteractor controller)
    {
        var lineVisual = controller.GetComponent<XRInteractorLineVisual>();
        if (lineVisual != null)
        {
            lineVisual.enabled = false;
        }
    }
}