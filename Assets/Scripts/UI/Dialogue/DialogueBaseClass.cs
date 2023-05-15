using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace DialogueSystem
{
    public class DialogueBaseClass : MonoBehaviour
    {
        public bool finished {get; private set;}

        protected IEnumerator WriteText(string input, TextMeshProUGUI textHolder, Color color, float delay, AudioClip sound, AudioClip startSound, int step){
            textHolder.color = color;
            SoundManager.instance.PlaySound(startSound);
            for(int i=0;i<input.Length;i++){
                textHolder.text += input[i];
                if((i+1)%step==0){
                    SoundManager.instance.PlaySound(sound);
                }
                yield return new WaitForSeconds(delay); 
            }
            yield return new WaitUntil(() => (Input.GetMouseButton(0) || Input.GetKey("return") || Input.GetKey("enter")));
            
            finished = true;
        }
    }
}
