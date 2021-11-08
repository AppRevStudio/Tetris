using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField]
    private Gradient volumeGradient;

    [SerializeField]
    private Image fill;

    private Slider volumeBar;

    // Start is called before the first frame update
    void Start()
    {
        volumeBar = GetComponent<Slider>();
    }

    private void OnEnable()
    {
        UpdateValue(PlayerPrefs.GetFloat("TetrisVolume"));
    }

    void UpdateColor(float val)
    {
        fill.color = volumeGradient.Evaluate(val);
    }

    public void UpdateValue(float val)
    {
        if (!this.gameObject.activeInHierarchy)
        {
            return;
        }

        if (volumeBar == null)
        {
            volumeBar = GetComponent<Slider>();
        }

        volumeBar.value = val;
        PlayerPrefs.SetFloat("TetrisVolume", val);

        UpdateColor(val);
    }
}
