using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selected : MonoBehaviour
{
    public GameObject target;
    public  Ray ray;
    void Update()
    {
        if (target != null)
        {
            if (target.activeInHierarchy)
            {
                ray = Player.Instance.movingController.positionReferenceCamera.ScreenPointToRay(StatesManager.Instance.ui.PointOnScreen.transform.position);
                Vector3 ScreenXy = Player.Instance.movingController.positionReferenceCamera.WorldToScreenPoint(target.transform.position);
                StatesManager.Instance.ui.PointOnScreen.transform.position = new Vector3(ScreenXy.x, ScreenXy.y, StatesManager.Instance.ui.PointOnScreen.transform.position.z);
                Debug.DrawRay(ray.origin, ray.direction * 30f, Color.blue); 
            }
            else
            {
                target = null;
            }
        }
        else
        {
            StatesManager.Instance.ui.PointOnScreen.transform.position = Vector3.zero;
        }
    }

    public void SetSelectedObject(GameObject target)
    {
        this.target = target;
    }
}
