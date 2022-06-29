using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public HealthPlayer health;
    

    // Start is called before the first frame update
    void Start()
    {
        health = GetComponent<HealthPlayer>();
    }
}
