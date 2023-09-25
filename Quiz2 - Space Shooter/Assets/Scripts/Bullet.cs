using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float sp;

    // Update is called once per frame
    void Update()
    {
        this.GetComponent<Rigidbody>().velocity = transform.forward * sp;

        Destroy(gameObject, 3);
    }
}
