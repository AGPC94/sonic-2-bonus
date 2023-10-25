using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour, IPooledObject
{
    public void OnObjectSpawn()
    {
        float xForce = Random.Range(-1, 1);
        float yForce = 10;

        Vector2 force = new Vector2(xForce, yForce);

        GetComponent<Rigidbody2D>().velocity = force;
    }
}
