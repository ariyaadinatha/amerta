using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DialogueSystem;

public class QuestPointerController : MonoBehaviour
{
    [System.Serializable]
    public class Quest
    {
        private Transform targetPosition;
        [SerializeField] private DialogueTrigger dialogueTrigger;
        [SerializeField] private GameObject pointer;
        private RectTransform pointerRectTransform;

        public void awake()
        {
            pointerRectTransform = pointer.GetComponent<RectTransform>();
            targetPosition = dialogueTrigger.transform;
        }

        public void update(Camera uiCamera, float borderSize)
        {
            if (dialogueTrigger.IsTriggered()) pointer.SetActive(false);
            else pointer.SetActive(true);
            Vector3 toPosition = targetPosition.position;
            Vector3 fromPosition = uiCamera.transform.position;
            fromPosition.z = 0f;
            Vector3 dir = (toPosition - fromPosition).normalized;
            dir = dir.normalized;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            angle += 90;
            if (angle < 0) angle += 360;
            if (angle > 360) angle -= 360;
            pointerRectTransform.localEulerAngles = new Vector3(0, 0, angle);

            Vector3 targetPositionScreenPoint = uiCamera.WorldToScreenPoint(targetPosition.position);

            Vector3 cappedTargetScreenPosition = targetPositionScreenPoint;
            if (cappedTargetScreenPosition.x <= borderSize) cappedTargetScreenPosition.x = borderSize;
            if (cappedTargetScreenPosition.x >= Screen.width - borderSize) cappedTargetScreenPosition.x = Screen.width - borderSize;
            if (cappedTargetScreenPosition.y <= borderSize) cappedTargetScreenPosition.y = borderSize;
            if (cappedTargetScreenPosition.y >= Screen.height - borderSize) cappedTargetScreenPosition.y = Screen.height - borderSize;

            pointerRectTransform.position = cappedTargetScreenPosition;
        }
    }

    private Camera uiCamera;
    [SerializeField] public Quest[] quest;


    const float borderSize = 100f;

    private void Awake()
    {
        uiCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        for (int i = 0; i < quest.Length; i++)
        {
            quest[i].awake();
        }
    }

    private void Update()
    {
        for (int i = 0; i < quest.Length; i++)
        {
            quest[i].update(uiCamera, borderSize);
        }
    }
}
