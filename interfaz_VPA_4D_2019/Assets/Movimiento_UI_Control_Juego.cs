using Leap.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movimiento_UI_Control_Juego : MonoBehaviour
{
    [SerializeField] RiggedHand HandModelBase;
    [SerializeField] GameObject currentObjetivo;
    [SerializeField] Color[] colors;
    [SerializeField] Image UI;
    [SerializeField] float speed;
    public Vector3 ScreenXy;
    public kindScene kindScene;
    public Selected selected;
    public GameObject crossFire; 
    public Ray ray;

    public void CrossFireState(bool isSelected)
    {
        if (isSelected)
        {
            crossFire.SetActive(true);
        }
        else
        {
            crossFire.SetActive(false);
        }
    }

    public void Selected()
    {
        if (!crossFire.activeInHierarchy)
        {
            return;
        }

        RaycastHit hit;
        ray = Camera.main.ScreenPointToRay(crossFire.transform.position);

        //Vector3 handPostition = HandModelBase.GetPalmDirection();
        Vector3 handPostition = HandModelBase.GetPalmDirection();

        ScreenXy = Camera.main.WorldToScreenPoint(handPostition * speed);
        //crossFire.transform.position = new Vector3(ScreenXy.x, ScreenXy.y, crossFire.transform.position.z);

        Debug.DrawRay(ray.origin, ray.direction * 30f, Color.red);

        if (kindScene == kindScene.Menu)
            return;

        if (Physics.Raycast(ray, out hit))
        {
            var selection = hit.transform;

            if (selection.CompareTag(Tags.SELECTABLE_TAG))
            {
                EnemyBullet bullet = hit.transform.GetComponent<EnemyBullet>();

                if (currentObjetivo == null)
                {
                    EditBulletSelected(bullet, 2, true);
                    currentObjetivo = bullet.gameObject;
                    selected.SetSelectedObject(currentObjetivo);
                }
                else if (currentObjetivo != null && bullet.selected)
                {
                    return;
                }
                else if (currentObjetivo != null && !bullet.selected)
                {
                    EditBulletSelected(currentObjetivo.GetComponent<EnemyBullet>(), 2, false);
                    EditBulletSelected(bullet, 2, true);
                    currentObjetivo = bullet.gameObject;
                    selected.SetSelectedObject(currentObjetivo);
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

        if (currentObjetivo != null)
        {
            if (!currentObjetivo.activeInHierarchy)
            {
                currentObjetivo = null;
            }
        }
    }

    IEnumerator ChangeColor()
    {
        UI.color = colors[1];
        yield return new WaitForSeconds(1f);
        UI.color = colors[0];
    }

    void EditBulletSelected(EnemyBullet bulletEnemy, int index, bool state)
    {
        ManagerSound.Instance.PlayNewSound(Player.Instance.fx_Sound[index].name);
        StartCoroutine(ChangeColor());
        bulletEnemy.uiSelected.SetActive(state);
        bulletEnemy.selected = state;
    }

    public Vector3 CurrentObjetivoPosition()
    {
        if (currentObjetivo != null)
        {
            return selected.ray.direction;
        }
        else
        {
            return ray.direction;
        }
    }

}

public enum kindScene
{
   Menu, Game
}