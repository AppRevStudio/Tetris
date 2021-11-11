using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
public class ButtonColor : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    private TMPro.TMP_Text buttonText;
    [SerializeField]
    private Color hoverColor;
    private Color originalColor;
    private Button thisButton;

    private void Awake()
    {
        buttonText = GetComponentInChildren<TMPro.TMP_Text>();
        thisButton = GetComponent<Button>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // originalColor = buttonText.color;
        originalColor = Color.white;

        //if (thisButton.interactable == false)
        //{
        //    buttonText.color = thisButton.colors.disabledColor;
        //}
    }

    public void ButtonColorDisable()
    {
        if (thisButton.interactable == false)
        {
            buttonText.color = thisButton.colors.disabledColor;
        }
        else
        {
            buttonText.color = Color.white;
        }
    }

    // On mouse hover, change button color
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (thisButton.interactable == false)
        {
            return;
        }

        buttonText.color = hoverColor; 
    }

    // On mouse hover exit, change button color back
    public void OnPointerExit(PointerEventData eventData)
    {
        if (thisButton.interactable == false)
        {
            return;
        }

        buttonText.color = originalColor; 
    }

    // On mouse click, change button color
    public void OnPointerDown(PointerEventData eventData)
    {
        if (thisButton.interactable == false)
        {
            return;
        }

        buttonText.color = hoverColor * thisButton.colors.pressedColor * thisButton.colors.colorMultiplier;
    }

    // On mouse click off, change button color
    public void OnPointerUp(PointerEventData eventData)
    {
        if (thisButton.interactable == false)
        {
            return;
        }

        buttonText.color = originalColor * thisButton.colors.highlightedColor * thisButton.colors.colorMultiplier;
    }

    public void SetHoverColor(Color newHoverColor)
    {
        hoverColor = newHoverColor;
    }
}
