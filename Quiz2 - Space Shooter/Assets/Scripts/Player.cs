using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float sp;
    [SerializeField] GameObject bPre;
    [SerializeField] Transform s1;
    [SerializeField] Transform s2;
    [SerializeField] Transform s3;

    private int mode = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        movement();
        modes();
        shoot();
    }

    void movement()
    {
        if(Input.GetKey(KeyCode.W)) this.gameObject.transform.Translate(Vector3.forward * (sp * 2) * Time.deltaTime);
        if(Input.GetKey(KeyCode.S)) this.gameObject.transform.Translate(Vector3.back * (sp * 0.5f) * Time.deltaTime);
        if(Input.GetKey(KeyCode.A)) this.gameObject.transform.Translate(Vector3.left * (sp * 1.5f) * Time.deltaTime);
        if(Input.GetKey(KeyCode.D)) this.gameObject.transform.Translate(Vector3.right * (sp * 1.5f) * Time.deltaTime);

        if(Input.GetKey(KeyCode.Q)) this.gameObject.transform.Rotate(Vector3.down * (sp * 20) * Time.deltaTime);
        if(Input.GetKey(KeyCode.E)) this.gameObject.transform.Rotate(Vector3.up * (sp * 20) * Time.deltaTime);
    }


    void shoot()
    {
        if(Input.GetKeyDown(KeyCode.Space) && mode == 1) 
        {
            GameObject b = Instantiate(bPre, s1.position, transform.rotation);
        }
        if(Input.GetKeyDown(KeyCode.Space) && mode == 2) 
        {
            GameObject b1 = Instantiate(bPre, s2.position, transform.rotation * Quaternion.Euler(0,15,0));
            GameObject b2 = Instantiate(bPre, s3.position, transform.rotation * Quaternion.Euler(0,-15,0));
        }
        if(Input.GetKeyDown(KeyCode.Space) && mode == 3) 
        {
            GameObject b1 = Instantiate(bPre, s1.position, transform.rotation);
            GameObject b2 = Instantiate(bPre, s2.position, transform.rotation * Quaternion.Euler(0,45,0));
            GameObject b3 = Instantiate(bPre, s3.position, transform.rotation * Quaternion.Euler(0,-45,0));
        }
        if(Input.GetKeyDown(KeyCode.Space) && mode == 4) 
        {
            GameObject b1 = Instantiate(bPre, s1.position, transform.rotation * Quaternion.Euler(0,30,0));
            GameObject b2 = Instantiate(bPre, s1.position, transform.rotation * Quaternion.Euler(0,-30,0));
            GameObject b3 = Instantiate(bPre, s2.position, transform.rotation * Quaternion.Euler(0,45,0));
            GameObject b4 = Instantiate(bPre, s3.position, transform.rotation * Quaternion.Euler(0,-45,0));
        }
        
    }

    void modes()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1)) mode = 1;
        if(Input.GetKeyDown(KeyCode.Alpha2)) mode = 2;
        if(Input.GetKeyDown(KeyCode.Alpha3)) mode = 3;
        if(Input.GetKeyDown(KeyCode.Alpha4)) mode = 4;
    }

}
