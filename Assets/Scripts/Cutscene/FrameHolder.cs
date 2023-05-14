using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SceneSystem
{
    public class FrameHolder : MonoBehaviour
    {
        public bool finished { get; private set; }
        [Header("Delay Options")]
        [SerializeField] private float startDelay;
        [SerializeField] private float endDelay;


        [Header("Audio Options")]
        [SerializeField] private AudioClip startingSound;

        private Animator anim;

        private void Awake()
        {
            StartCoroutine(frameSequence());
            anim = gameObject.GetComponent<Animator>();
        }
        private IEnumerator frameSequence()
        {
            yield return new WaitForSeconds(startDelay);
            if(startingSound != null)
            {
                SoundManager.instance.PlaySound(startingSound);
                yield return new WaitForSeconds(startingSound.length);
            }
            for (int i = 0; i < transform.childCount; i++)
            {
                Deactivate();
                transform.GetChild(i).gameObject.SetActive(true);
                yield return new WaitUntil(() => transform.GetChild(i).GetComponent<SceneLine>().finished);
            }
            CloseFrame();
            yield return new WaitForSeconds(endDelay);
            gameObject.SetActive(false);
            finished = true;
        }
        private void Deactivate()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }
        private bool CloseFrame()
        {
            Deactivate();
            if (anim != null)
            {
                anim.SetTrigger("isFinished1_2");
            }
            return true;
        }
    }
}
