using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject followedObject;
    public Vector3 offset = new Vector3(0, -10, -3);

    private PlayerMovement playerMov;

    private void Awake()
    {
        playerMov = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
    }

    private void LateUpdate()
    {
        if (!playerMov.InDialogue() && !playerMov.InTimeline())
        {
            transform.position = followedObject.transform.position + offset;
        }
    }
}
