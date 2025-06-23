using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MaxFishFoodAquarium : MonoBehaviour
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
    [SerializeField] bool Wait;
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
        if (ItAquarium.GetComponent<AquariumFood>().PriceFishMaxInt == -1)
        {
            PriceText.text = ("Max");
        }
        else
        {
        int pricemax = ItAquarium.GetComponent<AquariumFood>().PriceFishMaxInt;
        PriceText.text = ItAquarium.GetComponent<AquariumFood>().PriceFishMax[pricemax].ToString();
        Price = ItAquarium.GetComponent<AquariumFood>().PriceFishMax[pricemax];
        if (ThisProductBuy == 1)
        ButtonBuy.SetActive(false);
        if (ThisProductBuy == 0)
        ButtonBuy.SetActive(true);
        }
    }
    public void Buy()
    {
        Debug.Log("Click");
        if (YourClick == true && ItAquarium.GetComponent<AquariumFood>().PriceFishMaxInt != -1 && !Wait)
        {
            ItAquarium.GetComponent<AquariumFood>().BuyMaxHungry(Price, PriceText);
          //  if (Wait)
          //  ItAquarium.GetComponent<AquariumFood>().BuyMaxWait(Price, PriceText);
            
        }
        else if (YourClick == true && ItAquarium.GetComponent<AquariumFood>().PriceFishMaxInt != -1 && Wait)
        {
            ItAquarium.GetComponent<AquariumFood>().BuyMaxHungry(Price, PriceText);
            
        }
        else
        {
            Debug.Log("Can't");
        }
    }
    IEnumerator waitsecondmax()
    {
        yield return new WaitForSeconds(0.2f);
        if (ItAquarium != null && ItAquarium.GetComponent<AquariumFood>().PriceFishMaxInt == -1)
            {
                PriceText.text = ("Max");
            }
    }
}
