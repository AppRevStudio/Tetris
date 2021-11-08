using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class ColorToggle : MonoBehaviour, IPointerDownHandler // Script credit: https://www.youtube.com/watch?v=XZ4A1H82F7Q
{
    [SerializeField]
    bool playSoundEffect;

    [SerializeField]
    private bool _isOn = false;
    public bool isOn
    {
        get
        {
            return _isOn;
        }
        set
        {
            _isOn = value;
        }
    }

    [SerializeField]
    private RectTransform toggleIndicator;
    [SerializeField]
    private Image backgroundImage;
    [SerializeField]
    private Image toggleImage;

    [SerializeField]
    private Color onColor;
    [SerializeField]
    private Color offColor;

    [SerializeField]
    private Color toggleOnColor;
    [SerializeField]
    private Color toggleOffColor;

    private float onX;
    private float offX;

    [SerializeField]
    private float tweenTime = 0.25f;

    private AudioSource audSource;

    public delegate void ValueChanged(bool value);
    public event ValueChanged valueChanged;

    // Start is called before the first frame update
    void Start()
    {
        offX = toggleIndicator.anchoredPosition.x;
        onX = backgroundImage.rectTransform.rect.width - toggleIndicator.rect.width;
        audSource = GetComponent<AudioSource>();

        ResetToggle();
    }

    private void OnEnable()
    {
        ResetToggle();
    }

    public void ResetToggle()
    {
        if (!PlayerPrefs.HasKey("TetrisColor"))
        {
            PlayerPrefs.SetInt("TetrisColor", 1);
        }

        if (PlayerPrefs.GetInt("TetrisColor") == 1)
        {
            isOn = true;
        }
        else
        {
            isOn = false;
        }

        InititalSetup(isOn);
    }

    private void InititalSetup(bool value)
    {
        if (value)
        {
            backgroundImage.color = onColor;
            toggleImage.color = toggleOnColor;

            toggleIndicator.DOAnchorPosX(onX, 0);
        }
        else
        {
            backgroundImage.color = offColor;
            toggleImage.color = toggleOffColor;

            toggleIndicator.DOAnchorPosX(offX, 0);
        }
    }

    public void Toggle(bool value, bool playSFX)
    {
        if (value != isOn)
        {
            _isOn = value;

            ToggleColor(isOn);
            MoveIndicator(isOn);

            if (playSFX)
            {
                audSource.Play();
            }

            if (valueChanged != null)
            {
                valueChanged(isOn);
            }

            if (isOn)
            {
                PlayerPrefs.SetInt("TetrisColor", 1);
            }
            else
            {
                PlayerPrefs.SetInt("TetrisColor", 0);
            }
        }
    }

    private void ToggleColor(bool value)
    {
        if (value)
        {
            backgroundImage.DOColor(onColor, tweenTime);
            toggleImage.DOColor(toggleOnColor, tweenTime);
        }
        else
        {
            backgroundImage.DOColor(offColor, tweenTime);
            toggleImage.DOColor(toggleOffColor, tweenTime);
        }
    }

    private void MoveIndicator(bool value)
    {
        if (value)
        {
            toggleIndicator.DOAnchorPosX(onX, tweenTime);
        }
        else
        {
            toggleIndicator.DOAnchorPosX(offX, tweenTime);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Toggle(!isOn, playSoundEffect);
    }
}
