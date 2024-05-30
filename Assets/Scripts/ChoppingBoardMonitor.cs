using UnityEngine;
using System;

public class ChoppingBoardMonitor : MonoBehaviour
{
    public static event Action OnChoppingBoardTilted;

    private Vector3 lastCheckedPosition;
    private Quaternion lastCheckedRotation;

    private void Start()
    {
        lastCheckedPosition = transform.position;
        lastCheckedRotation = transform.rotation;
    }

    private void FixedUpdate()
    {
        if (transform.position != lastCheckedPosition || transform.rotation != lastCheckedRotation)
        {
            lastCheckedPosition = transform.position;
            lastCheckedRotation = transform.rotation;

            float boardXRotation = transform.eulerAngles.x;
            if (boardXRotation >= 20f || boardXRotation <= 340f)
            {
                OnChoppingBoardTilted?.Invoke();
            }
        }
    }
}
