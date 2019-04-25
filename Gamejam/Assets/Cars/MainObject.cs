using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainObject : MonoBehaviour
{
    public float thrusterSpeed;


    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * thrusterSpeed * Time.deltaTime);
    }
}
