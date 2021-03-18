using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TextBox
{8
    public string ID { get; set; } // ID to idetnify the text
    public GameObject parent1 { get; set; } // refers to class related to this textbox
    public GameObject parent2 { get; set; } // refers to second class/node in an association /or null
    public GameObject parent3 { get; set; } // refers to line which contains this text at an end/ or null

    public TextBox()
    {

    }
}
