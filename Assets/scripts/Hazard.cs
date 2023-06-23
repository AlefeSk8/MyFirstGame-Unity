using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using Cinemachine;

public class Hazard : MonoBehaviour
{
    Vector3 rotation;
    public ParticleSystem breakingEffect;

    private CinemachineImpulseSource cinemachineImpulseSource;
    private Player player;

    private void Start()
    {
        player = FindObjectOfType<Player>();
        cinemachineImpulseSource = GetComponent<CinemachineImpulseSource>();

        var xRotation = Random.Range(90f, 180f);
        rotation = new Vector3(-xRotation, 0);
    }

    private void Update()
    {
        transform.Rotate(rotation * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Hazard"))
        {
            Instantiate(breakingEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);

            if (player != null)
            {
                var distance = Vector3.Distance(transform.position, player.transform.position);
                var force = 1f / distance;

                cinemachineImpulseSource.GenerateImpulse(force);
            }

        }
    }

}
    