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
    public bool finish;

    public void Update()
    {
        if (rotate)
        {
            transform.Rotate(0, speed * Time.deltaTime, 0);
        }
    }

    public void ChangeSize(bool validation)
    {
        finish = false;
        StopAllCoroutines();
        sprite.gameObject.SetActive(true);
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
        sprite.transform.localScale =  Vector3.zero;
        sprite.gameObject.SetActive(false);
        return true;
    }

    IEnumerator SmallSize()
    {
        while (sprite.transform.localScale.x > 0)
        {
            sprite.transform.localScale += new Vector3(-Time.deltaTime * speed, -Time.deltaTime * speed);
            yield return null;
        }
        finish = true;
    }

    IEnumerator BigSize()
    {
        while (sprite.transform.localScale.x < tamaño)
        {
            sprite.transform.localScale += new Vector3(Time.deltaTime * speed, Time.deltaTime * speed);
            yield return null;
        }
        finish = true;
    }

}
