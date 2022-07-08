using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class MouseEvent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Color actions")]
    public Color normalColor;
    public Color selectedColor;
    public Image panelImage;

    public void OnPointerEnter(PointerEventData eventData)
    {
        Change(selectedColor);
        ManagerSound.Instance.PlayNewSound("SelectedQuest");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Change(normalColor);
        ManagerSound.Instance.PlayNewSound("SelectedQuest");
    }

    void Change(Color newColor)
    {
        panelImage.color = newColor;
    } 

}

