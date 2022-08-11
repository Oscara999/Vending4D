using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : MonoBehaviour
{
    public float rotationSpeed = 1;
    public float BlastPower = 5;

    public GameObject Cannonball;
    public Transform ShotPoint;
    public float HorizontalRotation;
    public float VericalRotation;
    public GameObject Explosion;
    
    
    private void Update()
    {
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + 
            new Vector3(0, HorizontalRotation * rotationSpeed, VericalRotation * rotationSpeed));
    }

    public void StartExplocion()
    {
        GameObject CreatedCannonball = Instantiate(Cannonball, ShotPoint.position, ShotPoint.rotation);
        CreatedCannonball.GetComponent<Rigidbody>().velocity = ShotPoint.transform.up * BlastPower * 4;

        Debug.Log("entro");

        // Added explosion for added effect
        Destroy(Instantiate(Explosion, ShotPoint.position, ShotPoint.rotation), 2);
    }

}
