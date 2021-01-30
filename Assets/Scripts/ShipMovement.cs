using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody)), RequireComponent(typeof(AudioSource))]
public class ShipMovement : MonoBehaviour
{
    public float maxForwardTilt = 15.0f;
    public float maxSidewaysTilt = 15.0f;

    public float rotationSpeed = 75.0f;
    public float moveSpeed = 30.0f;
    public float moveDirectionLerpSpeed = 3.0f;

    public float offsetFromGround = 5.0f;

    public Transform shipModelTransform;

    // public AudioSource[] pingAudioSources;


    private float forwardTilt = 0.0f;
    private float sidewaysTilt = 0.0f;

    private Vector3 moveDirection = Vector3.zero;

    private new Rigidbody rigidbody; // use keyword 'new' to overwrite the deprecated reference to 'rigidbody'


    private IEnumerator WaitAndPing(AudioSource audioSource, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        audioSource.Play();
    }

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


    private void PingPickups()
    {
        List<AudioSource> audioSources = new List<AudioSource>();
        GetComponents<AudioSource>(audioSources);

        // Don't ping again until all previous pings are done
        for (int i = 0; i < audioSources.Count; i++)
        {
            if (audioSources[i].isPlaying) return;
        }


        // Trigger the "Marco" call
        float maxClipDuration = -1.0f;
        for (int i = 0; i < audioSources.Count; i++)
        {
            float offsetDuration = Random.Range(0.0f, 0.5f);
            StartCoroutine(WaitAndPing(audioSources[i], offsetDuration));

            float totalDuration = audioSources[i].clip.length + offsetDuration;
            if (totalDuration > maxClipDuration)
            {
                maxClipDuration = totalDuration;
            }
        }

        // Loop over the pickups and trigger the sound
        GameObject[] pickups = GameObject.FindGameObjectsWithTag("Pickup");

        for (int i = 0; i < pickups.Length; i++)
        {
            float distance = Vector3.Distance(transform.position, pickups[i].transform.position);
            float playTimer = distance / 300.0f + maxClipDuration;
            pickups[i].SendMessage("PlaySound", playTimer);
        }
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
        Debug.DrawRay(transform.position, -Vector3.up * 10.0f, Color.green);
        if ((Physics.Raycast(transform.position, -Vector3.up, out RaycastHit hit, 10f)))
        {
            if (!hit.collider.CompareTag("Player") && hit.distance > 0.3f)
            {
                rigidbody.position = new Vector3(rigidbody.position.x, hit.point.y + offsetFromGround, rigidbody.position.z);
            }
        }

        // Tilt the model based on the user inputs
        UpdateModelTilt(horizontalInput);
    }


    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            PingPickups();
        }
    }

}
