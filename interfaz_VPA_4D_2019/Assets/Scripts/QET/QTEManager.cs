using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QTEManager : MonoBehaviour
{
    [Header("Configuration")]
    public UI ui;
    [HideInInspector]
    public bool startEvent;
    public QTEEvent eventData;
    public bool isFail;
    public bool isEnded;
    public bool isCorrect;
    public float smoothTimeUpdate;
    float currentTime;

    private void Start()
    {
        ui = StatesManager.Instance.ui;
    }

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
        setupGUI(eventData.index);
        StartCoroutine(countDown());
    }

    private IEnumerator countDown()
    {
        startEvent = true;
        while (currentTime > 0 && startEvent && !isEnded)
        {
            if (ui.eventTimerText != null)
            {
                ui.eventTimerText.text = currentTime.ToString();
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

        if (ui.eventUI != null)
        {
            for (int i = 0; i < ui.eventUI.Length; i++)
            {
                ui.eventUI[i].SetActive(false);
            }
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

    void UISetup(int index)
    {
        for (int i = 0; i < ui.eventUI.Length; i++)
        {
            if (i == index)
            {
                ui.eventUI[i].SetActive(true);
            }
            else
            {
                ui.eventUI[i].SetActive(false);
            }
        }

        switch (index)
        {
            case 0:
                ui.eventTimerText = ui.eventUI[0].GetComponentInChildren<Text>();
                ui.eventTimerImage = ui.eventUI[0].GetComponentInChildren<Image>();
                break;

            case 1:
                ui.eventTimerText = ui.eventUI[1].GetComponentInChildren<Text>();
                ui.eventTimerImage = ui.eventUI[1].GetComponentInChildren<Image>();
                break;
        }
    }

    protected void updateTimer()
    {
        smoothTimeUpdate -= Time.unscaledDeltaTime;

        if (ui.eventTimerImage != null)
        {
            ui.eventTimerImage.fillAmount = smoothTimeUpdate / eventData.time;
        }
    }

    protected void setupGUI(int index)
    {
        UISetup(index);

        if (ui.eventTimerImage != null)
        {
            ui.eventTimerImage.fillAmount = 1;
        }
    }
}
