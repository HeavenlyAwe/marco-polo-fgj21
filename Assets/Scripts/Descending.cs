using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Descending : MonoBehaviour
{
    public float moveDirectionLerpSpeed = 3.0f;
    public float moveSpeed = 30.0f;

    private Vector3 moveDirection = Vector3.zero;

    private new Rigidbody rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Give the rigidbody a velocity
        moveDirection = Vector3.Lerp(moveDirection, Vector3.down, moveDirectionLerpSpeed * Time.deltaTime);
        rigidbody.velocity = transform.TransformDirection(moveDirection) * moveSpeed / 2.0f;
    }
}
