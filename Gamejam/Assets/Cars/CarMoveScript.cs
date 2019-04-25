using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMoveScript : MonoBehaviour
{
    public Rigidbody ownRb;
    public float sidewaySpeed;


    // Start is called before the first frame update
    void Start()
    {
        ownRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
    }

    void HandleInput()
    {
        float x = Input.GetAxis("Horizontal");

        transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y + x*20, transform.rotation.z);

        //ownRb.AddForce(ownRb.velocity.x + x*sidewaySpeed, ownRb.velocity.y, ownRb.velocity.z);
    }
}
