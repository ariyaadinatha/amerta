using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueSystem;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    public float speed = 20f;
    public float jumpingPower = 45f;
    private bool isFacingRight = true;
    private DialogueTrigger dialogueTrigger;
    private float gravScaleInit;
    private Animator anim;
    float checking;
    bool down;
    public bool isOnTimeline;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    void Awake()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("Run", false);
    }

    void Start()
    {
        gravScaleInit = rb.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (!InDialogue() && !isOnTimeline){
            horizontal = Input.GetAxisRaw("Horizontal");
            if (Input.GetButtonDown("Jump") && IsGrounded())
            {
                /*Debug.Log("jump yeah");*/
                anim.SetTrigger("Jump");

                rb.AddForce(transform.up * jumpingPower, ForceMode2D.Impulse);
            }
        }
        if (rb.velocity.y < -100f)
        {
            rb.velocity = new Vector2(rb.velocity.x, -100f);
        }
        if (rb.velocity.y >= 10f)
        {
            rb.gravityScale = gravScaleInit;
        }
        else
        {
            rb.gravityScale = gravScaleInit * 1.5f;
        }
        if (rb.velocity.y < -5 && !down)
        {
            Debug.Log(rb.velocity.y);
            down = true;
            anim.SetTrigger("Down");
        }
        
        if(down == true && IsGrounded())
        {
            down = false;
            anim.SetTrigger("Land");
        }
    }
    
    private void FixedUpdate()
    {
        if (dialogueTrigger != null && !dialogueTrigger.IsDialogueFinished())
        {
            anim.SetBool("Run", false);
            rb.velocity = new Vector2(0, -100);
        }
        else rb.velocity = new Vector2(horizontal * speed * 1.3f, rb.velocity.y);
        if (rb.velocity.x == 0)
        {
            anim.SetBool("Run", false);
        }
        else
        {
            anim.SetBool("Run", true);
        }
        Flip();
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f) 
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    //For dialogue scene
    public bool InDialogue(){
        if(dialogueTrigger!=null){
            return dialogueTrigger.IsDialogueActive();
        }
        else return false;
    }

    public bool InTimeline()
    {
        return isOnTimeline;
    }
    
    private void OnTriggerStay2D(Collider2D collision){
        if(collision.gameObject.tag == "DialogueTrigger"){
            dialogueTrigger = collision.gameObject.GetComponent<DialogueTrigger>();
            if(!dialogueTrigger.IsDialogueFinished()){
                dialogueTrigger.ActivateDialogue();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision){
        dialogueTrigger = null;
    }
    
    public void SwitchIsOnTimeline(bool state)
    {
        isOnTimeline = state;
    }
}
