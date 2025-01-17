﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Section : MonoBehaviour
{

    public GameObject compRect;
    public const int UpdatePositionConst = -10;
    public GameObject textbox;
    public List<GameObject> textboxes = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // ************ UI model Methods for Section ****************//

    public bool SetCompartmentedRectangle(GameObject aCompRect)
    {
        if (aCompRect == null)
        {
            return false;
        }
        compRect = aCompRect;
        return true;
    }

    public GameObject GetCompartmentedRectangle()
    {
        return compRect;
    }

    // Get, Set for TextBox
    public bool AddTextBox(GameObject textbox)
    {
        if (textboxes.Contains(textbox))
        {
            return false;
        }
        textboxes.Add(textbox);
        textbox.GetComponent<TextBox>().SetSection(this.gameObject);
        return true;
    }

    public List<GameObject> GetTextBoxList()
    {
        return textboxes;
    }

    public GameObject GetTextBox(int index)
    {
        if (index >= 0 && index < textboxes.Capacity - 1)
        {
            return this.textboxes[index];
        }
        else
        {
            return null;
        }
    }

    // Used when creating attributes from popup menu
    public void AddAttribute()
    {
        // cap (hardcode) the number of attributes that can be added to a class to be 4
        if (textboxes.Count >= 4)
        {
            return;
        }
        var TB = GameObject.Instantiate(textbox, this.transform);
        TB.GetComponent<TextBox>().ID = "-1";
        TB.GetComponent<InputField>().text = "Enter Text ...";
        TB.transform.position = this.transform.position + new Vector3(0, UpdatePositionConst, 0) * textboxes.Count;
        // Update size of class depending on number of textboxes(attributes)
        // enlarge the section by 0.1*number of textboxes
        TB.transform.localScale += new Vector3(0, 0.1F * textboxes.Count, 0);
        // the code commented below can automatically enlarge the section as we create more attributes, 
        // but it would cause the new textboxes created become blured/disappeared as more than 4 attribute are created
        //this.GetCompartmentedRectangle().transform.localScale += new Vector3((float)0.2,(float)0.5, 0);
        //this.GetComponent<Section>().GetCompartmentedRectangle().transform.localScale +=  new Vector3(0,(float)0.5,0);
        this.AddTextBox(TB);

        // close the popup menu
        if (this.compRect != null)
        {
            this.compRect.GetComponent<CompartmentedRectangle>().GetPopUpMenu().GetComponent<PopupMenu>().Close();
        }
    }

    // Used when creating attribute after reading JSON from the WebCORE server
    public void AddAttribute(string _id, string name, string type)
    {
        var TB = GameObject.Instantiate(textbox, this.transform);
        TB.GetComponent<TextBox>().ID = _id;
        TB.GetComponent<InputField>().text = type + " " + name;
        TB.transform.position = this.transform.position + new Vector3(0, UpdatePositionConst, 0) * textboxes.Count;
        this.AddTextBox(TB);
    }

}
