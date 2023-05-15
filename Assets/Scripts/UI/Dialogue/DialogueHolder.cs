using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;

namespace DialogueSystem
{
    public class DialogueHolder : MonoBehaviour
    {
        public bool finished {get;private set;}
        public PlayableDirector timeline;
        private void Awake(){
            StartCoroutine(dialogueSequence());
        }
        private IEnumerator dialogueSequence(){
            for(int i=0; i<transform.childCount;i++){
                Deactivate();
                transform.GetChild(i).gameObject.SetActive(true);
                yield return new WaitUntil(() => transform.GetChild(i).GetComponent<DialogueLine>().finished);
            }
            finished = true;
            if (timeline != null)
            {
                Debug.Log("jalan");
                timeline.Play();
            }
            gameObject.SetActive(false);
        }
        private void Deactivate(){
            for(int i=0; i<transform.childCount;i++){
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }

}
