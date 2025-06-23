using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FishHunter : MonoBehaviour
{
    [Header("Need Full")]
    [SerializeField] Transform PositionLineStart;
    [SerializeField] LineRenderer Line;
    [SerializeField] GameObject Point;
    [SerializeField] GameObject Camera;
    [SerializeField] GameObject Fishing;
    [SerializeField] int Force;
    [SerializeField] LayerMask WaterLayerMaskMask;
    [SerializeField] LayerMask PlayerMask;
    [SerializeField] GameObject ParticleWhenWater;
    [Header("Information")]
    [SerializeField] int LuckNow;
    [SerializeField] int MaxLuck = 5;
    [SerializeField] bool Debounce = true;
    bool NowPoplavok;
    [SerializeField] bool TouchnWater;
    [SerializeField] bool PlayerisClose;
    [SerializeField] GameObject AnimCurcle;
    [SerializeField] float timeclick;
    [SerializeField] bool TimeEvent;
    [SerializeField] GameObject[] TextScore;
    [SerializeField] GameObject AnimTextScore;
    [SerializeField] ParticleSystem ParticleWater;
    [SerializeField] ParticleSystem ParticleJustVisual;
    [SerializeField] Animator Anim;
    [SerializeField] Transform PointNew;
    bool DebounceClick;
    bool DebounceUp;
    void Start()
    {
        
    }
    void Update()
    {
        StarFishHunter();
        TimeEventClick();
        if (NowPoplavok)
        {
        Line.SetPosition(0, PositionLineStart.position);
        Line.SetPosition(1, Point.transform.position);
        }
    }
    IEnumerator ScoreAnim()
    {
        AnimTextScore.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        AnimTextScore.SetActive(false);
        TextScore[0].SetActive(false);
        TextScore[1].SetActive(false);
        TextScore[2].SetActive(false);
        TextScore[3].SetActive(false);
    }
    void TimeEventClick()
    {
        if (Input.GetMouseButtonUp(1) && TimeEvent && !DebounceClick)
        {
            TimeEvent = false;
            StopCoroutine(TimerAdd());
            StopCoroutine("TimerAdd");
            
        }
        if (TimeEvent == false)
        {
            if (timeclick >= 0.2f && timeclick <= 0.2f)
            {
                timeclick = 0;
                Debug.Log("Bad!");
                TextScore[2].SetActive(true);
                StartCoroutine(ScoreAnim());
                TimeEvent = false;
                StopCoroutine("TimerAdd");
               // AnimCurcle.SetActive(false);
            }
            else if (timeclick >= 0.3f && timeclick <= 0.4f)
            {
                timeclick = 0;
                Debug.Log("Good Time!");
                TextScore[1].SetActive(true);
                StartCoroutine(ScoreAnim());
                TimeEvent = false;
                StopCoroutine("TimerAdd");
               // AnimCurcle.SetActive(false);
            }
            else if (timeclick >= 0.4f && timeclick <= 0.9f)
            {
                timeclick = 0;
                Debug.Log("Perfect!");
                TextScore[0].SetActive(true);
                StartCoroutine(ScoreAnim());
                TimeEvent = false;
                StopCoroutine("TimerAdd");
               // AnimCurcle.SetActive(false);
            }
            else if (timeclick >= 0.7f && timeclick <= 1)
            {
                timeclick = 0;
                Debug.Log("Bad!");
                TimeEvent = false;
                StopCoroutine("TimerAdd");
               // AnimCurcle.SetActive(false);
            }
            else if (timeclick >= 1)
            {
                timeclick = 0;
                Debug.Log("MISS!!");
                TextScore[0].SetActive(false);
                TextScore[3].SetActive(true);
                StartCoroutine(ScoreAnim());
                TimeEvent = false;
                StopCoroutine("TimerAdd");
                AnimCurcle.SetActive(false);
            }
        }
    }
    void FixedUpdate()
    {
        CheckWater();
    }
    
    void StarFishHunter()
    {
        if (Input.GetMouseButton(1) && Debounce && !NowPoplavok && !DebounceUp)
        {
            StartCoroutine("DebounceReload");
            DebounceUp = true;
            Fishing.SetActive(true);
            Line.SetPosition(0, PositionLineStart.position);
            Line.SetPosition(1, Point.transform.position);
            NowPoplavok = true;
            StartCoroutine("CatchingFish");
            
            AnimCurcle.SetActive(true);
            TimeEvent = true;
            DebounceClick = true;
            StartCoroutine(DebounceClickTime());
            StartCoroutine(TimerAdd());
        }
        if (Input.GetMouseButtonDown(1) && Debounce && NowPoplavok)
        {
            StartCoroutine("DebounceReload");
            StopCoroutine("CatchingFish");
            Point.SetActive(false);
            Point.transform.position = this.transform.position;
            StartCoroutine(AnimTimer());
            NowPoplavok = false;
            ParticleWhenWater.SetActive(false);
            TouchnWater = false;
        }
        if (Input.GetMouseButtonUp(1))
        DebounceUp = false;
    }
    void CheckWater()
    {
        TouchnWater = Physics.CheckSphere(Point.transform.position, 0.2f, WaterLayerMaskMask);
    }
    IEnumerator DebounceClickTime()
    {
        yield return new WaitForSeconds(0.1f);
        DebounceClick = false;
    }
    IEnumerator AutoDisabled()
    {
        yield return new WaitForSeconds(1);
        AnimCurcle.SetActive(false);
        Point.SetActive(true);
        Point.transform.position = PointNew.transform.position;
        Point.GetComponent<Rigidbody>().AddForce(Camera.transform.forward * Force, ForceMode.Impulse);
    }
    IEnumerator TimerAdd()
    {
        StartCoroutine(AutoDisabled());
        timeclick = 0;
        while (TimeEvent)
        {
            yield return new WaitForSeconds(0.1f);
            timeclick = timeclick + 0.1f;
            if (timeclick > 1)
            {
                AnimCurcle.SetActive(false);
                Debug.Log("MISS!!");
                TextScore[0].SetActive(false);
                TextScore[3].SetActive(true);
                StartCoroutine(ScoreAnim());
                TimeEvent = false;
                StopCoroutine("TimerAdd");
                AnimCurcle.SetActive(false);
                break;
            }
        }
    }
    IEnumerator DebounceReload()
    {
        Debounce = false;
        yield return new WaitForSeconds(1);
        Debounce = true;
    }
    IEnumerator CatchingFish()
    {
        while (true)
        {
        yield return new WaitForSeconds(1f);
        if (TouchnWater)
        {
        ParticleWhenWater.SetActive(true);
        LuckNow = UnityEngine.Random.Range(0, MaxLuck + 1);
        Debug.Log("Wait...");
        if (LuckNow == 1)
        {
            ParticleWhenWater.SetActive(false);
            GetComponent<FishAndInventory>().FishChance();
            Debug.Log("You done this");
            ParticleWater.transform.position = Point.transform.position;
            ParticleWater.Play();
            Point.SetActive(false);
            Point.transform.position = this.transform.position;
            StartCoroutine(AnimTimer());
            NowPoplavok = false;
            break;
            
        }
        }
        PlayerisClose = Physics.CheckSphere(Point.transform.position, 40, PlayerMask);
            if (!PlayerisClose)
            {
                Point.SetActive(false);
                Fishing.SetActive(false);
                NowPoplavok = false;
                TouchnWater = false;
                StopCoroutine("CatchingFish");
            }
        }
        
    }
    IEnumerator AnimTimer()
    {
        Anim.SetBool("1", true);
        yield return new WaitForSeconds(0.60f);
        Anim.SetBool("1", false);
        Fishing.SetActive(false);
    }
}
