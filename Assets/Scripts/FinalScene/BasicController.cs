using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicController : MonoBehaviour
{
    private float horizontalMovement;
    [SerializeField] private float speed;
    private Animator anim;
    private Rigidbody2D rb;
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Movement();
        MoveAnim();

    }
    private void Movement()
    {
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Transform"))
        {
            horizontalMovement = Input.GetAxisRaw("Horizontal");
            rb.velocity = new Vector2(horizontalMovement * speed, rb.velocity.y);
        }
    }
    private void MoveAnim()
    {
        anim.SetBool("Walk", horizontalMovement != 0 ? true : false);
        GetComponent<SpriteRenderer>().flipX = horizontalMovement < 0 ? true : false;
    }
}
