using UnityEngine;

public class cooking_soup : MonoBehaviour
{
    // Public variables to hold references to the objects
    public GameObject SoupObject;
    public GameObject canvasOff;
    public GameObject canvasOn;
    public GameObject tomatosObject;
    public GameObject carrotsObject;
    public GameObject greensObject;

    // Objects to check for collisions
    public GameObject soupCollisionObject;
    public GameObject carrotsCollisionObject;
    public GameObject tomatosCollisionObject;
    public GameObject greensCollisionObject;

    void Start()
    {
        // Ensure the objects are initially inactive
        SoupObject.SetActive(false);
        tomatosObject.SetActive(false);
        carrotsObject.SetActive(false);
        greensObject.SetActive(false);
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if the colliding object is the soup object
        if (collision.gameObject == soupCollisionObject)
        {
            Debug.Log("I GOT MF SOUP");
            SoupObject.SetActive(true);
            canvasOff.SetActive(false);
            canvasOn.SetActive(true);
        }

        // Check if the colliding object is the carrots object
        if (collision.gameObject == carrotsCollisionObject)
        {
            Debug.Log("carrot");
            carrotsObject.SetActive(true);
        }

        // Check if the colliding object is the tomatos object
        if (collision.gameObject == tomatosCollisionObject)
        {
            Debug.Log("tomato");

            tomatosObject.SetActive(true);
        }

        // Check if the colliding object is the greens object
        if (collision.gameObject == greensCollisionObject)
        {
            Debug.Log("green");
            greensObject.SetActive(true);
        }
    }
}
