using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField]
    private TMPro.TMP_Text tetrominoFragmentText;

    [SerializeField]
    private ShopItem[] shopItems;

    private void Start()
    {
        tetrominoFragmentText.text = PlayerPrefs.GetInt("TetrisFragments").ToString();

        SetupShop();
    }

    private void OnEnable()
    {
        SetupShop();
    }

    public void SetupShop()
    {
        tetrominoFragmentText.text = PlayerPrefs.GetInt("TetrisFragments").ToString();

        if (!PlayerPrefs.HasKey("TetrisOGMix"))
        {
            PlayerPrefs.SetInt("TetrisOGMix", 1);
        }
        if (!PlayerPrefs.HasKey("TetrisModernMix"))
        {
            PlayerPrefs.SetInt("TetrisModernMix", 0);
        }
        if (!PlayerPrefs.HasKey("TetrisDarkMix"))
        {
            PlayerPrefs.SetInt("TetrisDarkMix", 0);
        }
        if (!PlayerPrefs.HasKey("TetrisTrapMix"))
        {
            PlayerPrefs.SetInt("TetrisTrapMix", 0);
        }
        if (!PlayerPrefs.HasKey("TetrisSlavMix"))
        {
            PlayerPrefs.SetInt("TetrisSlavMix", 0);
        }
        if (!PlayerPrefs.HasKey("TetrisDopeMix"))
        {
            PlayerPrefs.SetInt("TetrisDopeMix", 0);
        }
        if (!PlayerPrefs.HasKey("TetrisDropMix"))
        {
            PlayerPrefs.SetInt("TetrisDropMix", 0);
        }
        if (!PlayerPrefs.HasKey("TetrisEpicMix"))
        {
            PlayerPrefs.SetInt("TetrisEpicMix", 0);
        }

        for (int i = 0; i < shopItems.Length; i++)
        {
            SetShopItemBuyStatus(i);
        }
        EquipShopItem(PlayerPrefs.GetInt("TetrisSong"));
        UpdateShopItems();
    }

    void SetShopItemBuyStatus(int index)
    {
        switch (index)
        {
            case 0:
                if (PlayerPrefs.GetInt("TetrisOGMix") == 1)
                {
                    shopItems[0].isSold = true;
                }
                else
                {
                    shopItems[0].isSold = false;
                }
                break;
            case 1:
                if (PlayerPrefs.GetInt("TetrisModernMix") == 1)
                {
                    shopItems[1].isSold = true;
                }
                else
                {
                    shopItems[1].isSold = false;
                }
                break;
            case 2:
                if (PlayerPrefs.GetInt("TetrisDarkMix") == 1)
                {
                    shopItems[2].isSold = true;
                }
                else
                {
                    shopItems[2].isSold = false;
                }
                break;
            case 3:
                if (PlayerPrefs.GetInt("TetrisTrapMix") == 1)
                {
                    shopItems[3].isSold = true;
                }
                else
                {
                    shopItems[3].isSold = false;
                }
                break;
            case 4:
                if (PlayerPrefs.GetInt("TetrisSlavMix") == 1)
                {
                    shopItems[4].isSold = true;
                }
                else
                {
                    shopItems[4].isSold = false;
                }
                break;
            case 5:
                if (PlayerPrefs.GetInt("TetrisDopeMix") == 1)
                {
                    shopItems[5].isSold = true;
                }
                else
                {
                    shopItems[5].isSold = false;
                }
                break;
            case 6:
                if (PlayerPrefs.GetInt("TetrisDropMix") == 1)
                {
                    shopItems[6].isSold = true;
                }
                else
                {
                    shopItems[6].isSold = false;
                }
                break;
            case 7:
                if (PlayerPrefs.GetInt("TetrisEpicMix") == 1)
                {
                    shopItems[7].isSold = true;
                }
                else
                {
                    shopItems[7].isSold = false;
                }
                break;
            default:
                Debug.LogError("Invalid index case! - Shop-->SetShopItemBuyStatus");
                break;
        }

        shopItems[index].isEquipped = false;
    }

    void EquipShopItem(int index)
    {
        shopItems[index].isEquipped = true;
    }

    void UpdateShopItems()
    {
        for (int i = 0; i < shopItems.Length; i++)
        {
            shopItems[i].UpdateItem();
        }
    }

    public void BuyItem(int index)
    {
        switch(index)
        {
            case 0:
                PlayerPrefs.SetInt("TetrisOGMix", 1);
                break;
            case 1:
                PlayerPrefs.SetInt("TetrisModernMix", 1);
                break;
            case 2:
                PlayerPrefs.SetInt("TetrisDarkMix", 1);
                break;
            case 3:
                PlayerPrefs.SetInt("TetrisTrapMix", 1);
                break;
            case 4:
                PlayerPrefs.SetInt("TetrisSlavMix", 1);
                break;
            case 5:
                PlayerPrefs.SetInt("TetrisDopeMix", 1);
                break;
            case 6:
                PlayerPrefs.SetInt("TetrisDropMix", 1);
                break;
            case 7:
                PlayerPrefs.SetInt("TetrisEpicMix", 1);
                break;
            default:
                Debug.LogError("Invalid index case! - Shop-->BuyItem");
                break;
        }
        SetupShop();
    }
}
