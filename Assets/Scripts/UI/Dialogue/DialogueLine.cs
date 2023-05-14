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
        [SerializeField] private Color color;
        [SerializeField] private TMP_FontAsset font;

        [Header ("Time Options")]
        [SerializeField] private float delay;
        
        [Header ("Audio Options")]
        [SerializeField] private AudioClip sound;
        [SerializeField] private AudioClip startSound;
        [SerializeField] private int step;

        [Header ("Sprite Options")]
        [SerializeField] private Sprite characterSprite;
        [SerializeField] private string characterName;
        [SerializeField] private Image imageHolder;
        

        private void Awake(){
            textHolder = GetComponent<TextMeshProUGUI>();
            nameHolder.text = characterName;
            nameHolder.color = color;
            nameHolder.font = font;

            imageHolder.sprite = characterSprite;
            imageHolder.preserveAspect = true;
        }

        private void Start(){
            StartCoroutine(WriteText(input, textHolder, color, font, delay, sound, startSound, step));
        }
    }
}
