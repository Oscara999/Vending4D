using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReposeState : State
{
    public IntroductionState introductionState;
    public GameObject demoPanel;

    public override State Tick()
    {
        if (!StatesManager.Instance.isThereSomeone)
        {
            Check();
            return this;
        }
        else
        {
            ManagerSound.Instance.PlayNewSound("BackGroundMainManu");
            demoPanel.SetActive(false);
            StartCoroutine(StatesManager.Instance.uIController.StartTimer());
            return introductionState;
        }

    }

    void Check()
    {
        Debug.Log(20);
        if (!demoPanel.activeInHierarchy)
        {
            demoPanel.SetActive(true);
        }
        //validar el sensor de que alguien entra a la maquina
        //mostrar videos 
        //Segregar olor
        //Camiar color leds
        // activar letrero de precio

    }
}
