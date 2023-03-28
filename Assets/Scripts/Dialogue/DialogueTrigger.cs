using UnityEngine;

namespace DialogueSystem{
    public class DialogueTrigger : MonoBehaviour
    {
        [SerializeField] private GameObject dialogue;
        
        public bool IsDialogueFinished(){
            return dialogue.gameObject.GetComponent<DialogueHolder>().finished;
        }

        public void ActivateDialogue(){
            dialogue.SetActive(true);
        }

        public bool IsDialogueActive(){
            return dialogue.activeInHierarchy;
        }
    }
}