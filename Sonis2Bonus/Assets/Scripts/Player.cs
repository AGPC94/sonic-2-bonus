using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float jumpHeight;
    float gravity;
    [SerializeField] float gravityGrounded;
    [SerializeField] float gravityMultiplier;
    [SerializeField] bool isGrounded;

    [Header("Invincibility")]
    [SerializeField] bool isInvincible;
    [SerializeField] float flickeringTime;
    [SerializeField] float invincibleTime;

    [Header("Raycast")]
    [SerializeField] Transform rayOrigin;
    [SerializeField] float rayDistance;
    [SerializeField] LayerMask layerGround;

    Vector3 velocity;

    CharacterController controller;
    Animator anim;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        RotateToSlope();

    }

    void Movement()
    {
        velocity.x = Input.GetAxis("Horizontal") * speed;

        gravity = Physics.gravity.y * gravityMultiplier;

        isGrounded = controller.isGrounded;

        if (isGrounded)
        {
            if (Input.GetButtonDown("Jump"))
            {
                velocity.y = Mathf.Sqrt(-2 * gravity * jumpHeight);
                AudioManager.instance.Play("Jump");
            }
            else
                velocity.y = gravityGrounded;
        }
        else
            velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }

    void RotateToSlope()
    {
        if (Physics.Raycast(rayOrigin.position, -transform.up, out RaycastHit hit, rayDistance, layerGround))
        {
            Quaternion slopeRotation = Quaternion.FromToRotation(transform.up, hit.normal);
            transform.rotation *= slopeRotation;

            anim.SetBool("isGrounded", true);
        }
        else
        {
            transform.eulerAngles = Vector3.zero;
            anim.SetBool("isGrounded", false);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawRay(rayOrigin.position, -transform.up * rayDistance);
    }

    public void Hurt()
    {
        if (!isInvincible)
            StartCoroutine(HurtCo());
    }

    IEnumerator HurtCo()
    {
        GameManager.gameManager.RemoveLive();
        isInvincible = true;
        StartCoroutine(Flicker());
        yield return new WaitForSeconds(invincibleTime);
        isInvincible = false;
    }

    IEnumerator Flicker()
    {
        while (isInvincible)
        {
            //GetComponent<MeshRenderer>().enabled = false;
            anim.transform.GetChild(0).gameObject.SetActive(false);
            yield return new WaitForSeconds(flickeringTime);

            //GetComponent<MeshRenderer>().enabled = true;
            anim.transform.GetChild(0).gameObject.SetActive(true);
            yield return new WaitForSeconds(flickeringTime);
        }
    }
}



//Vector3 adjustedVelocity = slopeRotation * velocity;
//if (adjustedVelocity.y < 0)
//velocity = adjustedVelocity;
/*
 * 
    void RotateTest()
    {
        gravity = Physics.gravity.y * gravityMultiplier;

        if (controller.isGrounded)
            velocity.y = gravityGrounded;
        else
            velocity.y += gravity * Time.deltaTime;

        Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        if (Physics.Raycast(rayOrigin.position, -transform.up, out RaycastHit hit, rayDistance, layerGround))
        {
            slopeRotation = Quaternion.FromToRotation(transform.up, hit.normal);
        }

        if (moveDirection != Vector3.zero)
        {
            direcRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
        }

        Quaternion finalRotation = slopeRotation * direcRotation;
        
        transform.rotation = Quaternion.RotateTowards(transform.rotation, finalRotation, 720 * Time.deltaTime);

        controller.Move(velocity * Time.deltaTime);
    }
 */