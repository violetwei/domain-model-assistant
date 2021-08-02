﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CompartmentedRectangle : Node
{
    
    public GameObject textbox;
    public GameObject section;
    public List<GameObject /*Section*/> sections = new List<GameObject /*Section*/>();
    
    // popup menu variables
    public GameObject popupMenu;
    float holdTimer = 0;
    bool hold = false;

    void Awake()
    {}
    
    // Start is called before the first frame update
    void Start()
    {
        CreateHeader();
        CreateSection();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.hold)
        {
            OnBeginHold();
        }
    }

    // ************ BEGIN Controller Methods for Compartmented Rectangle ****************//

    public void CreateHeader()
    {
        var header = GameObject.Instantiate(textbox, this.transform); // gameObject.AddComponent<TextBox>();
        //vector position will need to be obtained from transform of gameobject in the future
        header.transform.position = this.transform.position + new Vector3(0, 45, 0);
        AddHeader(header);
    }
    
    public void CreateSection()
    {
        Vector3 oldPosition = this.transform.position;
        for (int i = 0; i < 2; i++)
        {
            //Debug.Log(oldPosition);
            var sect = GameObject.Instantiate(section, this.transform); // gameObject.AddComponent<Section>();
            sect.transform.position = oldPosition;
            // at the moment vector positions are hardcoded but will need to be obtained
            //from the transform of the gameobject
            oldPosition += new Vector3(0, -46, 0);
            //Debug.Log(oldPosition);
            AddSection(sect);
        }
    }

    public void OnBeginHold()
    {
        this.hold = true;
        holdTimer += Time.deltaTime;
    }

    public void OnEndHold()
    {
        if(holdTimer > 1f - 5){
            SpawnPopupMenu();
        }
        holdTimer = 0;
        this.hold = false;
    }

    void SpawnPopupMenu()
    {
        if (this.popupMenu.GetComponent<PopupMenu>().getCompartmentedRectangle() == null)
        {
            this.popupMenu = GameObject.Instantiate(this.popupMenu);
            this.popupMenu.transform.position = this.transform.position + new Vector3(100, 0, 0);
            this.popupMenu.GetComponent<PopupMenu>().setCompartmentedRectangle(this);
        }
        else
        {
            this.popupMenu.GetComponent<PopupMenu>().Open(); 
        } 
    }

    /// <summary>
    /// Destroy class when click on delete class.
    /// </summary>
    public void Destroy()
    {
        this.popupMenu.GetComponent<PopupMenu>().Destroy(); 
        Destroy(this.gameObject);
    }

    // ************ END Controller Methods for Compartmented Rectangle ****************//

    // ************ BEGIN UI model Methods for Compartmented Rectangle ****************//

    public bool AddSection(GameObject /*Section*/ aSection)
    {
        if (sections.Contains(aSection))
        {
            return false;
        }
        sections.Add(aSection);
        aSection.GetComponent<Section>().SetCompartmentedRectangle(this.gameObject);
        //Debug.Log("Section added to list of sections for this compartmented rectangles");
        return true;
    }

    // ************ END UI model Methods for Compartmented Rectangle ****************//

}
