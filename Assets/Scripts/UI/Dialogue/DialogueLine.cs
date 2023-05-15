using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DialogueSystem
{
    public class DialogueLine : DialogueBaseClass
    {
        [Header ("Line Options")]
        private TextMeshProUGUI textHolder;
        [SerializeField] private TextMeshProUGUI nameHolder;
        [SerializeField] private string input;

        private float delay;
                
        [Header ("Audio Options")]
        [SerializeField] private AudioClip sound;
        [SerializeField] private AudioClip startSound;
        private int step;

        [Header ("Sprite Options")]
        [SerializeField] private Sprite characterSprite;
        [SerializeField] private string characterName;
        [SerializeField] private Image imageHolder;

        const float textShowTime = 0.75f;
        const float textTickTime = 0.03f;

        private void Awake()
        {
            if (input.Length <= 0) delay = 0f;
            else delay = textShowTime / input.Length;
            step = (int)(textTickTime / delay) + 1;
            textHolder = GetComponent<TextMeshProUGUI>();
            nameHolder.text = characterName;

            imageHolder.sprite = characterSprite;
            imageHolder.preserveAspect = true;
        }

        private void Start(){
            StartCoroutine(WriteText(input, textHolder, delay, sound, startSound, step));
        }
    }
}
