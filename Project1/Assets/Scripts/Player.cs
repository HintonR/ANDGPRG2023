using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float jumpF;
    [SerializeField] private float spd;
    [SerializeField] private float lookSense;
    [SerializeField] private Camera cam;
    [SerializeField] private AudioSource aSource;
    
    private Vector3 v = Vector3.zero;
    private Vector3 r = Vector3.zero;
    private float camUpDownRot = 0f;
    private float curCamUpDownRot = 0f;
    private Rigidbody rb;
   
    private Vector3 moveHor, moveVert, moveVel, rotVect;
    private float xMove, zMove, yRot, cUpDownRot;

    private bool onGround = true;
    public int score;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        score = GlobalVars.totalScore;
    }
    private void Update()
    {
        movement();
        shoot();
    }

    private void FixedUpdate()
    {
        jump();
    
        if (v != Vector3.zero)
        {
            rb.MovePosition(rb.position + v * Time.fixedDeltaTime);
        }

        rb.MoveRotation(rb.rotation * Quaternion.Euler(r));

        if (cam != null)
        {

            curCamUpDownRot -= camUpDownRot;
            curCamUpDownRot = Mathf.Clamp(curCamUpDownRot, -90, 90);

            cam.transform.localEulerAngles = new Vector3(curCamUpDownRot, 0, 0);
        }
    }


    void OnTriggerExit(Collider env)
    {
        if (env.gameObject.CompareTag("Environment")) onGround = false;
    }
    void OnTriggerStay(Collider env)
    {
        if (env.gameObject.CompareTag("Environment")) onGround = true;
    }


    void movement()
    {
        xMove = Input.GetAxis("Horizontal");
        zMove = Input.GetAxis("Vertical");

        moveHor = transform.right * xMove;
        moveVert = transform.forward * zMove;
        moveVel = (moveHor + moveVert).normalized * spd;
        v = moveVel;

        yRot = Input.GetAxis("Mouse X");
        rotVect = new Vector3(0,yRot,0) * lookSense;
        r = rotVect;

        cUpDownRot = Input.GetAxis("Mouse Y") * lookSense;
        camUpDownRot = cUpDownRot;
    }

    void jump()
    {
        if (Input.GetButton("Jump") && onGround) 
        {
            rb.AddForce(Vector3.up * jumpF, ForceMode.Impulse);
            
        }
    }

    void shoot()
    {
        if (Input.GetButton("Fire1") && !aSource.isPlaying)
        {
            aSource.Play();
            Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.CompareTag("Target"))
                {
                    score += 5;
                    hit.collider.gameObject.GetComponent<Bug>().takeDamage();
                }
            }
        }
    }
    
}
