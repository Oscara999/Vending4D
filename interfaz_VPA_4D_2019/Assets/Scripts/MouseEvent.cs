using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System.Collections;

public class MouseEvent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Panel Settings")]
    public bool isActive;
    public bool isSelected;
    public Color normalColor;
    public Color selectedColor;
    public Button eventButton;
    public Image loadingImage;
    public Image panelReference;
    public Sprite normalPanelImage;
    public Sprite selectedPanelImage;
    public float currentTime;
    public float smoothTimeUpdate;
    public MouseEvent anotherButton;
    void StartSelect()
    {
        Debug.Log("StartSelected");
        loadingImage.gameObject.SetActive(true);
        smoothTimeUpdate = 0;
        loadingImage.fillAmount = 0;
        isActive = true;
    }

    void EndSelect()
    {
        Debug.Log("EndSelected");
        loadingImage.gameObject.SetActive(false);
        smoothTimeUpdate = 0;
        isActive = false;
    }

    void Update()
    {
        if (isActive && !isSelected)
        {
            if (currentTime > smoothTimeUpdate)
            {
                Debug.Log("StartUpdate");
                smoothTimeUpdate += Time.unscaledDeltaTime;

                if (loadingImage != null)
                {
                    loadingImage.fillAmount = smoothTimeUpdate / currentTime;
                }
            }
            else
            {
                isSelected = true;

                if (anotherButton.isSelected)
                {
                    anotherButton.isSelected = false;
                }

                eventButton.onClick.Invoke();
                EndSelect();
                SoundManager.Instance.PlayNewSound("SelectedQuest");
            }

        }
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        StartSelect();
        Change(selectedPanelImage);
        StatesManager.Instance.ledsController.SetColor(selectedColor);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        EndSelect();

        if (isSelected)
            return;

        if (!anotherButton.isSelected)
        {
            Change(normalPanelImage);
            StatesManager.Instance.ledsController.SetColor(normalColor);
        }
        else
        {
            Change(anotherButton.selectedPanelImage);
            StatesManager.Instance.ledsController.SetColor(anotherButton.selectedColor);
        }
        
    }

    void Change(Sprite newImage)
    {
        panelReference.overrideSprite = newImage;
    }

}

