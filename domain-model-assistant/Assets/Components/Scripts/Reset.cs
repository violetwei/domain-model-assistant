using UnityEngine;
using UnityEngine.UI;
using System;

public class Reset : MonoBehaviour
{

  [SerializeField]
  private Button ResetButton; // assigned in the editor

  private Diagram _diagram;

  void Start()
  {
    _diagram = GameObject.Find("Canvas").GetComponent<Diagram>();
  }

  // Update is called once per frame
  void Update()
  {}

  public void ResetAction()
  {
    _diagram.ResetDiagram();
  }

}
