using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateTrigger : MonoBehaviour
{
    public Collider2D player;
    public Animator gateAnimator;
    float timePushed;
    float timeSpent = 0;

    private void Update()
    {
        if(timePushed <= 0 && timeSpent>0)
        {
            timeSpent -= Time.deltaTime;
        }
        if(timeSpent < 0)
        {
            timeSpent = 0;
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == "Enemy" || collider.tag == "Player")
        {
            gateAnimator.Play("gateDown", -1, timeSpent / 3);
            timePushed = timeSpent;
            timeSpent = 0;
        }
    }
    
    private void OnTriggerStay2D(Collider2D collider)
    {
        if ((collider.tag == "Enemy" || collider.tag == "Player") && timePushed < 3)
        {
            timePushed += Time.deltaTime;
        }
        if(timePushed > 3)
        {
            timePushed = 3;
        }
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.tag == "Enemy" || collider.tag == "Player")
        {
            gateAnimator.Play("gateUp", 0,(1- timePushed/3));
            timeSpent = timePushed;
            timePushed = 0f;
        }
    }
}
