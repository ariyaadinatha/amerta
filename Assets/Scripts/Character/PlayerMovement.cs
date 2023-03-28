using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueSystem;

public class PlayerMovement : MonoBehaviour
{

    private float horizontal;
    private float speed = 15f;
    private float jumpingPower = 30f;
    private bool isFacingRight = true;
    private DialogueTrigger dialogueTrigger;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private GameObject blindness;

    [SerializeField] private AudioClip normalTrack;
    [SerializeField] private AudioClip deafTrack;
    private AudioSource audioSource;
    private bool isPlayingNormalTrack = false;


    void Start()
    {
        blindness.SetActive(false);
        isPlayingNormalTrack = false;
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = deafTrack;
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if(!InDialogue()){
            horizontal = Input.GetAxisRaw("Horizontal");
            if (Input.GetButtonDown("Jump") && IsGrounded())
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            }

            if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            }

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                blindness.SetActive(!blindness.activeSelf);
                if(!isPlayingNormalTrack){
                    audioSource.clip = normalTrack;
                    isPlayingNormalTrack = true;
                    audioSource.Play();
                }
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                if (isPlayingNormalTrack)
                {
                    audioSource.clip = deafTrack;
                    isPlayingNormalTrack = false;
                }
                else
                {
                    audioSource.clip = normalTrack;
                    isPlayingNormalTrack = true;
                }

                audioSource.Play();
                blindness.SetActive(false);
            }

            Flip();
        }
        
    }

    
    private void FixedUpdate()
    {
        if(dialogueTrigger!=null && !dialogueTrigger.IsDialogueFinished()){  
            rb.velocity = new Vector2(0,0);
        }
        else rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
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
    private bool InDialogue(){
        if(dialogueTrigger!=null){
            return dialogueTrigger.IsDialogueActive();
        }
        else return false;
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
}
