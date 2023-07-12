using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableComponentsWhileTargetIsMoving : MonoBehaviour
{
    public List<MonoBehaviour> disableComponents;  //disable these components while moving
    public List<MonoBehaviour> enableComponents;  //enable these componets only while moving
    //these components are mutually exclusive i.e. while one is enabled other is not

    public GameObject target;
    private Rigidbody targetRigidbody;

    private float movementSensitivity = 0.01f;

    // Start is called before the first frame update
    void Start()
    {
        targetRigidbody = target.GetComponent<Rigidbody>();
        StartCoroutine(CheckTarget());
    }

    IEnumerator CheckTarget()
    {
        while (true) 
        {
            if (targetRigidbody.velocity.magnitude > movementSensitivity)
                DisableComponents();
            else
                EnableComponents();
            yield return new WaitForSeconds(0.01f);
        }
    }

    private void EnableComponents()
    {
        foreach(var component in disableComponents) 
            component.enabled = true;
        foreach (var component in enableComponents)
            component.enabled = false;
    }

    private void DisableComponents()
    {
        foreach (var component in disableComponents)
            component.enabled = false;
        foreach (var component in enableComponents)
            component.enabled = true;
    }
}
