using Leap;
using Leap.Unity;
using Microsoft.Win32.SafeHandles;
using System.Collections;
using System.Collections.Generic;
using System.Security.Principal;
using UnityEngine;

public class Poderes_Script : MonoBehaviour
{
    [SerializeField] private GameObject poder_invierno;
    [SerializeField] private GameObject mira;
    [SerializeField] private RiggedHand mano_derecha;
    private GameObject Jerarquia_poderes;
    [SerializeField] List<GameObject> lista_poderes = new List<GameObject>();
  

    void Start()
    {
        mano_derecha = GetComponent<RiggedHand>();
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
        if (Player.Instance.IsActivate)
        {
            GameObject poder = Instantiate(poder_invierno, Player.Instance.movimiento.ray.origin /*mano_derecha.GetPalmPosition()*/, Quaternion.identity);
            poder.transform.parent = Jerarquia_poderes.transform;
            lista_poderes.Add(poder);
        } 
    }
}
