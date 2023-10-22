using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float sp;
    [SerializeField] GameObject bPre;
    [SerializeField] Transform s1;
    [SerializeField] Transform s2;
    [SerializeField] Transform s3;
    [SerializeField] AudioClip shtSFX;
    [SerializeField] AudioClip dmgSFX;
    [SerializeField] AudioClip healSFX;
    [SerializeField] AudioClip upgSFX;

     AudioSource aSource;

    private int mode = 1;
    private int mhp = 100;
    private int chp = 100;
    void Start()
    {
        aSource = this.GetComponent<AudioSource>();
    }
    void Update()
    {
        movement();
        modes();
        shoot();
        death();
    }
    void OnTriggerEnter(Collider trigger)
    {
        if (trigger.gameObject.CompareTag("Upgrade")) //(Unused) Check Mode Toggles 
            aSource.PlayOneShot(upgSFX);        
        if (trigger.gameObject.CompareTag("Heal")) heal();
        if (trigger.gameObject.CompareTag("Enemy")) damage();
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
        if(Input.GetKeyDown(KeyCode.Space)) aSource.PlayOneShot(shtSFX);

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
        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Alpha2)
        ||  Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Alpha4))
            aSource.PlayOneShot(upgSFX);
        
        if (Input.GetKeyDown(KeyCode.Alpha1)) mode = 1; 
        if (Input.GetKeyDown(KeyCode.Alpha2)) mode = 2; 
        if (Input.GetKeyDown(KeyCode.Alpha3)) mode = 3; 
        if (Input.GetKeyDown(KeyCode.Alpha4)) mode = 4; 
    }
    private void heal()
    {
        aSource.PlayOneShot(healSFX);
        chp += 25;
        if (chp > mhp) chp = mhp;
    }

    private void damage()
    {
        aSource.PlayOneShot(dmgSFX);
        chp -= 10;
    }
    private void death() 
    {
        if (chp <= 0) Destroy(gameObject, 1);
    }
}
