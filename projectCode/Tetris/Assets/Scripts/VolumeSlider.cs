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

    private MenuController mc;

    // Start is called before the first frame update
    void Start()
    {
        volumeBar = GetComponent<Slider>();
        mc = FindObjectOfType<MenuController>();
    }

    private void OnEnable()
    {
        mc = FindObjectOfType<MenuController>();
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

        mc.UpdateMenuMusicVolume();

        UpdateColor(val);
    }
}
