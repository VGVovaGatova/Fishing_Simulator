using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AquariumFood : MonoBehaviour
{
    [SerializeField] GameObject Player;
    [SerializeField] GameObject FishFoodReady;
    [SerializeField] GameObject UIObject;
    [Header("Information")]
    [SerializeField] string NameFish;
    [SerializeField] string FoodName;
    [SerializeField] float HowManyFish;
    [SerializeField] float MaxFish = 5;
    [SerializeField] float NeedWaitToAdd = 0f;
    [SerializeField] bool ReadyOrder;
    [SerializeField] Animator Anim;
    [SerializeField] ParticleSystem Particle;
    [SerializeField] Image Time;
    [SerializeField] Image FishFill;
    [SerializeField] Text TimeText;
    [SerializeField] Text TextHowManyFish;
    [Header("Shop")]
    public int[] PriceFishMax;
    [SerializeField] GameObject Shop;
    public int PriceFishMaxInt;
    float NeedWaitToAddMax;
    string NowFish;
    bool PlayerTouchn;


    void Start()
    {
        FishFill.fillAmount = HowManyFish / MaxFish;
        TextHowManyFish.text = HowManyFish.ToString() + ("/") + MaxFish.ToString();
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
        if (Input.GetMouseButtonDown(0) && ReadyOrder && PlayerTouchn)
        {
            FoodAdd();
        }
        if (Input.GetKeyDown(KeyCode.E) && PlayerTouchn && HowManyFish < MaxFish && HowManyFish != MaxFish)
        {
            ChechFish();
        }
        if (HowManyFish > 0)
        {

        }
    }

    // СДЕЛАЙ ПРОВЕРКУ ДОПУСТИМОЙ РЫБЫ

    void FoodAdd()
    {
        NameFish = RemoveFish(NameFish);
        Player.GetComponent<FishAndInventory>().NowFish = NameFish + (" Food");
        FishFoodReady.SetActive(false);
        Player.GetComponent<FishAndInventory>().AddFishInInventory();
        HowManyFish--;
        FishFill.fillAmount = HowManyFish / MaxFish;
        TextHowManyFish.text = HowManyFish.ToString() + ("/") + MaxFish.ToString();
        if (HowManyFish == 0)
                {
                    NameFish = "NoneFish";
                    ReadyOrder = false;
                    Anim.SetBool("Start", false);
                    Particle.gameObject.SetActive(false);
                }
        if (HowManyFish != 0 && HowManyFish > 0)
        {
            while (true)
            {
                HowManyFish--;
                FishFill.fillAmount = HowManyFish / MaxFish;
                TextHowManyFish.text = HowManyFish.ToString() + ("/") + MaxFish.ToString();
                Player.GetComponent<FishAndInventory>().NowFish = NameFish + (" Food");
                Player.GetComponent<FishAndInventory>().AddFishInInventory();
                if (HowManyFish == 0)
                {
                    NameFish = "NoneFish";
                    ReadyOrder = false;
                    break;
                }
            }
        }
    }
    void ChechFish()
    {
        if (ContainsFish(Player.GetComponent<FishAndInventory>().Slots[Player.GetComponent<FishAndInventory>().SelectedSlot]))
        {
            FishAdd();
            
        }

    }
    bool ContainsFish(string str)
    {
        return str.Contains("F1");
    }
    string RemoveFish(string str)
    {
        return str.Replace("F1", "").Trim();
    }
    void FishAdd()
    {
        NowFish = Player.GetComponent<FishAndInventory>().Slots[Player.GetComponent<FishAndInventory>().SelectedSlot];
        if (NowFish == NameFish || NameFish == "NoneFish" && Player.GetComponent<FishAndInventory>().SlotsInt[Player.GetComponent<FishAndInventory>().SelectedSlot] > 0)
        {
            HowManyFish++;
            Anim.SetBool("Start", true);
            NameFish = NowFish;
            FishFill.fillAmount = HowManyFish / MaxFish;
            TextHowManyFish.text = HowManyFish.ToString() + ("/") + MaxFish.ToString();
            NeedWaitToAdd = NeedWaitToAdd + 60;
            NeedWaitToAddMax = NeedWaitToAdd;
            Time.fillAmount = NeedWaitToAdd / NeedWaitToAddMax;
            StopCoroutine("WaitToFood");
            StartCoroutine("WaitToFood");
            Player.GetComponent<FishAndInventory>().SlotsInt[Player.GetComponent<FishAndInventory>().SelectedSlot]--;
            Player.GetComponent<FishAndInventory>().ReloadIcon();
        }
    }
    
    IEnumerator WaitToFood()
    {
        if (NeedWaitToAdd > 0)
        {
            while (true)
            {
                yield return new WaitForSeconds(1);
                NeedWaitToAdd--;
                Time.fillAmount = NeedWaitToAdd / NeedWaitToAddMax;
                if (NeedWaitToAdd == 0)
                {
                    ReadyOrder = true;
                    FishFoodReady.SetActive(true);
                    Anim.SetBool("Start", false);
                    break;
                }
            }
        }
    }
    public void BuyMaxHungry(int Price, Text PriceText)
    {
        if (PriceFishMaxInt != PriceFishMax.Length && Player.GetComponent<FishAndInventory>().Money >= PriceFishMax[PriceFishMaxInt])
        {
            Player.GetComponent<FishAndInventory>().Money -= PriceFishMax[PriceFishMaxInt];
            PriceFishMaxInt++;
            MaxFish += 1;
            TextHowManyFish.text = HowManyFish.ToString() + ("/") + MaxFish.ToString();
            if (PriceFishMaxInt == PriceFishMax.Length)
            {
                Price = -1;
                PriceFishMaxInt = -1;
                PriceText.text = ("Max");
                
            }
            else
            {
            Price = PriceFishMax[PriceFishMaxInt];
            PriceText.text = PriceFishMax[PriceFishMaxInt].ToString();
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            PlayerTouchn = true;
            UIObject.SetActive(true);
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
            UIObject.SetActive(false);
        }
    }
}
