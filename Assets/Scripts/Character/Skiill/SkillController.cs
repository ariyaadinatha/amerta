using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using stateEnum;

public class SkillController : MonoBehaviour
{
    public KeyCode skillKey = KeyCode.E;

    private sacrificeState state;
    private PlayerMovement playerMovement;
    private SmallMessageController smallMessage;
    [SerializeField] private AudioClip useSkillSFX;
    private float cooldown;
    private bool usedSkill;
    const float cooldownTime = 10f;
    private GameObject[] invisibleObjects;
    private Animator skillUIAnimator;
    private GameObject questPointer;
    private RectTransform cooldownUI;
    private AudioSource audioSkillSource;

    private void Awake()
    {
        smallMessage = GameObject.FindWithTag("SmallMessage").GetComponent<SmallMessageController>();
        state = sacrificeState.EAR;
        invisibleObjects = GameObject.FindGameObjectsWithTag("Invisible");
        cooldownUI = GameObject.FindWithTag("SkillCooldown").GetComponent<RectTransform>();
        audioSkillSource = transform.Find("SkillUI").gameObject.GetComponent<AudioSource>();
        audioSkillSource.loop = false;
        audioSkillSource.clip = useSkillSFX;
        audioSkillSource.volume = 0.3f;

        setInvisibleObjects(false);
        questPointer = GameObject.FindWithTag("QuestPointer");
        if(questPointer != null) questPointer.SetActive(false);

        cooldown = cooldownTime;
        usedSkill = false;
        playerMovement = GetComponent<PlayerMovement>();
        GameObject skillUI = GameObject.Find("SkillUI");
        skillUIAnimator = skillUI.GetComponent<Animator>();
    }
    private void Update()
    {
        if (!playerMovement.InDialogue() && !playerMovement.InTimeline())
        {
            if (Input.GetKeyDown(skillKey))
            {
                if (CheckCooldown())
                {
                    UseSkill();
                }
                else
                {
                    smallMessage.Show("Skill on cooldown");
                }
            }
        }
    }

    private void LateUpdate()
    {
        if (usedSkill && CheckCooldown())
        {
            usedSkill = false;
            ReverseSkill();
        }
        if (cooldown <= cooldownTime)
        {
            cooldown += Time.deltaTime;
            float tmpCalc = 1 - (cooldown / cooldownTime);
            cooldownUI.localScale = new Vector3(tmpCalc, 1f, 1f);
        }
        else
        {
            cooldownUI.localScale = new Vector3(0f, 1f, 1f);
        }
    }

    private bool CheckCooldown()
    {
        return cooldown >= cooldownTime;
    }

    public void SwitchState(sacrificeState nextState)
    {
        ReverseSkill();
        state = nextState;
        cooldown = cooldownTime;
    }

    private void UseSkill()
    {
        SoundManager.instance.PlaySound(useSkillSFX);
        if (state == sacrificeState.EYE)
        {
            setInvisibleObjects(true);
        }
        else if(state == sacrificeState.MOUTH)
        {
            skillUIAnimator.SetTrigger("SkillUsedMouth");
        }
        else if(state == sacrificeState.EAR)
        {
            if (questPointer != null) questPointer.SetActive(true);
        }
        usedSkill = true;
        cooldown = 0f;
    }

    private void ReverseSkill()
    {
        if (state == sacrificeState.EYE)
        {
            setInvisibleObjects(false);
        }
        else if (state == sacrificeState.EAR)
        {
            if (questPointer != null) questPointer.SetActive(false);
        }
    }

    private void setInvisibleObjects(bool active)
    {
        for (int i = 0; i < invisibleObjects.Length; i++)
        {
            invisibleObjects[i].SetActive(active);
        }
    }
}
