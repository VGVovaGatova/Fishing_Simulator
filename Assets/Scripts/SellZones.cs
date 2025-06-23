using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SellZones : MonoBehaviour
{
    [SerializeField] GameObject Player;
    [SerializeField] GameObject UIObject;
    [SerializeField] Text MoneyTextAdd;
    [SerializeField] Animator AnimMoneyText;
    [SerializeField] GameObject MoneyTextObject;


    [Header("Information")]
    [SerializeField] int[] MoneyGet;
    [SerializeField] Animator Anim;
    [SerializeField] int NumberCheck;
    [SerializeField] int Money;
    [SerializeField] int MoneyNeedAdd;

    bool Debounce;
    float MinusHungry = 0.1f;
    
    string NowFish;
    bool PlayerTouchn;

    void Start()
    {
        
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && PlayerTouchn)
        {
            CheckFood();
        }
        if (Input.GetMouseButtonDown(0) && PlayerTouchn && !Debounce && MoneyNeedAdd > 0)
        {
            StartCoroutine("AnimEnabled");
        }
    }
    IEnumerator AnimEnabled()
    {
        Debounce = true;
        Anim.SetBool("Start", true);
        yield return new WaitForSeconds(1f);
        Anim.SetBool("Start", false);
        yield return new WaitForSeconds(0.5f);
        Debounce = false;
        MoneyTextObject.SetActive(true);
        AnimMoneyText.SetBool("1", true);
        StartCoroutine(MoneyAdd());
        MoneyTextAdd.text = ("+") + (" ") +MoneyNeedAdd.ToString();
        
    }
    void CheckFood()
    {
        if (Player.GetComponent<FishAndInventory>().Slots[Player.GetComponent<FishAndInventory>().SelectedSlot] == Player.GetComponent<FishAndInventory>().FishName[NumberCheck])
            {
                Player.GetComponent<FishAndInventory>().SlotsInt[Player.GetComponent<FishAndInventory>().SelectedSlot]--;
                MoneyNeedAdd += MoneyGet[NumberCheck];
            }
        else
        {
                NumberCheck++;
                if (NumberCheck == MoneyGet.Length || NumberCheck > MoneyGet.Length)
                {
                    NumberCheck = 0;
                }
                else
                CheckFood();
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
            PlayerTouchn = false;
            UIObject.SetActive(false);
        }
    }
    IEnumerator MoneyAdd()
    {
        yield return new WaitForSeconds(0.5f);
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            Player.GetComponent<FishAndInventory>().Money++;
            MoneyNeedAdd--;
            MoneyTextAdd.text = ("+") + (" ") +MoneyNeedAdd.ToString();
            if (MoneyNeedAdd <= 0)
            {
                AnimMoneyText.SetBool("1", false);
                yield return new WaitForSeconds(0.5f);
                MoneyTextObject.SetActive(false);
                break;
            }
        }
    }
}
