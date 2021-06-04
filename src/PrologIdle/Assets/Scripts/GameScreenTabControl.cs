using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameScreenTabControl : MonoBehaviour
{
    [SerializeField] private List<Tab> _tabs;

    private void Start()
    {
        foreach (var tab in _tabs)
        {
            tab._button.onClick.AddListener(() => Open(tab));
        }
        
        Open(_tabs.First());
    }

    private void Open(Tab tab)
    {
        foreach (var t in _tabs)
        {
            t._content.SetActive(t == tab);
        }
    }

    [Serializable]
    public class Tab
    {
        public Button _button;
        public GameObject _content;
    }
}
