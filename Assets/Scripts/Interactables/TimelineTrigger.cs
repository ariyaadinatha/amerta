using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineTrigger : MonoBehaviour
{
    [SerializeField] private PlayableDirector timeline;
    private PlayerMovement playerMov;
    private bool done;

    private void Awake()
    {
        playerMov = GameObject.FindWithTag("Player").gameObject.GetComponent<PlayerMovement>();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!done && other.tag == "Player")
        {
            done = true;
            playerMov.isOnTimeline = true;
            timeline.Play();
        }
    }
}
