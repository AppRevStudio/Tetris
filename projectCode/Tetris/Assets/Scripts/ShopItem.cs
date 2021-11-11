using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopItem : MonoBehaviour
{
    [SerializeField]
    private Image fillImage;

    [SerializeField]
    private TMPro.TMP_Text buyText;
    [SerializeField]
    private TMPro.TMP_Text buyButtonText;
    [SerializeField]
    private Button buyButton;

    [SerializeField]
    private TMPro.TMP_Text equipButtonText;
    [SerializeField]
    private Button equipButton;

    [HideInInspector]
    public bool isSold;
    [HideInInspector]
    public bool isEquipped;
    [SerializeField]
    private string songName;
    [SerializeField]
    private TMPro.TMP_Text songNameText;
    [SerializeField]
    private int price;
    [SerializeField]
    private Color songColor;
    [SerializeField]
    int index;

    int fragmentsOwned;

    private Shop shop;

    private void Awake()
    {
        shop = FindObjectOfType<Shop>();

        songNameText.text = songName;
        buyText.text = price.ToString();

        fillImage.color = songColor;

        buyButton.GetComponent<ButtonColor>().SetHoverColor(songColor);
        equipButton.GetComponent<ButtonColor>().SetHoverColor(songColor);

        fragmentsOwned = PlayerPrefs.GetInt("TetrisFragments");
        if (fragmentsOwned < price)
        {
            buyText.color = Color.red;
            buyButton.interactable = false;
        }
    }

    public void UpdateItem()
    {
        fragmentsOwned = PlayerPrefs.GetInt("TetrisFragments");

        if (isSold)
        {
            buyText.gameObject.SetActive(false);
            buyButton.gameObject.SetActive(false);

            equipButton.gameObject.SetActive(true);
            if (isEquipped)
            {
                equipButton.interactable = false;
                equipButtonText.text = "Equipped";
            }
            else
            {
                equipButton.interactable = true;
                equipButtonText.text = "Equip";
            }

            equipButton.GetComponent<ButtonColor>().ButtonColorDisable();
        }
        else
        {
            buyText.gameObject.SetActive(true);
            buyButton.gameObject.SetActive(true);

            equipButton.gameObject.SetActive(false);

            if (fragmentsOwned < price)
            {
                buyText.color = Color.red;
                buyButton.interactable = false;
            }
        }
    }

    public void BuyItem()
    {
        fragmentsOwned = PlayerPrefs.GetInt("TetrisFragments");
        fragmentsOwned -= price;
        PlayerPrefs.SetInt("TetrisFragments", fragmentsOwned);

        isSold = true;
        isEquipped = true;

        PlayerPrefs.SetInt("TetrisSong", index);
        shop.BuyItem(index);
    }

    public void EquipItem()
    {
        isEquipped = true;
        PlayerPrefs.SetInt("TetrisSong", index);
        shop.BuyItem(index);
    }
}
