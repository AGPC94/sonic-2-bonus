using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ring : MonoBehaviour
{
    [SerializeField] float speedRotation;
    [SerializeField] Transform pivot;
    [SerializeField] ParticleSystem particle;

    // Update is called once per frame
    void Update()
    {
        pivot.Rotate(Vector3.forward * speedRotation * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.gameManager.AddRing();
            AudioManager.instance.Play("Ring");
            particle.Play();
            gameObject.SetActive(false);
            //transform.Find("Item").gameObject.SetActive(false);
            //transform.Find("Particle").GetComponent<ParticleSystem>().Play();
            //GetComponent<Collider>().enabled = false;
            //enabled = false;
        }
    }
}
