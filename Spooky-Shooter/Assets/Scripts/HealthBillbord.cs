using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBillbord : MonoBehaviour
{
    [SerializeField] private Camera cam;

    private void Awake()
    {
        cam = Camera.main;
    }

    void LateUpdate()
    {
        transform.LookAt(transform.position + cam.transform.forward);
    }
}
