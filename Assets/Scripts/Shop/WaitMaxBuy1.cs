using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WaitMaxBuy : MonoBehaviour
{
    [SerializeField] GameObject Player;
    [SerializeField] string Description;
    [SerializeField] int Price;
    [SerializeField] UnityEngine.UI.Image Icon;
    [SerializeField] UnityEngine.UI.Image NeedChange;
    [SerializeField] Text Text;
    [SerializeField] Text PriceText;
    [SerializeField] GameObject ButtonBuy;
    [SerializeField] GameObject Close;
    [SerializeField] GameObject DescriptionObject;
    [SerializeField] GameObject Shop;
    [Header("Aquarium")]
    [SerializeField] GameObject ItAquarium;
    [SerializeField] bool YourClick;
    int ThisProductBuy;
    void Start()
    {
        ButtonBuy.GetComponent<Button>().onClick.AddListener(Buy);
        Close.GetComponent<Button>().onClick.AddListener(Closed);
    }
    void Closed()
    {
        Shop.SetActive(false);
        DescriptionObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Player.GetComponent<PlayerController>().CanRotationCamera = true;
    }
    private void FixedUpdate()
    {
        if (YourClick && Text.text != Description)
        {
            YourClick = false;
        }
    }
    public void View()
    {
        YourClick = true;
        DescriptionObject.SetActive(true);
        Text.text = Description;
        NeedChange.sprite = Icon.sprite;
        if (ItAquarium.GetComponent<AquariumLogic>().PriceHungryMaxInt == -1)
        {
            PriceText.text = ("Max");
        }
        else
        {
        int pricemax = ItAquarium.GetComponent<AquariumLogic>().PriceHungryMaxInt;
        PriceText.text = ItAquarium.GetComponent<AquariumLogic>().PriceHungryMax[pricemax].ToString();
        Price = ItAquarium.GetComponent<AquariumLogic>().PriceHungryMax[pricemax];
        if (ThisProductBuy == 1)
        ButtonBuy.SetActive(false);
        if (ThisProductBuy == 0)
        ButtonBuy.SetActive(true);
        }
    }
    public void Buy()
    {
        Debug.Log("Click");
        if (YourClick == true && ItAquarium.GetComponent<AquariumLogic>().PriceHungryMaxInt != -1)
        {
            Debug.Log("Functio");
            ItAquarium.GetComponent<AquariumLogic>().BuyMaxHungry(Price, PriceText);
            
        }
        else
        {
            Debug.Log("Can't");
        }
    }
    IEnumerator waitsecondmax()
    {
        yield return new WaitForSeconds(0.2f);
        if (ItAquarium != null && ItAquarium.GetComponent<AquariumLogic>().PriceFishMaxInt == -1)
            {
                PriceText.text = ("Max");
            }
    }
}
