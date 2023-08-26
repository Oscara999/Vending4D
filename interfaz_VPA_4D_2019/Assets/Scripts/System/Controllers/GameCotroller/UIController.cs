using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [Header(" UI Settings")]
    public Canvas canvas;
    public GameObject[] DesingCursor;
    public TMP_Text coinsText;
    public GameObject eventUI;
    public Text eventTimerText;
    public Image eventTimerImage;
    public GameObject crossFire;
    public GameObject PointOnScreen;

    [Header(" UI Mottis Presentation")]
    public GameObject questPanel;
    public GameObject pausePanel;
    public GameObject rulesPanel;
    public GameObject valuePanel;

    [Header(" UI Game")]
    public Text timerPanel;
    public GameObject lifeEnemyObject;
    public GameObject gamePanel;
    public Slider sliderEnemyUI;
    public Image[] lifesUI;
    
    [Header("Settings CrossFire")]
    [SerializeField] private Vector2 boundaryMax;
    [SerializeField] private Vector2 boundaryMin;

    public void MoveCursor(Vector2 position)
    {
        if (!Camera.main)
            return;

        // Crear un nuevo vecor que almacene la posicion del virtual mouse
        Vector2 screenPoint = new Vector2(position.x, position.y);

        // Obtén las coordenadas de la esquina inferior izquierda de la pantalla en el mundo del juego
        Vector3 minWorldPoint = new Vector3(boundaryMin.x, boundaryMin.y);

        // Obtén las coordenadas de la esquina superior derecha de la pantalla en el mundo del juego
        Vector3 maxWorldPoint = new Vector3(Screen.width - boundaryMax.x, Screen.height - boundaryMax.y);

        // Aplica los límites de la pantalla al movimiento del puntero del mouse
        screenPoint.x = Mathf.Clamp(screenPoint.x, minWorldPoint.x, maxWorldPoint.x);
        screenPoint.y = Mathf.Clamp(screenPoint.y, minWorldPoint.y, maxWorldPoint.y);

        StatesManager.Instance.uiController.crossFire.transform.position = new Vector3(screenPoint.x, screenPoint.y, StatesManager.Instance.uiController.crossFire.transform.position.z);
    }


    public void CrossFireState(bool isSelected)
    {
        if (isSelected)
        {
            StatesManager.Instance.uiController.crossFire.SetActive(true);
        }
        else
        {
            StatesManager.Instance.uiController.crossFire.SetActive(false);
        }
    }
}
