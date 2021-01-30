using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    public float maxForwardTilt = 15.0f;
    public float maxSidewaysTilt = 15.0f;

    public float rotationSpeed = 75.0f;
    public float moveSpeed = 30.0f;
    public float moveDirectionLerpSpeed = 3.0f;

    public Transform shipModelTransform;


    private float forwardTilt = 0.0f;
    private float sidewaysTilt = 0.0f;

    private Vector3 moveDirection = Vector3.zero;

    private new Rigidbody rigidbody; // use keyword 'new' to overwrite the deprecated reference to 'rigidbody'


    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();    
    }


    private void UpdateModelTilt(float horizontalInput)
    {
        // Tilt the model based on the rigidbody velocity
        Vector3 direction = transform.InverseTransformDirection(rigidbody.velocity);
        forwardTilt = maxForwardTilt * Mathf.Clamp(direction.z / moveSpeed, -1.0f, 1.0f);
        sidewaysTilt = maxSidewaysTilt * Mathf.Clamp(direction.x / moveSpeed, -1.0f, 1.0f);
  
        // Tilt the model if rotating
        if (horizontalInput != 0.0f)
        {
            sidewaysTilt = Mathf.Lerp(sidewaysTilt, horizontalInput * maxSidewaysTilt, 10 * Time.deltaTime);
        }
        else
        {
            sidewaysTilt = Mathf.Lerp(sidewaysTilt, 0, 10 * Time.deltaTime);
        }

        // Apply the tilt
        shipModelTransform.localEulerAngles = new Vector3(forwardTilt, sidewaysTilt, -sidewaysTilt);
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        float strafeInput = Input.GetAxis("Strafe");

        // Give the rigidbody a velocity
        Vector3 targetDirection = new Vector3(0.5f * strafeInput, 0.0f, verticalInput);
        moveDirection = Vector3.Lerp(moveDirection, targetDirection, moveDirectionLerpSpeed * Time.deltaTime);
        rigidbody.velocity = transform.TransformDirection(moveDirection) * moveSpeed;
        
        // Rotate around the vertical axis
        transform.Rotate(Vector3.up, horizontalInput * rotationSpeed * Time.deltaTime);

        // Follow the ground
        if ((Physics.Raycast(transform.position, -Vector3.up, out RaycastHit hit, 10f)))
        {
            if (hit.distance > 0.3f)
            {
                transform.position = new Vector3(transform.position.x, hit.point.y + 5.0f, transform.position.z);
            }
        }

        // Tilt the model based on the user inputs
        UpdateModelTilt(horizontalInput);
    }
}
