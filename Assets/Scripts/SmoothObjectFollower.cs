using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothObjectFollower : MonoBehaviour
{
    public Transform target;  //target to follow
    Vector3 offset;  //maintain this offset after following
    private Vector3 velocity = Vector3.zero;

    public float smoothFactor = 1f;

    private void OnEnable()
    {
       offset = target.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPosition = target.position - offset;
        float smoothTime = smoothFactor / (transform.position - targetPosition).magnitude;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
