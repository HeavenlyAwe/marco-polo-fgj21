using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    // Start is called before the first frame update

    public float maxForwardTilt = 15.0f;
    public float maxSidewaysTilt = 15.0f;

    public float rotationSpeed = 75.0f;
    public float moveSpeed = 10.0f;

    public Transform shipModelTransform;


    private float forwardTilt = 0.0f;
    private float sidewaysTilt = 0.0f;


    void Start()
    {

    }

    private void UpdateModelTilt(float verticalInput, float horizontalInput, float strifeInput)
    {
        if (verticalInput != 0.0f)
        {
            forwardTilt = Mathf.Lerp(forwardTilt, verticalInput * maxForwardTilt, 10 * Time.deltaTime);
        }
        else
        {
            forwardTilt = Mathf.Lerp(forwardTilt, 0, Time.deltaTime);
        }

        if (horizontalInput != 0.0f)
        {
            sidewaysTilt = Mathf.Lerp(sidewaysTilt, horizontalInput * maxSidewaysTilt, 10 * Time.deltaTime);
        }
        else
        {
            sidewaysTilt = Mathf.Lerp(sidewaysTilt, 0, 10 * Time.deltaTime);
        }

        // forwardTilt = Mathf.Clamp(forwardTilt, -maxForwardTilt, maxForwardTilt);
        // sidewaysTilt = Mathf.Clamp(sidewaysTilt, -maxSidewaysTilt, maxSidewaysTilt);

        shipModelTransform.localEulerAngles = new Vector3(forwardTilt, sidewaysTilt, sidewaysTilt);
    }

    // Update is called once per frame
    void Update()
    {
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");
        float strifeInput = 0.0f;

        Debug.Log("Vertical:  " + verticalInput + " Horizontal:" + horizontalInput);

        // shipModelTransform.localEulerAngles.x = maxForwardTilt;

        transform.Rotate(Vector3.up, horizontalInput * rotationSpeed * Time.deltaTime);
        transform.Translate(Vector3.forward * verticalInput * moveSpeed * Time.deltaTime);

        UpdateModelTilt(verticalInput, horizontalInput, strifeInput);
    }
}
