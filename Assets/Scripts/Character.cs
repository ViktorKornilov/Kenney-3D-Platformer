using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        var grounded = Physics.Raycast(transform.position, Vector3.down, 1.1f);
        if(grounded && Input.GetKeyDown(KeyCode.Space)) {
            rb.velocity = new Vector3(0, 5, 0);
        }
    }
}