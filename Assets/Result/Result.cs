using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class Result : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI clear_time;
    void Start()
    {
        int all_sec;
        if (PlayerPrefs.HasKey("StartTime"))
        {
            int unixTimestamp = (int)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            all_sec = -PlayerPrefs.GetInt("StartTime") + unixTimestamp;
        }
        else
        {
            all_sec = 0;
        }
        int sec = all_sec % 60;
        all_sec /= 60;
        int min = all_sec % 60;
        int hour = all_sec / 60;
        clear_time.text = hour + ":" + min + ":" + sec;
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
