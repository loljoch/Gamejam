using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMoveScript : MonoBehaviour
{
    public Rigidbody ownRb;
    public Quaternion currentRotation;
    public string horControls, verControls;
    public float thrusterSpeed;
    public float fallbackSpeed;
    private float originalThrusterSpeed;
    public float timeToRotate;
    public float brakeTime;
    public float notBrakeTime;


    // Start is called before the first frame update
    void Start()
    {
        originalThrusterSpeed = thrusterSpeed;
        ownRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //currentRotation = transform.rotation;
        HandleInput();
        transform.Translate(Vector3.forward * thrusterSpeed * Time.deltaTime);

    }

    void HandleInput()
    {
        float x = Input.GetAxis(horControls);
        float y = Input.GetAxis(verControls);

        //if (x != 0)
        //{
        Quaternion rotateTo = Quaternion.Euler(currentRotation.eulerAngles.x, currentRotation.eulerAngles.y + (x*45), currentRotation.eulerAngles.z);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotateTo, timeToRotate);
        //}
        //ownRb.AddForce(Vector3.left * -x * sidewaySpeed);

        if (y < 0)
        {
            thrusterSpeed = Mathf.Lerp(thrusterSpeed, fallbackSpeed, brakeTime);
        } else
        {
            thrusterSpeed = Mathf.Lerp(thrusterSpeed, originalThrusterSpeed, notBrakeTime);
        }

        //ownRb.AddForce(ownRb.velocity.x + x*sidewaySpeed, ownRb.velocity.y, ownRb.velocity.z);
    }
}
