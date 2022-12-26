using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReturnButton : MonoBehaviour
{
    public List<BaseWindow> windows;
    public int currentIndex;
    private Button btn;

    private void Awake()
    {
        btn = GetComponentInChildren<Button>();
        btn.onClick.AddListener(Return);
        Root.Instance.OnCreateNewPrefabWindow += AppendWindows;
    }

    private void AppendWindows(BaseWindow prefab)
    {
        windows.TrimExcess();
        if (!windows.Contains(prefab))
        {
            windows.Add(prefab);
        }

        currentIndex = windows.IndexOf(prefab);
    }

    private void Return()
    {
        if (windows.Count <= 1 || currentIndex == 0)
        {
            return;
        }

        windows[currentIndex].Close();
        windows[currentIndex - 1].Create();
        windows[currentIndex].Show();
    }
}