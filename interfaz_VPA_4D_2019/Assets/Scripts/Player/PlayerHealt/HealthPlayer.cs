using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthPlayer : MonoBehaviour
{
    public int startingHealth = 100; //VidaInicial
    public int CurrentHealth; //Nivel De energia 
    public Slider HealthSlider; //SliderdeVida
    public Image damageImagen; //PantallaRoja
    public float flashSpeed = 5f; //VelocidadDeColor
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f); //ColorDeLaPantalla

    [SerializeField]bool isDead;
    bool damaged;

    // Update is called once per frame
    //void Update()
    //{
    //    if (damaged)
    //    {
    //        damageImagen.color = flashColour;
    //    }
    //    else
    //    {
    //        damageImagen.color = Color.Lerp(damageImagen.color, Color.clear, flashSpeed);

    //    }

    //    damaged = false;
    //}

    public void TakeDamage(int amount)
    {
        damaged = true;

        CurrentHealth -= amount;

        HealthSlider.value = CurrentHealth;

        if (CurrentHealth <= 0 && !isDead)
        {
            Death();
        }
        else
        {
            //Player.Instance.DamagePlayer();
        }
    }

    void Death()
    {
        isDead = true;
        //Player.Instance.DeathPlayer();
    }
}
