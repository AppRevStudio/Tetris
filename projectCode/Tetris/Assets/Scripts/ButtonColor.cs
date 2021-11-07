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
        originalColor = buttonText.color;
    }

    // On mouse hover, change button color
    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonText.color = hoverColor; 
    }

    // On mouse hover exit, change button color back
    public void OnPointerExit(PointerEventData eventData)
    {
        buttonText.color = originalColor; 
    }

    // On mouse click, change button color
    public void OnPointerDown(PointerEventData eventData)
    {
        buttonText.color = hoverColor * thisButton.colors.pressedColor * thisButton.colors.colorMultiplier;
    }

    // On mouse click off, change button color
    public void OnPointerUp(PointerEventData eventData)
    {
        buttonText.color = originalColor * thisButton.colors.highlightedColor * thisButton.colors.colorMultiplier;
    }
}
