using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FungiController : MonoBehaviour
{
    [SerializeField] private float speed;

    private Rigidbody2D rb;
    private bool recolector;
    private Animator anim;
    private float horizontalMovement;
    private bool flip;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        Movement();
        ChangeState();
        MoveAnim();
    }
    private void Movement()
    {
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Transform") && !anim.GetCurrentAnimatorStateInfo(0).IsName("TransformRecolector"))
        {
            horizontalMovement = Input.GetAxisRaw("Horizontal");
        }

        rb.velocity = new Vector2(horizontalMovement * speed, rb.velocity.y);
    }
    private void MoveAnim()
    {
        anim.SetBool("Walk", horizontalMovement != 0 ? true : false);
        flip = horizontalMovement == 1 ? false : horizontalMovement == -1 ? true : flip;
        anim.GetComponent<SpriteRenderer>().flipX = flip;   
    }
    private void ChangeState()
    {
        if (Input.GetMouseButtonDown(0)|| Input.GetMouseButtonDown(1))
        {
            recolector = !recolector;
            anim.SetBool("Recolector", recolector);
            anim.SetTrigger("Transform");
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            GoToAction(collision.gameObject);
        }
        if (collision.CompareTag("Vitamina"))
        {
            GameManager.Instance.AddDificult();
            GameManager.Instance.AddVitaminas();
            Destroy(collision.gameObject);
        }
    }
    private void GoToAction(GameObject _enemy)
    {
        if (_enemy.GetComponent<AcidBehaviour>()&&recolector)
        {
            GameManager.Instance.AddAnimationCount();
        }
        else if(_enemy.GetComponent<SlimeBehaviour>() && !recolector)
        {
            GameManager.Instance.AddAnimationCount();
        }
        else
        {
            GetDamage();
            GameManager.Instance.AddDificult();
        }

        _enemy.SetActive(false);
    }
    private void GetDamage()
    {
        anim.SetTrigger("Damage");
        GameManager.Instance.FungiDamage();
        Debug.Log("Damage");
    }
}
