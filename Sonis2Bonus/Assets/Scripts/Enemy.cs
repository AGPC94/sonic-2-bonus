using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float speedRotation;
    [SerializeField] ParticleSystem particle;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * speedRotation * Time.deltaTime);
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Player>().Hurt();
            AudioManager.instance.Play("Lost");
            particle.Play();
        }
    }
}
