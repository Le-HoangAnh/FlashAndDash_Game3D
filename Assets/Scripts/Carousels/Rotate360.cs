using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate360 : MonoBehaviour
{
    [SerializeField] float spped;

    private void Update()
    {
        transform.rotation = transform.rotation * Quaternion.AngleAxis(spped, Vector3.up);
    }
}
