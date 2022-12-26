using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginWindow : BaseWindow
{
    [SerializeField] private List<Button> btns;
    [SerializeField] private MainWindow mainWindow;

    private void Awake()
    {
        btns[0].onClick.AddListener(OnUserLogin);
        btns[1].onClick.AddListener(OnModerLogin);
        btns[2].onClick.AddListener(OnAdminLogin);
    }

    private void OnUserLogin()
    {
        BeforeLogin();
        mainWindow.InitUser();
        AfterLogin();
    }

    private void OnModerLogin()
    {
        BeforeLogin();
        mainWindow.InitModer();
        AfterLogin();
    }

    private void OnAdminLogin()
    {
        BeforeLogin();
        mainWindow.InitAdmin();
        AfterLogin();
    }

    private void BeforeLogin()
    {
        mainWindow = (MainWindow)mainWindow.Create();
    }

    private void AfterLogin()
    {
        mainWindow.Show();
        Close();
    }
}