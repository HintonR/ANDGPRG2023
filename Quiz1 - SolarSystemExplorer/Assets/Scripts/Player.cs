using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float sp;
  
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        movement();
    }

    void movement()
    {
        if(Input.GetKey(KeyCode.W)) this.gameObject.transform.Translate(Vector3.up * (sp * 2) * Time.deltaTime);
        if(Input.GetKey(KeyCode.S)) this.gameObject.transform.Translate(Vector3.down * sp * Time.deltaTime);
        if(Input.GetKey(KeyCode.A)) this.gameObject.transform.Rotate(Vector3.forward * (sp * 10) * Time.deltaTime);
        if(Input.GetKey(KeyCode.D)) this.gameObject.transform.Rotate(Vector3.back * (sp * 10) * Time.deltaTime);

        if(Input.GetKey(KeyCode.UpArrow)) this.gameObject.transform.Translate(Vector3.up * (sp * 2) * Time.deltaTime);
        if(Input.GetKey(KeyCode.DownArrow)) this.gameObject.transform.Translate(Vector3.down * sp * Time.deltaTime);
        if(Input.GetKey(KeyCode.LeftArrow)) this.gameObject.transform.Rotate(Vector3.forward * (sp * 10) * Time.deltaTime);
        if(Input.GetKey(KeyCode.RightArrow)) this.gameObject.transform.Rotate(Vector3.back * (sp * 10) * Time.deltaTime); 
    }


}
