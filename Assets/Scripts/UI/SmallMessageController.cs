using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SmallMessageController : MonoBehaviour
{
    private AudioSource audioSource;
    private Animator animator;
    private TextMeshProUGUI text;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        text = GetComponent<TextMeshProUGUI>();
        text.text = "";

    }
    public void Show(string message="")
    {
        text.text = message;
        animator.SetTrigger("show");
        audioSource.Play();
    }

}
