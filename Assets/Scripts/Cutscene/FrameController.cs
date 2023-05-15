using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SceneSystem
{
    public class FrameController : MonoBehaviour
    {
        public float delaySceneOut;
        public Animator anim;
        public string sceneTujuan;
        private void Awake()
        {
            StartCoroutine(sceneSequence());
        }
        private IEnumerator sceneSequence()
        {
            anim.SetTrigger("FadeIn");
            for (int i = 0; i < transform.childCount; i++)
            {
                Deactivate();
                transform.GetChild(i).gameObject.SetActive(true);
                yield return new WaitUntil(() => transform.GetChild(i).GetComponent<FrameHolder>().finished);
            }
            anim.SetTrigger("FadeOut");
            yield return new WaitForSeconds(delaySceneOut);
            if(sceneTujuan == null || sceneTujuan == "")
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            else
            {
                SceneManager.LoadScene(sceneTujuan);
            }

        }
        private void Deactivate()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
}

