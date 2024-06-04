using UnityEngine;
using System.Collections;

public class cooking_soup : MonoBehaviour
{
    // Public variables to hold references to the objects
    public GameObject SoupObject;
    public GameObject canvasOff;
    public GameObject canvasOn;
    public GameObject tomatosObject;
    public GameObject carrotsObject;
    public GameObject greensObject;
    public GameObject pan;

    // Objects to check for collisions
    public GameObject soupCollisionObject;
    public GameObject handle;

    public GameObject carrotsCollisionObject;
    public GameObject tomatosCollisionObject;
    public GameObject greensCollisionObject;
    public GameObject choppingBoard;
    public GameObject spoon;
    private bool isStirring = false;
    // Particle system for pouring effect
    public ParticleSystem pouringEffect;
    // Duration of the pouring effect
    public float pouringDuration = 2.0f; // Duration in seconds
    private bool isPouring = false;
    public AudioSource pouringAudioSource;

    void Start()
    {
        // Ensure the objects are initially active
        SoupObject.SetActive(false);
        tomatosObject.SetActive(false);
        carrotsObject.SetActive(false);
        greensObject.SetActive(false);
        // Ensure the pouring effect is initially inactive
        if (pouringEffect != null)
        {
            pouringEffect.Stop();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if the chopping board has the correct x rotation
        float boardXRotation = choppingBoard.transform.eulerAngles.x;
        bool isBoardTilted = boardXRotation >= 20f || boardXRotation <= 340f;

        if (collision.gameObject == soupCollisionObject)
        {
            Debug.Log("I GOT MF SOUP");
            canvasOff.SetActive(false);
            canvasOn.SetActive(true);
            if (!isPouring)
            {
                StartCoroutine(PourForDuration(pouringDuration));
                StartCoroutine(FillSoup(SoupObject, 0.5f));
            }
        }

        if (collision.gameObject == carrotsCollisionObject && isBoardTilted)
        {
            Debug.Log("carrot");
            carrotsObject.SetActive(true);
        }

        if (collision.gameObject == tomatosCollisionObject && isBoardTilted)
        {
            Debug.Log("tomato");
            tomatosObject.SetActive(true);
        }

        if (collision.gameObject == greensCollisionObject && isBoardTilted)
        {
            Debug.Log("green");
            greensObject.SetActive(true);
        }

        if (collision.gameObject == choppingBoard && isBoardTilted)
        {
            Debug.Log("all");
            greensObject.SetActive(true);
            tomatosObject.SetActive(true);
            carrotsObject.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == spoon)
        {
            Debug.Log("Spoon has entered the pot.");
            isStirring = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == spoon)
        {
            Debug.Log("Spoon has exited the pot.");
            isStirring = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == spoon && isStirring)
        {
            StirContents();
        }
    }

    private void StirContents()
    {
        // Calculate circular motion
        float angle = Time.time * Mathf.PI * 2; // Continuous circular motion
        float x = Mathf.Cos(angle) * 0.0003f;
        float z = Mathf.Sin(angle) * 0.0003f;

        // Apply circular motion to the objects
        MoveInCircle(tomatosObject, x, z);
        MoveInCircle(carrotsObject, x, z);
        MoveInCircle(greensObject, x, z);
    }

    private void MoveInCircle(GameObject obj, float x, float z)
    {
        if (obj != null)
        {
            obj.transform.position += new Vector3(x, 0, z);
        }
    }

    private IEnumerator PourForDuration(float duration)
    {
        isPouring = true;

        if (pouringEffect != null)
        {
            pouringEffect.transform.position = handle.transform.position;
            pouringEffect.transform.LookAt(pan.transform);
            pouringEffect.Play();
        }

        // Play the pouring sound
        if (pouringAudioSource != null)
        {
            pouringAudioSource.Play();
        }

        yield return new WaitForSeconds(duration);

        if (pouringEffect != null)
        {
            pouringEffect.Stop();
        }

        // Stop the pouring sound
        if (pouringAudioSource != null)
        {
            pouringAudioSource.Stop();
        }
        
        isPouring = false;
    }

    private IEnumerator FillSoup(GameObject soupObject, float duration)
    {
        Debug.Log("Starting to fill the soup.");
        float elapsedTime = 0f;
        Vector3 initialScale = new Vector3(soupObject.transform.localScale.x, 0f, soupObject.transform.localScale.z);
        Vector3 targetScale = soupObject.transform.localScale;

        soupObject.transform.localScale = initialScale;
        soupObject.SetActive(true); // Make sure the soup object is active
        var renderer = soupObject.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.enabled = true; // Ensure the renderer is enabled
        }

        while (elapsedTime < duration)
        {
            soupObject.transform.localScale = Vector3.Lerp(initialScale, targetScale, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        soupObject.transform.localScale = targetScale;
        Debug.Log("Finished filling the soup.");
    }
}
