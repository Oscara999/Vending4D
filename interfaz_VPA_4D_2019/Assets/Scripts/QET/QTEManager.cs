using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QTEManager : Singleton<QTEManager>
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

    public GameObject eventUIPanel;
    public GameObject[] eventUI;
    public Text eventTimerText;
    public Image eventTimerImage;

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
            if (eventTimerText != null)
            {
                eventTimerText.text = currentTime.ToString();
            }

            currentTime--;
            yield return new WaitForSecondsRealtime(1f);
        }

        if (!isEnded)
        {
            isFail = true;
        }
    }

    protected void doFinally()
    {
        isEnded = true;

        if (eventUI != null)
        {
            for (int i = 0; i < eventUI.Length; i++)
            {
                eventUI[i].SetActive(false);
            }
        }

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
        for (int i = 0; i < eventUI.Length; i++)
        {
            if (i == index)
            {
                eventUI[i].SetActive(true);
            }
            else
            {
                eventUI[i].SetActive(false);
            }
        }

        switch (index)
        {
            case 0:
                eventTimerText = eventUI[0].GetComponentInChildren<Text>();
                eventTimerImage= eventUI[0].GetComponentInChildren<Image>();
                break;

            case 1:
                eventTimerText = eventUI[1].GetComponentInChildren<Text>();
                eventTimerImage = eventUI[1].GetComponentInChildren<Image>();
                break;
        }
    }

    protected void updateTimer()
    {
        smoothTimeUpdate -= Time.unscaledDeltaTime;

        if (eventTimerImage != null)
        {
            eventTimerImage.fillAmount = smoothTimeUpdate / eventData.time;
        }
    }

    protected void setupGUI(int index)
    {
        UISetup(index);

        if (eventTimerImage != null)
        {
            eventTimerImage.fillAmount = 1;
        }
    }
}
