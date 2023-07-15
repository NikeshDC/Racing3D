using UnityEngine;

public class ResetCar : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform spawnCameraTransform;
    [SerializeField] private GameObject car;

    public void Reset()
    {
        car.transform.position = spawnPoint.position;
        car.transform.rotation = spawnPoint.rotation;
        car.GetComponent<Rigidbody>().velocity = Vector3.zero;
        Camera.main.transform.position = spawnCameraTransform.position;
        Camera.main.transform.rotation = spawnCameraTransform.rotation;
        Physics.SyncTransforms();
    }
}
