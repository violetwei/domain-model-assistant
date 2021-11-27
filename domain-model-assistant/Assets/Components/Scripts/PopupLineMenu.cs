using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupLineMenu : MonoBehaviour
{   
    GameObject edgE;

    // Start is called before the first frame update
    void Start()
    {
        // Debug.Log("finding canvas");
        // this.gameObject.transform.SetParent(GameObject.Find("Canvas").transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetLine(Edge edgE)
    {
        Debug.Log("set line in popupline menu");
        this.edgE = edgE.gameObject;
        this.transform.GetChild(2).GetComponent<UnityEngine.UI.Button>().onClick.AddListener(edgE.SetComposition);
        // TODO add other relationships
    }
    public GameObject GetLine()
    {
        return this.edgE;
    }

    public void Close()
    {
        this.gameObject.SetActive(false);
    }

    public void Open()
    {
        this.gameObject.SetActive(true);
    }

    public void Destroy()
    {
        Destroy(this.gameObject);
    }

}