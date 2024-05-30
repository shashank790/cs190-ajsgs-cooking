using UnityEngine;

public class VegetableManager : MonoBehaviour
{
    public GameObject choppingBoard;
    public GameObject vegetable; // Reference to the vegetable game object

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == vegetable)
        {
            // Make the vegetable a child of the chopping board
            collision.transform.SetParent(choppingBoard.transform);
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject == vegetable)
        {
            // Optionally: Remove the vegetable as a child of the chopping board
            collision.transform.SetParent(null);
        }
    }
}
