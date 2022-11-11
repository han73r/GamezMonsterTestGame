using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinObjects : MonoBehaviour
{
    public float spinSpeed;

    void Update()
    {
        transform.Rotate(Vector3.up, spinSpeed * Time.deltaTime);
    }
}
