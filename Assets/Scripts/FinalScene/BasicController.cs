using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicController : MonoBehaviour
{
    private float horizontalMovement;
    [SerializeField] private float speed;
    private Animator anim;
    private Rigidbody2D rb;
    private bool flip;
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
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(horizontalMovement * speed, rb.velocity.y);
    }
    private void MoveAnim()
    {
        anim.SetBool("Walk", horizontalMovement != 0 ? true : false);
        flip = horizontalMovement == 1 ? false : horizontalMovement == -1 ? true : flip;
        anim.GetComponent<SpriteRenderer>().flipX = flip;
    }
}
