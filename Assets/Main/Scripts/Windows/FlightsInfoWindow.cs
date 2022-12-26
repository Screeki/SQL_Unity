using System;
using System.Data;
using Main.Scripts.WindowElements;
using UnityEngine;

namespace Main.Scripts.Windows
{
    public class FlightsInfoWindow : BaseWindow
    {
        [SerializeField] private FlightInfo flightInfoPrefab;
        [SerializeField] private CreateFlight createFlight;
        [SerializeField] private Transform container;

        private void Awake()
        {
            InitFromDB();
        }

        private void InitFromDB()
        {
            var table = DataBase.GetTable("SELECT * FROM FlightInfo;");
            var childCount = container.childCount;
            for (int i = 1; i < childCount; i++)
            {
                DestroyImmediate(container.GetChild(1).gameObject);
            }

            for (int i = 0; i < table.Rows.Count; i++)
            {
                var curRow = table.Rows[i];
                var flightInfo = Instantiate(flightInfoPrefab, container);
                flightInfo.Init(curRow);
            }
        }

        public void InitRemove()
        {
            for (int i = 1; i < container.childCount; i++)
            {
                var flightInfo = container.GetChild(i).GetComponent<FlightInfo>();
                flightInfo.AddListener(() =>
                {
                    RemoveFlight(flightInfo);
                    Destroy(flightInfo.gameObject);
                });
            }

            void RemoveFlight(FlightInfo flightInfo)
            {
                DataBase.ExecuteQueryWithoutAnswer($"DELETE FROM FlightInfo WHERE id_FlightInfo = {flightInfo.ID};");
            }
        }

        public void InitAdd()
        {
            container.gameObject.SetActive(false);
            createFlight.Init();
        }

        public void InitBook()
        {
            for (int i = 1; i < container.childCount; i++)
            {
                var flightInfo = container.GetChild(i).GetComponent<FlightInfo>();
                flightInfo.AddListener(() =>
                {
                    if (flightInfo.n_FreeSeats == 0)
                    {
                        return;
                    }

                    BookFlight(flightInfo);
                    InitFromDB();
                    InitBook();
                });
            }

            void BookFlight(FlightInfo flightInfo)
            {
                DataBase.ExecuteQueryWithoutAnswer(
                    $"UPDATE FlightInfo SET n_FreeSeats = {flightInfo.n_FreeSeats - 1} WHERE id_FlightInfo = {flightInfo.ID};");
            }
        }

        public void InitUnBook()
        {
            for (int i = 1; i < container.childCount; i++)
            {
                var flightInfo = container.GetChild(i).GetComponent<FlightInfo>();
                flightInfo.AddListener(() =>
                {
                    if (flightInfo.n_FreeSeats >= flightInfo.MaxSeats)
                    {
                        return;
                    }
                    UnBookFlight(flightInfo);
                    InitFromDB();
                    InitUnBook();
                });
            }

            void UnBookFlight(FlightInfo flightInfo)
            {
                DataBase.ExecuteQueryWithoutAnswer(
                    $"UPDATE FlightInfo SET n_FreeSeats = {flightInfo.n_FreeSeats + 1} WHERE id_FlightInfo = {flightInfo.ID};");
            }
        }

        public void DisableButtons()
        {
            for (int i = 1; i < container.childCount; i++)
            {
                var flightInfo = container.GetChild(i).GetComponent<FlightInfo>();
                flightInfo.DisableButton();
            }
        }
    }
}