using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using stateEnum;
using UnityEngine.UI;

public class SacrificeStateController : MonoBehaviour
{
    public Image imageHolder;
    [SerializeField] private GameObject blindness;
    private SmallMessageController smallMessage;

    [SerializeField] private AudioClip normalTrack;
    [SerializeField] private AudioClip deafTrack;

    [SerializeField] private AudioClip switchStateSFX;

    [Header("State Image")]
    [SerializeField] private Sprite deafState;
    [SerializeField] private Sprite blindState;
    [SerializeField] private Sprite muteState;



    private AudioSource audioSource;
    private bool isPlayingNormalTrack = false;
    private sacrificeState state;
    private float cooldown;
    private PlayerMovement playerMovement;
    private SkillController skillController;
    const float cooldownTime = 10f;

    // Start is called before the first frame update
    void Awake()
    {
        smallMessage = GameObject.FindWithTag("SmallMessage").GetComponent<SmallMessageController>();
        playerMovement = GetComponent<PlayerMovement>();
        skillController = GetComponent<SkillController>();
        blindness.SetActive(false);
        isPlayingNormalTrack = false;
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = deafTrack;
        audioSource.Play();
        state = sacrificeState.EAR;
        cooldown = cooldownTime;
        imageHolder.sprite = deafState;
        imageHolder.preserveAspect = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerMovement.InDialogue())
        {
            if (state != sacrificeState.EYE && Input.GetKeyDown(KeyCode.Alpha1))
            {
                SwitchSacrificeState(sacrificeState.EYE);
            }
            else if (state != sacrificeState.EAR && Input.GetKeyDown(KeyCode.Alpha2))
            {
                SwitchSacrificeState(sacrificeState.EAR);
            }
            else if (state != sacrificeState.MOUTH && Input.GetKeyDown(KeyCode.Alpha3))
            {
                SwitchSacrificeState(sacrificeState.MOUTH);
            }
        }
    }
    private void LateUpdate()
    {
        cooldown += Time.deltaTime;
    }

    public sacrificeState getState()
    {
        return state;
    }

    public bool CheckCooldown()
    {
        return cooldown >= cooldownTime;
    }

    private void SwitchSacrificeState(sacrificeState nextState)
    {
        if (CheckCooldown())
        {
            SoundManager.instance.PlaySound(switchStateSFX);
            if (state == sacrificeState.EAR)
            {
                audioSource.clip = normalTrack;
                isPlayingNormalTrack = true;
                audioSource.Play();
            }
            else if (state == sacrificeState.EYE)
            {
                blindness.SetActive(false);
            }

            if (nextState == sacrificeState.EAR)
            {
                imageHolder.sprite = deafState;
                audioSource.clip = deafTrack;
                isPlayingNormalTrack = false;
                audioSource.Play();
            }
            else if (nextState == sacrificeState.EYE)
            {
                imageHolder.sprite = blindState;
                blindness.SetActive(true);
            }
            else if (nextState == sacrificeState.MOUTH)
            {
                imageHolder.sprite = muteState;
            }

            state = nextState;
            skillController.SwitchState(nextState);
            cooldown = 0f;
        }
        else
        {
            smallMessage.Show("State is on cooldown");
        }
    }

    public void SwitchState(int state)
    {
        sacrificeState nextState;
        if(state == 0)
        {
            nextState = sacrificeState.EAR;
        }
        else if (state == 1)
        {
            nextState = sacrificeState.EYE;
        }
        else if (state == 2)
        {
            nextState = sacrificeState.MOUTH;
        }
        else
        {
            nextState = sacrificeState.MOUTH;
        }
        SwitchSacrificeState(nextState);
    }
}
