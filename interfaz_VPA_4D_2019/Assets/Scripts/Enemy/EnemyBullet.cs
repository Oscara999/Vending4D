using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public bool selected;
    public float speed;
    public GameObject uiSelected;
    public Vector3 targetPosition;
    public Vector3 targetLookPosition;

    void Start()
    {
        uiSelected.SetActive(false);
    }

    private void Update()
    {
        if (targetPosition == null || targetLookPosition == null)
            return;

        Vector3 move = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        transform.position = move;
        transform.LookAt(new Vector3(transform.rotation.x, Player.Instance.transform.position.y, transform.rotation.z));
    }

    public void SetLookTarget(Vector3 lookPosition)
    {
        targetLookPosition = lookPosition;
    }

    public void SetTarget(Vector3 position)
    {
        targetPosition = position;
    }
}
