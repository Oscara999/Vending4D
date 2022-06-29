using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllerSettings : MonoBehaviour
{
    [SerializeField]
    Slider sliderVoluemSettings;
    [SerializeField]
    Dropdown dropdownGrapichsSettings;
    int valueQuality = 3;

    void Start()
    {
        float valueVolumen;
        ManagerSound.Instance.audioMixer.GetFloat(Tags.VOLUMENMASTER_TAG, out valueVolumen);
        sliderVoluemSettings.value = valueVolumen;
        SetQualitySettings(valueQuality);
        Debug.Log(valueVolumen);
    }

    /// <summary>
    /// Método que permite cambiar por medio de dropdown la calidad de graficas.
    /// </summary>
    /// <param name="qualityIndex">Numero de calidad grafica segun ployect settings.</param>
    public void SetQualitySettings(int qualityIndex)
    {
        ScenesManager.Instance.SetQuality(qualityIndex);
    }

    /// <summary>
    /// Método que permite cambiar por medio de un slider el volumen actual.
    /// </summary>
    /// <param name="value">Numero de volumen que queremos cambiar.</param>
    public void SetVolumenSettings(float value)
    {
        ManagerSound.Instance.SetVolume(value);
    }

    /// <summary>
    /// Método que permite cambiar por medio de un botton la scena 
    /// </summary>
    /// <param name="levelName">nombre de escena que queremos cargar.</param>
    public void CallChangeScene(string levelName)
    {
        ScenesManager.Instance.LoadLevel(levelName);
    }

    /// <summary>
    /// Método que permite por medio de un botton salir del juego.
    /// </summary>
    public void CallQuit()
    {
        ScenesManager.Instance.Quit();
    }
}
