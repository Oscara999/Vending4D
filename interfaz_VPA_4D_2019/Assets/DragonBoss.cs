using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonBoss : Singleton<DragonBoss>
{
    [SerializeField]
    State currentState;

    // Start is called before the first frame update
    void Start()
    {
        
    }
}
