using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turn : MonoBehaviour
{
    public float speed;
    public bool isTurn;

    private void Start()
    {
        StartCoroutine(StartTurn());    
    }

    IEnumerator StartTurn()
    {
        yield return new WaitForSeconds(2f);
        isTurn = true;
    }

    void Update()
    {
        if (isTurn)
        {
            transform.Rotate(Vector3.forward, speed * Time.deltaTime);
        }
    }
}
