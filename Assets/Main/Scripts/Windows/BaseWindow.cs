using System;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class BaseWindow : MonoBehaviour
{
    [FormerlySerializedAs("prefab")] [SerializeField] protected BaseWindow thisWindowPrefab;

    [HideInInspector] public BaseWindow createdWindow;

    public BaseWindow Create()
    {
        Debug.Log("Create");
        createdWindow = Root.Instance.CreateInUI(thisWindowPrefab);
        createdWindow.gameObject.SetActive(false);
        createdWindow.OnCreate();
        return createdWindow;
    }

    protected virtual void OnCreate()
    {
    }

    public void Show()
    {
        Debug.Log("Show");
        if (createdWindow == null)
        {
            gameObject.SetActive(true);
            return;
        }

        createdWindow.gameObject.SetActive(true);
    }

    public void Close()
    {
        Debug.Log("Close");
        if (createdWindow != null)
        {
            Destroy(createdWindow.gameObject);
            return;
        }

        Destroy(gameObject);
    }
}