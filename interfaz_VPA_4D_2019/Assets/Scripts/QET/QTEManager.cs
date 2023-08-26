using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QTEManager : MonoBehaviour
{
    [Header("Configuration")]
    [HideInInspector]
    public bool startEvent;
    public QTEEvent eventData;
    public bool isFail;
    public bool isEnded;
    public bool isCorrect;
    public float smoothTimeUpdate;
    float currentTime;

    protected void Update()
    {
        if (!startEvent || eventData == null || ScenesManager.Instance.IsPaused) 
            return;

        updateTimer();

        if (isCorrect || isFail)
        {
            doFinally();
        }
    }

    public void StartEvent(QTEEvent eventScriptable)
    {
        eventData = eventScriptable;

        if (eventData.onStart != null)
        {
            eventData.onStart.Invoke();
        }

        isEnded = false;
        isFail = false;
        isCorrect = false;
        currentTime = eventData.time;
        smoothTimeUpdate = currentTime;
        setupGUI();
        StartCoroutine(countDown());
    }

    private IEnumerator countDown()
    {
        startEvent = true;
        while (currentTime > 0 && startEvent && !isEnded)
        {
            if (StatesManager.Instance.uiController.eventTimerText != null)
            {
                StatesManager.Instance.uiController.eventTimerText.text = currentTime.ToString();
            }
                currentTime --;
                yield return new WaitForSeconds(1f);
        }

        if (!isEnded)
        {
            isFail = true;
        }
    }

    public void doFinally()
    {
        isEnded = true;

        if (StatesManager.Instance.uiController.eventUI != null)
        {
            StatesManager.Instance.uiController.eventUI.SetActive(false);
        }

        if (eventData == null)
            return;

        if (eventData.onEnd != null)
        {
            eventData.onEnd.Invoke();
        }

        if (eventData.onFail != null && isFail)
        {
            eventData.onFail.Invoke();
        }

        if (eventData.onSuccess != null && isCorrect)
        {
            eventData.onSuccess.Invoke();
        }
        
        startEvent = false;
        eventData = null;
    }

    public void SetResult(bool state)
    {
        if (!state)
        {
            isFail = true;
        }
        else
        {
            isCorrect = true;
        }
    }

    protected void updateTimer()
    {
        smoothTimeUpdate -= Time.unscaledDeltaTime;

        if (StatesManager.Instance.uiController.eventTimerImage != null)
        {
            StatesManager.Instance.uiController.eventTimerImage.fillAmount = smoothTimeUpdate / eventData.time;
        }
    }

    protected void setupGUI()
    {
        if (StatesManager.Instance.uiController.eventTimerImage != null)
        {
            StatesManager.Instance.uiController.eventTimerImage.fillAmount = 1;
        }
    }
}
