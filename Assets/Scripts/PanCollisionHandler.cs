using UnityEngine;
using System.Collections;

public class PanCollisionHandler : MonoBehaviour
{
    // Public variables to hold references to the objects
    public GameObject panObject;
    public GameObject panSoupObject;
    public GameObject tomatoObject;
    public GameObject carrotObject;
    public GameObject greenObject;

    // Objects to activate inside the bowl
    public GameObject bowlSoupObject;
    public GameObject bowlTomatoObject;
    public GameObject bowlCarrotObject;
    public GameObject bowlGreenObject;

    // Reference to the bowl
    public Transform bowlTransform;

    // Particle system for pouring effect
    public ParticleSystem pouringEffect;

    // Distance threshold for "collision"
    public float activationDistance = 0.5f; // Adjust as needed

    // Duration of the pouring effect
    public float pouringDuration = 2.0f; // Duration in seconds

    // AudioSource for pouring sound
    public AudioSource pouringAudioSource;
    private bool isPouring = false;

    void Start()
    {
        // Ensure the objects inside the bowl are initially inactive
        bowlSoupObject.SetActive(false);
        bowlTomatoObject.SetActive(false);
        bowlCarrotObject.SetActive(false);
        bowlGreenObject.SetActive(false);

        // Ensure the pouring effect is initially inactive
        if (pouringEffect != null)
        {
            pouringEffect.Stop();
        }
    }

    void Update()
    {
        // Check if the pan has the correct x rotation
        float boardXRotation = panObject.transform.eulerAngles.x;
        bool isBoardTilted = boardXRotation >= 20f || boardXRotation <= 340f;

        // Check the distance between the pan and the bowl
        if (isBoardTilted && IsPanNearBowl() && !isPouring)
        {
            Debug.Log("Pan and contents are near the bowl and tilted correctly.");
            StartCoroutine(PourForDuration(pouringDuration));
            StartCoroutine(FillSoup(bowlSoupObject, 0.5f));
            StartCoroutine(FillSoup(bowlTomatoObject, 0.5f));
            StartCoroutine(FillSoup(bowlCarrotObject, 0.5f));
            StartCoroutine(FillSoup(bowlGreenObject, 0.5f));
            panSoupObject.SetActive(false);
            tomatoObject.SetActive(false);
            carrotObject.SetActive(false);
            greenObject.SetActive(false);
        }
    }

    private bool IsPanNearBowl()
    {
        // Check distance between panObject and bowl
        if (Vector3.Distance(panObject.transform.position, bowlTransform.position) <= activationDistance)
        {
            return true;
        }

        // Check distance between panSoupObject and bowl
        if (Vector3.Distance(panSoupObject.transform.position, bowlTransform.position) <= activationDistance)
        {
            return true;
        }

        // Check distance between tomatoObject and bowl
        if (Vector3.Distance(tomatoObject.transform.position, bowlTransform.position) <= activationDistance)
        {
            return true;
        }

        // Check distance between carrotObject and bowl
        if (Vector3.Distance(carrotObject.transform.position, bowlTransform.position) <= activationDistance)
        {
            return true;
        }

        // Check distance between greenObject and bowl
        if (Vector3.Distance(greenObject.transform.position, bowlTransform.position) <= activationDistance)
        {
            return true;
        }

        return false;
    }

    private IEnumerator PourForDuration(float duration)
    {
        isPouring = true;

        if (pouringEffect != null)
        {
            pouringEffect.transform.position = panObject.transform.position;
            pouringEffect.transform.LookAt(bowlTransform);
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

        if (pouringEffect != null)
        {
            pouringEffect.Stop();
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
