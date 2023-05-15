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
        [SerializeField] private PhysicsMaterial2D noSlip;
        [SerializeField] private PhysicsMaterial2D slip;
        private Collider2D coll;
        private void Awake(){
            coll = GameObject.FindWithTag("Player").GetComponent<Collider2D>();
            StartCoroutine(dialogueSequence());
        }
        private IEnumerator dialogueSequence(){
            coll.sharedMaterial = noSlip;
            for(int i=0; i<transform.childCount;i++){
                Deactivate();
                transform.GetChild(i).gameObject.SetActive(true);
                yield return new WaitUntil(() => transform.GetChild(i).GetComponent<DialogueLine>().finished);
            }
            finished = true;
            if (timeline != null)
            {
                timeline.Play();
            }
            coll.sharedMaterial = slip;
            gameObject.SetActive(false);
        }
        private void Deactivate(){
            for(int i=0; i<transform.childCount;i++){
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }

}
