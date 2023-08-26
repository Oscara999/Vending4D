using Leap.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movimiento_UI_Control_Juego : MonoBehaviour
{
    [Header("Selected Settings")] 
    [SerializeField] GameObject currentTarget;
    [SerializeField] Color[] colors;
    [SerializeField] Image SelectedUI;
    [SerializeField] float speed;
    public Selected selected;
    public Camera positionReferenceCamera;

    public void Selected()
    {
        if (!StatesManager.Instance.uiController.crossFire.activeInHierarchy)
            return;

        RaycastHit hit;

        if (Physics.Raycast(StatesManager.Instance.inputController.ray, out hit))
        {
            var selection = hit.transform;

            if (selection.CompareTag(Tags.SELECTABLE_TAG))
            {
                EnemyBullet bullet = hit.transform.GetComponent<EnemyBullet>();

                if (currentTarget == null)
                {
                    EditBulletSelected(bullet, 2, true);
                    currentTarget = bullet.gameObject;
                    selected.SetSelectedObject(currentTarget);
                }
                else if (currentTarget != null && bullet.selected)
                {
                    return;
                }
                else if (currentTarget != null && !bullet.selected)
                {
                    EditBulletSelected(currentTarget.GetComponent<EnemyBullet>(), 2, false);
                    EditBulletSelected(bullet, 2, true);
                    currentTarget = bullet.gameObject;
                    selected.SetSelectedObject(currentTarget);
                }
            }

            if (selection.CompareTag(Tags.SELECTABLEATTACK_TAG))
            {
                if (Enemy.Instance.enemySelected.selectTime < 0)
                {
                    Enemy.Instance.enemySelected.SetSelected();
                }
                else
                {
                    Enemy.Instance.enemySelected.selectTime -= Time.fixedDeltaTime;
                }
            }
        }

        if (currentTarget != null)
        {
            if (!currentTarget.activeInHierarchy)
            {
                currentTarget = null;
            }
        }
    }

    IEnumerator ChangeColor()
    {
        SelectedUI.color = colors[1];
        yield return new WaitForSeconds(1f);
        SelectedUI.color = colors[0];
    }

    void EditBulletSelected(EnemyBullet bulletEnemy, int index, bool state)
    {
        SoundManager.Instance.PlayNewSound(Player.Instance.fx_Sound[index].name);
        //StartCoroutine(ChangeColor());
        bulletEnemy.uiSelected.SetActive(state);
        bulletEnemy.selected = state;
    }

    public Vector3 CurrentObjetivoPosition()
    {
        if (currentTarget != null)
        {
            return selected.ray.direction;
        }
        else
        {
            return StatesManager.Instance.inputController.ray.direction;
        }
    }

}