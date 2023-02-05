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
        else horizontalMovement = 0;

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
        GameManager.Instance.FungiDamage();
        StartCoroutine(DamageAnim());
    }
    IEnumerator DamageAnim()
    {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        float t = 0;
        float speed = 10f;
        bool growing = false;
        int rounds = 0;
        bool ready = false;
        while (true)
        {
            t = Mathf.Clamp01(t + Time.deltaTime * speed * (growing ? -1 : 1));
            if (t == 1 || t == 0) { growing = !growing; rounds++; }

            sprite.color = new Color(255, 255, 255, t);

            if (rounds >= 4) ready = true;
            if (ready)
            {
                sprite.color = new Color(255, 255, 255, 1);
                yield return false;
            }

            yield return null;
        }
    }
}
