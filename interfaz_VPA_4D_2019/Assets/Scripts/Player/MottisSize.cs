 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MottisSize : MonoBehaviour
{
    public float speed;
    public float tamaño;

    public void StartChangeSize()
    {
        StartCoroutine(ChangeSize());
    }

    IEnumerator ChangeSize()
    {
        while (transform.localScale.x < tamaño)
        {
            transform.localScale += new Vector3(Time.deltaTime * speed, Time.deltaTime * speed, Time.deltaTime * speed);
            yield return null;
        }

        Debug.Log("Mottis starts to Disminuir.");
    }
}
