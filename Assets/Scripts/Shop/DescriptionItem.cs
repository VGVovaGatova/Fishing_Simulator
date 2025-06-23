using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DescriptionItem : MonoBehaviour
{
    [SerializeField] GameObject Player;
    [SerializeField] string Description;
    [SerializeField] int Price;
    [SerializeField] string FuntionEnabled;
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
    [SerializeField] bool FishMax;
    [SerializeField] bool Hungry;
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
        YourClick = false;
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
        PriceText.text = Price.ToString();
        NeedChange.sprite = Icon.sprite;
        if (ThisProductBuy == 1)
        ButtonBuy.SetActive(false);
        if (ThisProductBuy == 0)
        ButtonBuy.SetActive(true);

        if (ItAquarium != null)
        {
            int pricemax = ItAquarium.GetComponent<AquariumLogic>().PriceFishMaxInt;
            PriceText.text = ItAquarium.GetComponent<AquariumLogic>().PriceFishMax[pricemax].ToString();
            Price = ItAquarium.GetComponent<AquariumLogic>().PriceFishMax[pricemax];
        }
    }
    public void Buy()
    {
        
        if (Player.GetComponent<FishAndInventory>().Money >= Price && YourClick)
        {
            if (ItAquarium == null)
            {
            Player.GetComponent<FishAndInventory>().CheckFunction();
            Player.GetComponent<FishAndInventory>().FuntionName = FuntionEnabled;
            Player.GetComponent<FishAndInventory>().Money -= Price;
            ThisProductBuy = 1;
            }


            // FUNTION!

            
            else if (ItAquarium != null && ItAquarium.GetComponent<AquariumLogic>().PriceFishMaxInt != -1 && FishMax && YourClick)
            {
                Debug.Log("BFM");
                ItAquarium.GetComponent<AquariumLogic>().FuntionName = FuntionEnabled;
                ItAquarium.GetComponent<AquariumLogic>().CheckFunction();
                int pricemax = ItAquarium.GetComponent<AquariumLogic>().PriceFishMaxInt;
                if (ItAquarium.GetComponent<AquariumLogic>().PriceFishMaxInt > 0)
                PriceText.text = ItAquarium.GetComponent<AquariumLogic>().PriceFishMax[pricemax].ToString();
                if (ItAquarium != null && ItAquarium.GetComponent<AquariumLogic>().PriceFishMaxInt == -1 && YourClick)
                PriceText.text = ("Max");
                if (ItAquarium.GetComponent<AquariumLogic>().PriceFishMaxInt > 0)
                Price = ItAquarium.GetComponent<AquariumLogic>().PriceFishMax[pricemax];
                StartCoroutine(waitsecondmax());
            }
            
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
