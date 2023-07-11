using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraZoom : MonoBehaviour
{
    private Camera mCamera;
    float zoomFactor = 1f;
    public float fieldOfViewMax = 50;
    public float fieldOfViewMin = 10;

    private float previousBetweenFingerDistance;  //holds distance between two fingers used for zooming in previous frame


    private void Start()
    {
        mCamera = GetComponent<Camera>();
    }

    private void Update()
    {
        if (Input.touchCount == 2)
        {
            Touch touch1 = Input.GetTouch(0);
            Touch touch2 = Input.GetTouch(1);
            if(touch1.phase == TouchPhase.Began || touch2.phase == TouchPhase.Began)
                previousBetweenFingerDistance = (touch1.position - touch2.position).magnitude;
            else
                HandleCameraZoom(touch1, touch2);
        }
    }
    private void HandleCameraZoom(Touch touch1, Touch touch2)
    {
        float currentBetweenFingerDistance = (touch1.position - touch2.position).magnitude;
        float deltaFingerDistance = previousBetweenFingerDistance - currentBetweenFingerDistance;
        float targetFieldOfView;

        if (deltaFingerDistance > 0)
        {
            targetFieldOfView = fieldOfViewMax;
        }
        else
        {
            targetFieldOfView = fieldOfViewMin;
        }

        mCamera.fieldOfView = Mathf.Lerp(mCamera.fieldOfView, targetFieldOfView, Time.deltaTime * Mathf.Abs(deltaFingerDistance) * zoomFactor);

        previousBetweenFingerDistance = currentBetweenFingerDistance;
    }
}
