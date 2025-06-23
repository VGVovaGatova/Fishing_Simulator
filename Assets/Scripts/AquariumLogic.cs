using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;
using UnityEngine.UI;
public class AquariumLogic : MonoBehaviour
{
    [SerializeField] GameObject Player;
    [SerializeField] Text TextHowManyFish;
    [Header("Information")]
    [SerializeField] string NameFish;
    [SerializeField] float HowManyFish;
    [SerializeField] float MaxFish = 5;
    [SerializeField] int MaxHungry = 100;
    [SerializeField] float FishHungry = 100;
    [SerializeField] float NeedWaitToAdd = 600f;

    [SerializeField] string[] FoodName;
    [SerializeField] int[] FoodFull;
    [SerializeField] int MaxCheck;

    [SerializeField] int NumberCheck;
    float MinusHungry = 0.1f;
    
    string NowFish;
    bool PlayerTouchn;
    [SerializeField] Text FoodText;

    [Header("Shop")]
    [SerializeField] GameObject Shop;
    public int[] PriceFishMax;
    public int[] PriceHungryMax;
    public int[] PriceWaitMax;
    public int[] WaitNeed;
    [SerializeField] Text PriceTextFishMax;
    [SerializeField] GameObject FishCreate;
    [SerializeField] GameObject DestroyFish;
    [SerializeField] GameObject UI;
    [SerializeField] GameObject Canvas;
    [SerializeField] Image FoodFill;
    [SerializeField] Image FishFill;
    public string FuntionName;
    public int PriceFishMaxInt;
    public int PriceHungryMaxInt;
    public int PriceWaitMaxInt;

    bool EnabledShop;

    void Start()
    {
        TextHowManyFish.text = HowManyFish.ToString() + ("/") + MaxFish.ToString();
        FishFill.fillAmount = HowManyFish / MaxFish;
        StartCoroutine("HungrySystem");
        StartCoroutine("FishAddAuto");
        PriceTextFishMax.text = ("Cost:") + (" ") + PriceFishMax[PriceFishMaxInt].ToString();
        FoodFill.fillAmount = FishHungry / MaxHungry;
        FoodText.text = FishHungry.ToString("F0") +("/") + MaxHungry.ToString("");
    }
    void AddFishInAquarium()
    {
        GameObject FishNew = Instantiate(FishCreate, FishCreate.transform.position, Quaternion.identity);
        FishNew.name = NameFish;
        FishNew.SetActive(true);
    }
    IEnumerator RotationTransform()
    {
        yield return new WaitForSeconds(0.1f);
        Shop.transform.position = new Vector3(0, 0, 0);
        Shop.transform.rotation = Quaternion.Euler(0,0,0);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && PlayerTouchn)
        {
            Shop.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                Player.GetComponent<PlayerController>().CanRotationCamera = false;
        }
        if (Input.GetKeyDown(KeyCode.E) && PlayerTouchn)
        {
            CheckFood();
        }
        if (Input.GetKeyDown(KeyCode.Q) && PlayerTouchn && NameFish != "NoneFish")
        {
            Player.GetComponent<FishAndInventory>().NowFish = NameFish;
            Player.GetComponent<FishAndInventory>().AddFishInInventory();
            HowManyFish--;
            DestroyFish.SetActive(true);
            FishFill.fillAmount = HowManyFish / MaxFish;
            TextHowManyFish.text = HowManyFish.ToString() + ("/") + MaxFish.ToString();
            if (HowManyFish == 0)
            {
                NameFish = "NoneFish";
            }
        }
    }
    void CheckFood()
    {
        if (Player.GetComponent<FishAndInventory>().Slots[Player.GetComponent<FishAndInventory>().SelectedSlot] == FoodName[NumberCheck])
            {
                Player.GetComponent<FishAndInventory>().SlotsInt[Player.GetComponent<FishAndInventory>().SelectedSlot]--;
                Player.GetComponent<FishAndInventory>().ReloadIcon();
                FishHungry = FishHungry + FoodFull[NumberCheck];
                FoodText.text = FishHungry.ToString("F0") +("/") + MaxHungry.ToString("");
                FoodFill.fillAmount = FishHungry / MaxHungry;
                if (FishHungry > MaxHungry)
                {
                    FishHungry = 100;
                    FoodText.text = FishHungry.ToString("F0") +("/") + MaxHungry.ToString("");
                }
                
                
            }
        else
        {
                NumberCheck++;
                if (NumberCheck == FoodFull.Length || NumberCheck > FoodFull.Length)
                {
                    FishAdd();
                    NumberCheck = 0;
                }
                else
                CheckFood();
        }
            
    }
    void FishAdd()
    {
        NowFish = Player.GetComponent<FishAndInventory>().Slots[Player.GetComponent<FishAndInventory>().SelectedSlot];
        if (NowFish == NameFish || NameFish == "NoneFish" && Player.GetComponent<FishAndInventory>().SlotsInt[Player.GetComponent<FishAndInventory>().SelectedSlot] > 0 && HowManyFish < MaxFish && HowManyFish != MaxFish)
        {
            if (HowManyFish < MaxFish)
            {
            HowManyFish++;
            FishFill.fillAmount = HowManyFish / MaxFish;
            TextHowManyFish.text = HowManyFish.ToString() + ("/") + MaxFish.ToString();
            NameFish = NowFish;
            
            Player.GetComponent<FishAndInventory>().SlotsInt[Player.GetComponent<FishAndInventory>().SelectedSlot]--;
            Player.GetComponent<FishAndInventory>().ReloadIcon();
            AddFishInAquarium();
            }
        }
    }
    IEnumerator FishAddAuto()
    {
        while (true)
        {
            yield return new WaitForSeconds(2);
            if (HowManyFish >= 2 && HowManyFish < MaxFish && HowManyFish != MaxFish)
            {
                yield return new WaitForSeconds(NeedWaitToAdd);
                HowManyFish++;
                FishFill.fillAmount = HowManyFish / MaxFish;
            }
        }
    }
    IEnumerator HungrySystem()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.3f);
            if (HowManyFish >= 1)
            {
                FishHungry = FishHungry - MinusHungry;
                FoodFill.fillAmount = FishHungry / MaxHungry;
                FoodText.text = FishHungry.ToString("F0") +("/") + MaxHungry.ToString("");
                if (FishHungry <= 0)
                {
                    Debug.Log("Fish dead");

                }
            }
        }
    }

    // shop
    public void CheckFunction()
    {
        if (FuntionName == ("BuyMaxFish"))
        {
            BuyMaxFish();
            Debug.Log("BuyMaxFish");
            FuntionName = null;
        }
        if (FuntionName == ("BuyMaxHungry"))
        {
            Debug.Log("BuyMaxHungry");
            FuntionName = null;
        }
    }
    public void BuyMaxFish()
    {
        if (Player.GetComponent<FishAndInventory>().Money >= PriceFishMax[PriceFishMaxInt] && PriceFishMaxInt != PriceFishMax.Length)
        {
            Player.GetComponent<FishAndInventory>().Money -= PriceFishMax[PriceFishMaxInt];
            PriceFishMaxInt++;
            if (PriceFishMaxInt == PriceFishMax.Length)
            PriceFishMaxInt = -1;
            MaxFish++;
        //    PriceTextFishMax.text = ("Cost:") + (" ") + PriceFishMax[PriceFishMaxInt].ToString();
            TextHowManyFish.text = HowManyFish.ToString() + ("/") + MaxFish.ToString();
        }
    }
    public void BuyMaxHungry(int Price, Text PriceText)
    {
        if (PriceHungryMaxInt != PriceHungryMax.Length && Player.GetComponent<FishAndInventory>().Money >= PriceHungryMax[PriceHungryMaxInt])
        {
            Player.GetComponent<FishAndInventory>().Money -= PriceHungryMax[PriceHungryMaxInt];
            PriceHungryMaxInt++;
            MaxHungry += 5;
            FoodText.text = FishHungry.ToString("F0") +("/") + MaxHungry.ToString("");
            if (PriceHungryMaxInt == PriceHungryMax.Length)
            {
                Price = -1;
                PriceHungryMaxInt = -1;
                PriceText.text = ("Max");
                
            }
            else
            {
            Price = PriceHungryMax[PriceHungryMaxInt];
            PriceText.text = PriceHungryMax[PriceHungryMaxInt].ToString();
            }
        }
    }
    public void BuyMaxWait(int Price, Text PriceText)
    {
        if (PriceWaitMaxInt != PriceWaitMax.Length && Player.GetComponent<FishAndInventory>().Money >= PriceWaitMax[PriceWaitMaxInt])
        {
            Player.GetComponent<FishAndInventory>().Money -= PriceWaitMax[PriceWaitMaxInt];
            NeedWaitToAdd -= 5;
            PriceWaitMaxInt++;
            if (PriceWaitMaxInt == PriceWaitMax.Length)
            {
                Price = -1;
                PriceWaitMaxInt = -1;
                PriceText.text = ("Max");
                
            }
            else
            {
            Price = PriceWaitMax[PriceWaitMaxInt];
            PriceText.text = PriceWaitMax[PriceWaitMaxInt].ToString();
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            PlayerTouchn = true;
            UI.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            Shop.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Player.GetComponent<PlayerController>().CanRotationCamera = true;
            PlayerTouchn = false;
            UI.SetActive(false);
        }
    }
}
