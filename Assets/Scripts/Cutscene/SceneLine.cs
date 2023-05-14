using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SceneSystem
{
    public class SceneLine : SceneBaseClass
    {
        [Header("Line Options")]
        private TextMeshProUGUI textHolder;
        [SerializeField] private string input;
        [SerializeField] private Color color;
        [SerializeField] private TMP_FontAsset font;

        private float delay;

        [Header("Audio Options")]
        [SerializeField] private AudioClip sound;
        [SerializeField] private AudioClip startSound;
        private int step;

        const float textShowTime = 0.75f;
        const float textTickTime = 0.03f;

        private void Awake()
        {
            if (input.Length <= 0) delay = 0f;
            else delay = textShowTime / input.Length;
            step = (int)(textTickTime / delay) + 1;
            textHolder = GameObject.Find("Text").GetComponent<TextMeshProUGUI>();
            textHolder.color = color;
            textHolder.font = font;
        }

        private void Start()
        {
            StartCoroutine(WriteText(input, textHolder, color, font, delay, sound, startSound, step));
        }
    }
}
