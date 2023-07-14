using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


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
    
    [Header("Event actions")]
    public UnityEvent onStart;
    public UnityEvent onEnd;
    public UnityEvent onSuccess;
    public UnityEvent onFail;
}
