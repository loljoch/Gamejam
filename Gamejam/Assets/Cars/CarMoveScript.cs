using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CarMoveScript : MonoBehaviour
{
    public Rigidbody ownRb;
    public Quaternion currentRotation;
    public string horControls;
    public KeyCode jumpKey, brakeKey;
    public float thrusterSpeed;
    public float fallbackSpeed;
    private float originalThrusterSpeed;
    public float timeToRotate;
    public float brakeTime;
    public float notBrakeTime;
    public bool onGround;
    public float jumpHeight;
    public AudioSource mine;
    public AudioClip[] clips;


    // Start is called before the first frame update
    void Start()
    {
        originalThrusterSpeed = thrusterSpeed;
        ownRb = GetComponent<Rigidbody>();
        mine = GetComponent<AudioSource>();
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

        //if (x != 0)
        //{
        Quaternion rotateTo = Quaternion.Euler(currentRotation.eulerAngles.x, currentRotation.eulerAngles.y + (x*45), currentRotation.eulerAngles.z);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotateTo, timeToRotate);
        //}
        //ownRb.AddForce(Vector3.left * -x * sidewaySpeed);

        if (Input.GetKey(brakeKey))
        {
            thrusterSpeed = Mathf.Lerp(thrusterSpeed, fallbackSpeed, brakeTime);
        } else
        {
            thrusterSpeed = Mathf.Lerp(thrusterSpeed, originalThrusterSpeed, notBrakeTime);
        }

        if (Input.GetKey(jumpKey) && onGround)
        {
            onGround = false;
            ownRb.AddForce(0, jumpHeight, 0);
        }

        //ownRb.AddForce(ownRb.velocity.x + x*sidewaySpeed, ownRb.velocity.y, ownRb.velocity.z);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Ground"))
        {
            StartCoroutine(CoroutineGroundTimer());
        }

        if (collision.transform.CompareTag("Obstacle"))
        {
            Die();
        }

        if (collision.transform.CompareTag("Finish"))
        {
            SceneManager.LoadScene("Joris");
        }
    }

    void Die()
    {
        mine.PlayOneShot(clips[0]);


        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        if(players.Length == 1)
        {
            SceneManager.LoadScene("Joris");
        }
        gameObject.SetActive(false);
    }

    IEnumerator CoroutineGroundTimer()
    {
        yield return new WaitForSeconds(0.5f);
        onGround = true;
    }
}
