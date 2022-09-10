using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySelected : MonoBehaviour
{
    public GameObject UIselected;
    public float selectTime;
    [SerializeField] float startTime;

    public void StartSelected()
    {
        Enemy.Instance.DamageQTE(false);
        selectTime = startTime;
        UIselected.SetActive(true);
    }

    public void FinishSelected()
    {
        selectTime = 0;
        UIselected.SetActive(false);
    }

    public void SetSelected()
    {
        Debug.Log("Selected");
        Enemy.Instance.DamageQTE(true);
        ManagerGame.Instance.QTEManager.SetResult(true);
    }
}
