using System;
using System.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace Main.Scripts.WindowElements
{
    public class FlightInfo : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI flightNumber;
        [SerializeField] private TextMeshProUGUI flightDeparture;
        [SerializeField] private TextMeshProUGUI flightDest;
        [SerializeField] private TextMeshProUGUI flightSeats;
        [SerializeField] private TextMeshProUGUI flightStops;

        [HideInInspector] public int ID;
        private Button _btn;
        public int n_FreeSeats;
        public int MaxSeats;

        private void Awake()
        {
            _btn = GetComponentInChildren<Button>();
        }

        public void Init(DataRow row)
        {
            ID = int.Parse(row[0].ToString());
            MaxSeats = int.Parse(row[2].ToString());
            var flightID = int.Parse(row[1].ToString());
            flightDeparture.text =
                DataBase.ExecuteQueryWithAnswer($"SELECT Departure FROM Flight WHERE ID = {flightID};");
            flightDest.text = DataBase.ExecuteQueryWithAnswer($"SELECT Destination FROM Flight WHERE ID = {flightID};");
            flightNumber.text =
                DataBase.ExecuteQueryWithAnswer($"SELECT Flight_Number FROM Flight WHERE ID = {flightID};");
            n_FreeSeats = int.Parse(row[4].ToString());
            flightSeats.text = row[4].ToString();
            flightStops.text = row[3].ToString();
        }

        public void AddListener(Action action)
        {
            _btn.onClick.AddListener(action.Invoke);
        }

        public void Destroy()
        {
            Object.Destroy(gameObject);
        }

        public void DisableButton()
        {
            _btn.enabled = false;
        }
    }
}