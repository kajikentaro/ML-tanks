﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

[SerializeField]
public class ShareClick : MonoBehaviour
{
    [SerializeField]
    public void test()
    {
        Debug.Log("hello");
        SocialConnector.SocialConnector.Share("Social Connector", "https://github.com/anchan828/social-connector", null);
    }
    public void viewRanking()
    {

        Application.OpenURL("https://github.com");
    }
    public void goMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}