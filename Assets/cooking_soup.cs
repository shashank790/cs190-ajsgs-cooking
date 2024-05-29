using UnityEngine;

public class cooking_soup : MonoBehaviour
{
    // Public variable to hold the GameObject that should appear after collision
    public GameObject objectToAppear;

    void Start()
    {
        // Ensure the object is initially inactive
        if (objectToAppear != null)
        {
            objectToAppear.SetActive(false);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if the colliding object has the tag "Soup"
        if (collision.gameObject.CompareTag("Soup"))
        {
            Debug.Log("Collided with Soup!");
            // Activate the object that was initially hidden
            if (objectToAppear != null)
            {
                objectToAppear.SetActive(true);
            }
        }
    }
}