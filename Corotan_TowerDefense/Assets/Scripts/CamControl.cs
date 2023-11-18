using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamControl : MonoBehaviour
{
    [SerializeField] float _sp = 5;

    void Start()
    {
        
    }

    void Update()
    {
       if(GameManager.Instance._gStatus) Movement();
        
    }

    void Movement()
    {
        if(Input.GetKey(KeyCode.UpArrow) && GetComponent<Transform>().position.y < 2.2) 
            gameObject.transform.Translate(Vector2.up    * (_sp * 1f) * Time.deltaTime);
        if(Input.GetKey(KeyCode.DownArrow) && GetComponent<Transform>().position.y > -2.0) 
            gameObject.transform.Translate(Vector2.down  * (_sp * 1f) * Time.deltaTime);
        if(Input.GetKey(KeyCode.LeftArrow) && GetComponent<Transform>().position.x > -3.6) 
            gameObject.transform.Translate(Vector2.left  * (_sp * 1f) * Time.deltaTime);
        if(Input.GetKey(KeyCode.RightArrow) && GetComponent<Transform>().position.x < 3.9) 
            gameObject.transform.Translate(Vector2.right * (_sp * 1f) * Time.deltaTime);
    }
}
