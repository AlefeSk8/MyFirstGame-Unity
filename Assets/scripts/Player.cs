using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Player : MonoBehaviour
{
    public float forceMultiplier = 6f;
    public float maximumVelocity = 4.5f;
    public ParticleSystem deathParticles;
    public GameObject mainVCamera;
    public GameObject zoomVCamera;

    private Rigidbody rb;
    private CinemachineImpulseSource cinemachineImpulseSource;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cinemachineImpulseSource = GetComponent<CinemachineImpulseSource>();
    }

    // Update is called once per frame
    void Update()
    {
        var horizontalInput = Input.GetAxis("Horizontal");

        if (rb.velocity.magnitude <= maximumVelocity)
        {
            rb.AddForce(new Vector3(horizontalInput * forceMultiplier * Time.deltaTime, 0, 0));
        }


    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Hazard"))
        {
            GameManager.GameOver();
            
            Destroy(gameObject);
            Instantiate(deathParticles, transform.position, Quaternion.identity);

            cinemachineImpulseSource.GenerateImpulse();
            mainVCamera.SetActive(false);
            zoomVCamera.SetActive(true);
        }
    }
}
