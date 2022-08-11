using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Task : MonoBehaviour
{
    [SerializeField] float speed;
    public float tamaño;
    public Image sprite;
    public bool rotate;
    public bool start;

    public void Update()
    {
        if (rotate && start)
        {
            transform.Rotate(0, speed * Time.deltaTime, 0);
        }
    }

    public void ChangeSize(bool validation)
    {
        start = true;
        sprite.enabled = true;

        if (validation)
        {
            StartCoroutine(BigSize());
        }
        else
        {
            StartCoroutine(SmallSize());
        }
    }

    public bool RestartSize()
    {
        sprite.transform.localScale =  new Vector3(tamaño,tamaño,1);
        sprite.enabled = false;
        return true;
    }

    IEnumerator SmallSize()
    {
        while (sprite.transform.localScale.x > 0)
        {
            sprite.transform.localScale += new Vector3(-Time.deltaTime * speed, -Time.deltaTime * speed);
            yield return null;
        }

        Debug.Log("startBlackWindows");
        start = false;
    }

    IEnumerator BigSize()
    {
        while (sprite.transform.localScale.x < tamaño)
        {
            sprite.transform.localScale += new Vector3(Time.deltaTime * speed, Time.deltaTime * speed);
            yield return null;
        }

        start = false;
    }
}
