using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FishAndInventory : MonoBehaviour
{
    public int Money;
    public int SelectedSlot;
    public string[] Slots;
    public int[] SlotsInt;
    public string NowFish = "None";
    [SerializeField] int MaxSlot = 3;

    [Header("Fish Chance and Names")]
    public string[] FishName;
    [SerializeField] int[] FishChanceInt;
    [SerializeField] int MaxChanceFish;

    [Header("Icon in Inventory")]
    [SerializeField] string[] NameFish;
    [SerializeField] Sprite[] IconFish;
    [SerializeField] Image[] InventorySlot;
    [SerializeField] Text[] NumberText;
    string LastNameFish;
    [SerializeField]int NameFishCheck;
    [SerializeField]int SlotintCheck;

    [SerializeField] Animator[] Anim;
    [SerializeField] ParticleSystem[] ParticleInventory;
    [SerializeField] Text CoinText;

    [Header("Other")]
    [SerializeField] GameObject[] RareFishAnim;
    public string FuntionName;
    int CheckFish;
    int WhatNeedWait;
    int CheckInventory;

    int RandomFish;

    void Start()
    {
        ReloadIcon();
        Anim[0].SetBool("Select", false);
        Anim[1].SetBool("Select", false);
        Anim[2].SetBool("Select", false);
    }
    public void AddFishInInventory()
    {
        Debug.Log(NowFish);
        if (NowFish == Slots[0])
        {
            SlotsInt[0]++;
            Slots[0] = NowFish;
            LastNameFish = NowFish;
            ReloadIcon();
            NowFish = "None";
            CheckInventory = 0;
        }
        else if (NowFish == Slots[1])
        {
            SlotsInt[1]++;
            Slots[1] = NowFish;
            LastNameFish = NowFish;
            ReloadIcon();
            NowFish = "None";
            CheckInventory = 0;
        }
        else if (NowFish == Slots[2])
        {
            SlotsInt[2]++;
            Slots[2] = NowFish;
            LastNameFish = NowFish;
            ReloadIcon();
            NowFish = "None";
            CheckInventory = 0;
        }
        else
        {
        if (NowFish == Slots[CheckInventory] || Slots[CheckInventory] == "None")
        {
            SlotsInt[CheckInventory]++;
            Slots[CheckInventory] = NowFish;
            LastNameFish = NowFish;
            ReloadIcon();
            NowFish = "None";
            CheckInventory = 0;
        }
        else
        {
            CheckInventory++;
            if (CheckInventory == 3)
            {
                CheckInventory = 0;
                NowFish = "None";
                Debug.Log("You not have free slot in your Inventory");
            }
            else
            {
                AddFishInInventory();
            }
        
        }
        }
    }
    private void FixedUpdate()
    {
        NumberText[0].text = SlotsInt[0].ToString();
        NumberText[1].text = SlotsInt[1].ToString();
        NumberText[2].text = SlotsInt[2].ToString();
        CoinText.text = Money.ToString("");
    }
    public void FishChance()
    {
        RandomFish = UnityEngine.Random.Range(0, FishChanceInt[CheckFish] + 1);
        if (RandomFish == 1)
        {
            NowFish = FishName[CheckFish];
            RareFishCheck();
            RandomFish = 0;
            AddFishInInventory();
            CheckFish = 0;
        }
        else
        {
            CheckFish++;
            if (CheckFish == MaxSlot)
            {
                CheckFish = 0;
                RandomFish = 0;
                FishChance();
            }
            else
            {
                FishChance();
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
        SelectedSlot = 0;
        Anim[0].SetBool("Select", true);
        Anim[1].SetBool("Select", false);
        Anim[2].SetBool("Select", false);
        ParticleInventory[0].Play();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
        SelectedSlot = 1;
        Anim[0].SetBool("Select", false);
        Anim[1].SetBool("Select", true);
        Anim[2].SetBool("Select", false);
        ParticleInventory[1].Play();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
        SelectedSlot = 2;
        Anim[0].SetBool("Select", false);
        Anim[1].SetBool("Select", false);
        Anim[2].SetBool("Select", true);
        ParticleInventory[2].Play();
        }

        if (SlotsInt[0] == 0)
        {
        Slots[0] = "None";
        ReloadIcon();
        }
        if (SlotsInt[1] == 0)
        {
        Slots[1] = "None";
        ReloadIcon();
        }
        if (SlotsInt[2] == 0)
        {
        Slots[2] = "None";
        ReloadIcon();
        }
    }
    public void ReloadIcon()
    {
        if (Slots[SlotintCheck] == NameFish[NameFishCheck])
        {
            InventorySlot[SlotintCheck].sprite = IconFish[NameFishCheck];
            NameFishCheck = 0;
            SlotintCheck++;
            if (SlotintCheck != 3)
            ReloadIcon();
            else
            SlotintCheck = 0;
        }
        else
        {
            NameFishCheck++;
            if (NameFishCheck == NameFish.Length)
            {
                    NameFishCheck = 0;
                    SlotintCheck = 0;
            }
            else
            ReloadIcon();
        }
    }


    public void RareFishCheck()
    {
        Debug.Log(NowFish);
        if (NowFish == ("Blue Fish F1"))
        {
            RareFishAnim[0].SetActive(true);
            WhatNeedWait = 3;
            StartCoroutine(DisabledFishAnimationRare());
        }
    }
    IEnumerator DisabledFishAnimationRare()
    {
        yield return new WaitForSeconds(WhatNeedWait);
        RareFishAnim[0].SetActive(false);
    }
    // Shop!
    public void CheckFunction()
    {
        if (FuntionName == ("BeginnerBait"))
        {
            BeginnerBait();
            FuntionName = null;
        }
    }
    void BeginnerBait()
    {
        
    }
}
