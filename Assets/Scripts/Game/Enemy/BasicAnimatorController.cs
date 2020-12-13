using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAnimatorController : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rb;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponentInParent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 direction = rb.velocity.normalized;
        if (rb.velocity.sqrMagnitude > 0)
        {
            animator.SetFloat("Horizontal", direction.x);
            animator.SetFloat("Vertical", direction.y);
            animator.SetFloat("Magnitude", direction.magnitude);
            //transform.position = transform.position + movement * Time.deltaTime * speed;
        }
        else
        {
            animator.SetFloat("Magnitude", 0.05f);
        }
    }
}
