using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject followedObject;
    public Vector3 offset = new Vector3(0, -10, -3);

    private void LateUpdate()
    {
        transform.position = followedObject.transform.position + offset;
    }
}
