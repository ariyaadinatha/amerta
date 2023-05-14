using UnityEngine;

namespace DialogueSystem{
    public class DialogueTrigger : MonoBehaviour
    {
        [SerializeField] private GameObject dialogue;
        private bool done;
        
        public bool IsDialogueFinished(){
            return dialogue.gameObject.GetComponent<DialogueHolder>().finished;
        }

        public void ActivateDialogue(){
            dialogue.SetActive(true);
            done = true;
        }

        public bool IsDialogueActive(){
            return dialogue.activeInHierarchy;
        }

        public bool IsTriggered()
        {
            return done;
        }
    }
}