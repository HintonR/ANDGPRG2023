using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Planet : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI tName;
    [SerializeField] String pName;
    [SerializeField] Camera cam;

    void Start()
    {

    }

    void Update()
    {
        faceCamera();
    }
   
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") tName.text = pName;
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player") tName.text = " ";
    }

    void faceCamera()
    {
        transform.LookAt(transform.position + cam.transform.rotation * Vector3.forward, cam.transform.rotation * Vector3.up);
    }
}
