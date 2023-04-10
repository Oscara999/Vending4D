using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Task : MonoBehaviour
{
    [SerializeField] float speed;
    public float tamaño;
    public Image sprite;
    public bool start;

    public void ChangeSize(bool validation)
    {
        start = true;

        if (!sprite.enabled)
        {
            sprite.enabled = true;
        }

        if (validation)
        {
            StartCoroutine(BigSize());
        }
        else
        {
            StartCoroutine(SmallSize());
        }
    }

    public void RestartSize(bool state)
    {
        sprite.enabled = false;

        if (state)
        {
            sprite.transform.localScale = new Vector3(tamaño, tamaño, 1);
        }
        else
        {
            sprite.transform.localScale = new Vector3(0, 0, 1);
        }

    }

    IEnumerator SmallSize()
    {        
        while (sprite.transform.localScale.x > 0)
        {
            sprite.transform.localScale += new Vector3(-Time.deltaTime * speed, -Time.deltaTime * speed);
            yield return null;
        }

        Debug.Log("The black window starts to Disminuir.");
        start = false;
    }

    IEnumerator BigSize()
    {
        while (sprite.transform.localScale.x < tamaño)
        {
            sprite.transform.localScale += new Vector3(Time.deltaTime * speed, Time.deltaTime * speed);
            yield return null;
        }

        Debug.Log("The black window starts to Grow");
        start = false;
    }
}
