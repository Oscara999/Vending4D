using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selected : MonoBehaviour
{
    public GameObject crossFire;
    GameObject target;
    public  Ray ray;
    
    void Update()
    {
        if (target != null)
        {
            if (target.activeInHierarchy)
            {
                ray = Camera.main.ScreenPointToRay(crossFire.transform.position);
                Vector3 ScreenXy = Camera.main.WorldToScreenPoint(target.transform.position);
                crossFire.transform.position = new Vector3(ScreenXy.x, ScreenXy.y, crossFire.transform.position.z);
                Debug.DrawRay(ray.origin, ray.direction * 30f, Color.blue); 
            }
            else
            {
                target = null;
            }
        }
        else
        {
            crossFire.transform.position = Vector3.zero;
        }
    }

    public void SetSelectedObject(GameObject target)
    {
        this.target = target;
    }
}
