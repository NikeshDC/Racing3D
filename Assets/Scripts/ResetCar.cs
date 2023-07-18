using UnityEngine;

public class ResetCar : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform spawnCameraTransform;
    [SerializeField] private GameObject car;

    public void Reset()
    {
        Rigidbody carRB = car.GetComponent<Rigidbody>();
        carRB.velocity = Vector3.zero;
        carRB.angularVelocity = Vector3.zero;
        carRB.isKinematic = true;
        Camera.main.transform.position = spawnCameraTransform.position;
        Camera.main.transform.rotation = spawnCameraTransform.rotation;
        car.transform.position = spawnPoint.position;
        car.transform.rotation = spawnPoint.rotation;
        Physics.SyncTransforms();
        carRB.isKinematic = false;
        car.GetComponent<CarController>().Reset();
    }
}
