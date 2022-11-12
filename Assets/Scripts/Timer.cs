using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    public static Timer Instance;
    public TextMeshProUGUI resultTime;

    private TimeSpan timePlaying;
    private bool timerGoing;
    private float elapsedTime;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        //DontDestroyOnLoad(gameObject);

    }

    void Start()
    {
        resultTime.text = "Продолжительность последней попытки: 00:00.00";
        timerGoing = false;
    }

    public void BeginTimer()
    {
        timerGoing = true;
        elapsedTime = 0f;

        StartCoroutine(UpdateTimer());
    }

    public void EndTimer()
    {
        timerGoing = false;
    }

    private IEnumerator UpdateTimer()
    {
        while (timerGoing)
        {
            elapsedTime += Time.deltaTime;
            timePlaying = TimeSpan.FromSeconds(elapsedTime);
            string timePlayingStr = "Продолжительность последней попытки: " + timePlaying.ToString("mm':'ss'.'ff");
            resultTime.text = timePlayingStr;

            yield return null;
        }
    }

}
