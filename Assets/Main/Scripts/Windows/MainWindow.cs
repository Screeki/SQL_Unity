using System;
using System.Collections.Generic;
using Main.Scripts.Windows;
using UnityEngine;
using UnityEngine.UI;

public class MainWindow : BaseWindow
{
    [SerializeField] private List<GameObject> containers;

    [SerializeField] private BaseWindow seeAllFlightsWindow;

    [SerializeField] private Button seeAllFlights;
    [SerializeField] private Button removeFlight;
    [SerializeField] private Button addFlight;
    [SerializeField] private Button add2Flight;
    [SerializeField] private Button bookFlight;
    [SerializeField] private Button unBookFlight;

    private void Awake()
    {
        seeAllFlights.onClick.AddListener(() =>
        {
            OpenAllFlights();
            ((FlightsInfoWindow)seeAllFlightsWindow.createdWindow).DisableButtons();
            Close();
        });
        removeFlight.onClick.AddListener(() =>
        {
            OpenAllFlights();
            ((FlightsInfoWindow)seeAllFlightsWindow.createdWindow).InitRemove();
            Close();
        });
        addFlight.onClick.AddListener(() =>
        {
            OpenAllFlights();
            ((FlightsInfoWindow)seeAllFlightsWindow.createdWindow).InitAdd();
            Close();
        });
        add2Flight.onClick.AddListener(() =>
        {
            OpenAllFlights();
            ((FlightsInfoWindow)seeAllFlightsWindow.createdWindow).InitAdd();
            Close();
        });
        bookFlight.onClick.AddListener(() =>
        {
            OpenAllFlights();
            ((FlightsInfoWindow)seeAllFlightsWindow.createdWindow).InitBook();
            Close();
        });
        unBookFlight.onClick.AddListener(() =>
        {
            OpenAllFlights();
            ((FlightsInfoWindow)seeAllFlightsWindow.createdWindow).InitUnBook();
            Close();
        });
    }

    protected override void OnCreate()
    {
        base.OnCreate();
        CalculateRole();
    }

    private void CalculateRole()
    {
        foreach (var container in containers)
        {
            container.SetActive(false);
        }

        switch (Root.Instance.currentRole)
        {
            case Roles.None:
                break;
            case Roles.User:
                containers[0].SetActive(true);
                break;
            case Roles.Moderator:
                containers[1].SetActive(true);
                break;
            case Roles.Admin:
                containers[2].SetActive(true);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void OpenAllFlights()
    {
        seeAllFlightsWindow.Create();
        seeAllFlightsWindow.Show();
    }

    public void InitUser()
    {
        Root.Instance.currentRole = Roles.User;
        Debug.Log("InitUser");
        CalculateRole();
    }

    public void InitModer()
    {
        Root.Instance.currentRole = Roles.Moderator;
        Debug.Log("InitModer");
        CalculateRole();
    }

    public void InitAdmin()
    {
        Root.Instance.currentRole = Roles.Admin;
        Debug.Log("InitAdmin");
        CalculateRole();
    }
}