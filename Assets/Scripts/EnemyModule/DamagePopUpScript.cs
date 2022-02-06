using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePopUpScript : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 0.2f);
    }

    private void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * 1.5f);
    }
}
