using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class MouseEvent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Panel Settings")]
    public Color normalColor;
    public Color selectedColor;
    public Image panelReference;
    public Sprite normalPanelImage;
    public Sprite selectedPanelImage;

    public void OnPointerEnter(PointerEventData eventData)
    {
        Change(selectedPanelImage);
        SoundManager.Instance.PlayNewSound("SelectedQuest");
        StatesManager.Instance.ledsController.SetColor(selectedColor);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Change(normalPanelImage);
        SoundManager.Instance.PlayNewSound("SelectedQuest");
        StatesManager.Instance.ledsController.SetColor(normalColor);
    }

    void Change(Sprite newImage)
    {
        panelReference.overrideSprite = newImage;
    } 

}

