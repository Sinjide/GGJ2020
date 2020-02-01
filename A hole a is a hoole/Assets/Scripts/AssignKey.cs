﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssignKey : MonoBehaviour
{
    private static AssignKey _instance;

    public static AssignKey Instance { get { return _instance; } }

    private List<string> _alreadyAssignedKey = new List<string>();
    
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    void Start()
    {
        _alreadyAssignedKey.Add("");
    }

    public string GetRandomKey()
    {
        string key = "";

        while (_alreadyAssignedKey.Contains(key))
        {
            int unicode = Random.Range(65, 90);
            char character = (char)unicode;
            key = character.ToString();
        }
        _alreadyAssignedKey.Add(key);
        return (key);
    }

    public void RemoveKey(string key)
    {
        _alreadyAssignedKey.Remove(key);
    }
}
