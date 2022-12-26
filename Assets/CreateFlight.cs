using System;
using TMPro;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UI;

public class CreateFlight : MonoBehaviour
{
    public event Action<CreateParams> OnCreate;

    [SerializeField] private Button createButton;
    [SerializeField] private TMP_InputField departureInput;
    [SerializeField] private TMP_InputField destinationInput;
    [SerializeField] private TMP_InputField freeSeatsInput;
    [SerializeField] private TMP_InputField stopsInput;

    public struct CreateParams
    {
        public string Departure;
        public string Destination;
        public int FreeSeats;
        public int Stops;
    }

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    public void Init()
    {
        gameObject.SetActive(true);
        createButton.onClick.AddListener(CreateFromInputFieldsData);
    }

    private void CreateFromInputFieldsData()
    {
        CreateParams inputParams = new CreateParams()
        {
            Departure = departureInput.text,
            Destination = destinationInput.text,
            FreeSeats = int.Parse(freeSeatsInput.text),
            Stops = int.Parse(stopsInput.text)
        };
        Create(inputParams);
    }

    private void Create(CreateParams createParams)
    {
        var id_Flight = CalculateIdFrom(createParams.Departure, createParams.Destination);
        var freeSeats = createParams.FreeSeats;
        var stops = createParams.Stops;
        var table = DataBase.GetTable("SELECT * FROM FlightInfo;");
        var flightInfoId = int.Parse(table.Rows[^1][0].ToString()) + 1;
        var command = $"INSERT INTO FlightInfo VALUES ({flightInfoId},{id_Flight},{freeSeats},{stops},{freeSeats});";
        DataBase.ExecuteQueryWithoutAnswer(command);
        OnCreate?.Invoke(createParams);
    }

    private int CalculateIdFrom(string departure, string destination)
    {
        var command = $"SELECT ID FROM Flight WHERE Departure = '{departure}' AND Destination = '{destination}';";
        return int.Parse(DataBase.ExecuteQueryWithAnswer(command));
    }
}