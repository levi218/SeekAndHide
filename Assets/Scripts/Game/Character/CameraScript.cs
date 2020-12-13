using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject following;
    public float clampRate = 0.1f;

    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, following.transform.position+Vector3.back*10,clampRate);
    }
}
