﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBox : MonoBehaviour
{

    public string text;
    public string ID
    { get; set; }

    public string GetText()
    {
        return GetComponent<InputField>().text;
    }

    public bool SetText(string text)
    {
        var inputField = GetComponent<InputField>();
        if (inputField == null)
        {
            inputField = gameObject.AddComponent<InputField>();
        }
        inputField.text = text;
        Debug.Log("Text has been set to " + text);
        return true;
    }

}
