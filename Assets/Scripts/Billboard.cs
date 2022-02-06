using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    [SerializeField] Transform _cam;

    // Update is called once per frame
    void LateUpdate()
    {
        transform.LookAt(transform.position + _cam.forward);
    }
}
