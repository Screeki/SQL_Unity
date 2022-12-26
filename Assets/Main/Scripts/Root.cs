using System;
using UnityEngine;

public class Root : SceneSingleton<Root>
{
    [SerializeField] private RectTransform windowsContainer;
    [SerializeField] private LoginWindow loginWindow;
    public event Action<BaseWindow> OnCreateNewPrefabWindow;

    public Roles currentRole = Roles.None;
    
    private void Start()
    {
        loginWindow.Create();
        loginWindow.Show();
    }

    public T CreateInUI<T>(T prefab) where T : BaseWindow
    {
        var createdWindow = Instantiate(prefab, windowsContainer);
        OnCreateNewPrefabWindow?.Invoke(prefab);
        return createdWindow;
    }
}