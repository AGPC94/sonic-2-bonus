using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveArch : MonoBehaviour, IPooledObject
{
    [SerializeField] bool isPooling;

    public void OnObjectSpawn()
    {
        foreach (Transform child in transform.GetComponentsInChildren<Transform>(true))
            child.gameObject.SetActive(true);
    }

    void Update()
    {
        transform.Translate(Vector3.back * GameManager.gameManager.roadSpeed * Time.deltaTime);

        if (transform.position.z <= GameManager.gameManager.posFinal)
        {
            if (isPooling)
            {
                Vector3 newPos = transform.position;
                newPos.z += GameManager.gameManager.posSpawn;
                transform.position = newPos;
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }
}
