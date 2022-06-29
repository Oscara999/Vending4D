using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


[System.Serializable]
public class QTEUI
{
    public GameObject eventUI;
    public Text eventTimerText;
    public Image eventTimerImage;
}

public enum QTETimeType
{
    Normal,
    Slow,
    Paused
}

public class QTEEvent : MonoBehaviour
{
    [Header("Event settings")]
    public float time = 3f;
    [Header("UI")]
    public QTEUI keyboardUI;

    [Header("Event actions")]
    public UnityEvent onStart;
    public UnityEvent onEnd;
    public UnityEvent onSuccess;
    public UnityEvent onFail;
}
