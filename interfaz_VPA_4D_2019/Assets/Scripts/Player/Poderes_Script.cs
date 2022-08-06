using System.Collections.Generic;
using UnityEngine;

public class Poderes_Script : MonoBehaviour
{
    [SerializeField] private GameObject poder_invierno;
    private GameObject Jerarquia_poderes;
    [SerializeField] List<GameObject> lista_poderes = new List<GameObject>();
  

    void Start()
    {
        Jerarquia_poderes = new GameObject();
        Jerarquia_poderes.name = "Poderes";
    }

    public void crear_esfera()
    {
        GameObject sfra = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sfra.transform.position = new Vector3(0,10,0);
        sfra.AddComponent<Rigidbody>();
    }

    public void poder_de_invierno()
    {
        if (Player.Instance)
        {
            Player.Instance.Attack();
            Debug.Log("emuso");
        }

        //if (Player.Instance.IsActivate)
        //{
        //    GameObject poder = Instantiate(poder_invierno, StatesManager.Instance.uIController.ray.origin /*mano_derecha.GetPalmPosition()*/, Quaternion.identity);
        //    poder.transform.parent = Jerarquia_poderes.transform;
        //    lista_poderes.Add(poder);
        //} 
    }
}
