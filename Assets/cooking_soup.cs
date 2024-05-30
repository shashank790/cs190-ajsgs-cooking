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
    public GameObject choppingBoard;

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
        // Check if the chopping board has the correct x rotation
        float boardXRotation = choppingBoard.transform.eulerAngles.x;
        bool isBoardTilted = boardXRotation >= 20f || boardXRotation <= 340f;

        if (collision.gameObject == soupCollisionObject && isBoardTilted)
        {
            Debug.Log("I GOT MF SOUP");
            SoupObject.SetActive(true);
            canvasOff.SetActive(false);
            canvasOn.SetActive(true);
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
}
