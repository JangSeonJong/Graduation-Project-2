using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static bool isGameStart = false;
    public static bool isInvenOpen = false;
    public static bool isSpawnerOpen = false;
    public static bool isPause = false;
    public static bool canMove = true;

    public Inventory inven;
    public Map map;
    public PauseMenu pauseMenu;
    public DayTime dayTime;
    public GameObject sleepPanel;

    public int activePoint;
    public int maxActivePoint;
    public int dailyActivePoint;

    public float timeCount;
    public float timePerDay;
    public int dayCount = 1;
    public int endDayCount;

    [SerializeField]
    GameObject endPanel;

    [SerializeField]
    Text actionText;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
            Destroy(gameObject);
    }

    void Start()
    {
        ActivePoint(dailyActivePoint);
    }

    void Update()
    {
        if (!isGameStart)
            return;

        timeCount += Time.deltaTime;

        if (timeCount >= timePerDay)
        {
            dayCount++;
            timeCount = 0;
            dayTime.UpdateDay();
        }

        if (isInvenOpen || isSpawnerOpen || isPause)
            canMove = false;
        else
            canMove = true;
    }

    public void ActivePoint(int count)
    {
        activePoint += count;

        activePoint = Mathf.Clamp(activePoint,0, maxActivePoint);
    }

    public void SleepPanel()
    {
        sleepPanel.GetComponent<Animator>().Play("Sleep");
    }

    public void Sleep()
    {
        if (timeCount < 10)
        {
            StartCoroutine(ActionText());
            return;
        }

        dayCount++;
        map.OnButton();

        PlayerStatus stat = GameScene.instance.player.GetComponent<PlayerStatus>();
        stat.SetHP(10);

        float remainTime = timePerDay - timeCount;
        float hungryTime = remainTime / stat.MaxHungryTime;
        stat.SetHunger(-(int)hungryTime);
        float thirstTime = remainTime / stat.MaxThirstyTime;
        stat.SetThirst(-(int)thirstTime);

        ActivePoint(dailyActivePoint);
        SleepPanel();
        timeCount = 0;
        dayTime.UpdateDay();

        if (dayCount >= endDayCount)
            GameOver();
    }

    IEnumerator ActionText()
    {
        actionText.text = "Can not sleep yet";
        yield return new WaitForSeconds(1);
        actionText.text = "";
    }

    public void GameOver()
    {
        endPanel.SetActive(true);
    }

    public void GameExit()
    {
        Application.Quit();
    }
}
