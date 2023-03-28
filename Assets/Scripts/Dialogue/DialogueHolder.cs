using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace DialogueSystem
{
    public class DialogueHolder : MonoBehaviour
    {
        public bool finished {get;private set;}
        private void Awake(){
            StartCoroutine(dialogueSequence());
        }
        private IEnumerator dialogueSequence(){
            for(int i=0; i<transform.childCount;i++){
                Deactivate();
                transform.GetChild(i).gameObject.SetActive(true);
                yield return new WaitUntil(() => transform.GetChild(i).GetComponent<DialogueLine>().finished);
            }
            gameObject.SetActive(false);
            finished = true;
        }
        private void Deactivate(){
            for(int i=0; i<transform.childCount;i++){
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }

}
