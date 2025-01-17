using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node: MonoBehaviour
{
    // private string id;
    public GameObject canvas;

    // renamed textbox as header to avoid parent and child
    // from having the same serializable field
    public GameObject header;

    public List<GameObject> connections;
    
    public string ID
    { get; set; }

    public GameObject GetDiagram()
    {
        return canvas;
    }

    public bool SetDiagram(GameObject aCanvas)
    {
        if (aCanvas == null)
        {
            return false;
        }
        canvas = aCanvas;
        return true;
    }

    public GameObject GetHeader()
    {
        return header;
    }

    public bool AddHeader(GameObject aHeader)
    {
        if(aHeader == null)
        {
            return false;
        }
        header = aHeader;
        return true;
    }

    public bool AddEdge(GameObject aEdge){
        if(connections.Contains(aEdge)){
            return false;
        }
        connections.Add(aEdge);
        aEdge.GetComponent<Edge>().AddNode(this.gameObject);
        Debug.Log("Edge added");
        return true;
    }

}
