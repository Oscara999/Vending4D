using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : MonoBehaviour
{
    // Esta funcion determinara lo que el estado va a desarrollar "Update"
    public abstract State Tick();
    // Esta funcion determinara el final de este estado desarrollado y dara paso a un nuevo estado
    public abstract void ExitState();
}
