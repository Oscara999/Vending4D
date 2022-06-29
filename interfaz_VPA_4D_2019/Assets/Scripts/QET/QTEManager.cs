using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QTEManager : Singleton<QTEManager>
{
    [Header("Configuration")]
    [HideInInspector]
    public bool isEventStarted;
    public QTEEvent eventData;
    public bool isFail;
    public bool isEnded;
    public bool isCorrect;
    public float currentTime;
    public float smoothTimeUpdate;

    protected void Update()
    {
        if (!isEventStarted || eventData == null || ScenesManager.Instance.IsPaused) return;

        updateTimer();

        if (isCorrect || isFail)
        {
            doFinally();
        }
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

    public void startEvent(QTEEvent eventScriptable)
    {
        eventData = eventScriptable;
        Debug.Log("1");

        if (eventData.onStart != null)
        {
            eventData.onStart.Invoke();
        }

        isEnded = false;
        isFail = false;
        isCorrect = false;
    
        currentTime = eventData.time;
        setupGUI();
        StartCoroutine(countDown());
    }

    private IEnumerator countDown()
    {
        isEventStarted = true;
        while(currentTime > 0 && isEventStarted && !isEnded)
        {
            if(eventData.keyboardUI.eventTimerText != null)
            {
                eventData.keyboardUI.eventTimerText.text = currentTime.ToString();
            }

            currentTime--;
            yield return new WaitForSecondsRealtime(1f);
        }

        if (!isEnded)
        {
            isFail = true;
            doFinally();
        }
    }

    protected void doFinally()
    {
        isEnded = true;
        isEventStarted = false;

        var ui = getUI();

        if (ui.eventUI != null)
        {
            ui.eventUI.SetActive(false);
        }

        if (eventData.onEnd != null)
        {
            eventData.onEnd.Invoke();
        }

        if (eventData.onFail != null && isFail)
        {
            eventData.onFail.Invoke();
        }

        if(eventData.onSuccess != null && isCorrect)
        {
            eventData.onSuccess.Invoke();
        }

        eventData = null;
    }

    protected void updateTimer()
    {
        var ui = getUI();

        if (ui.eventTimerImage != null)
        {
            ui.eventTimerImage.fillAmount = smoothTimeUpdate / eventData.time;
        }
    }

    protected void setupGUI()
    {
        var ui = getUI();
        
        if (ui.eventTimerImage != null)
        {
            ui.eventTimerImage.fillAmount = 1;
        }

        if (ui.eventUI != null)
        {
            ui.eventUI.SetActive(true);
        }
    }

    protected QTEUI getUI()
    {
        var ui = eventData.keyboardUI;
        return ui;
    }
}
