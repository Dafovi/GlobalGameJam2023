using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FungiController : MonoBehaviour
{
    [SerializeField] private States fungiState;
    [SerializeField] private float speed;

    [Header("FungiStatesGameObjects")]
    [SerializeField] private GameObject fungi1;
    [SerializeField] private GameObject fungi2;
    [SerializeField] private GameObject fungi3;

    private Rigidbody2D rb;
    private float state;
    private GameObject currentFungi;
    private Animator currentAnim;
    private float horizontalMovement;
    void Start()
    {
        SetFungiState();
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        Movement();
        ChangeState();
        MoveAnim();
    }
    [ContextMenu("SetFungiState")]
    private void SetFungiState()
    {
        state = (int)fungiState;
        FungiState();
    }
    private void FungiState()
    { 
        switch (state)
        {
            case 0:
                fungiState = States.state1;
                StartCoroutine(ChangeState(fungi1));
                break;
            case 1:
                fungiState = States.state2;
                StartCoroutine(ChangeState(fungi2));
                break;
            case 2:
                fungiState = States.state3;
                StartCoroutine(ChangeState(fungi3));
                break;
        }
    }
    private IEnumerator ChangeState(GameObject _newFungi)
    {
        if (!currentFungi)
        {
            if (fungiState == States.state1) currentFungi = fungi1;
            if (fungiState == States.state2) currentFungi = fungi2;
            if (fungiState == States.state3) currentFungi = fungi3;

            currentAnim= currentFungi.GetComponent<Animator>();
        }
        currentAnim.SetTrigger("Transform");
        //yield return new WaitUntil(() => currentAnim.GetCurrentAnimatorStateInfo(0).IsName("Exit"));
        fungi1.SetActive(false);
        fungi2.SetActive(false);
        fungi3.SetActive(false);
        currentFungi = _newFungi;
        currentFungi.SetActive(true);
        currentAnim = currentFungi.GetComponent<Animator>();
        ////
        yield return null;
    }
    private void Movement()
    {
        if (!currentAnim.GetCurrentAnimatorStateInfo(0).IsName("Transform"))
        {
            horizontalMovement = Input.GetAxisRaw("Horizontal");
            rb.velocity = new Vector2(horizontalMovement * speed, rb.velocity.y);
        }
    }
    private void MoveAnim()
    {
        currentAnim.SetBool("Walk", horizontalMovement != 0 ? true : false);
        currentFungi.GetComponent<SpriteRenderer>().flipX = rb.velocity.x < 0 ? true : false;   
    }
    private void ChangeState()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (state > 0)
                state--;
            else
                state = System.Enum.GetNames(typeof(States)).Length-1;
            FungiState();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (state < System.Enum.GetNames(typeof(States)).Length-1)
                state++;
            else
                state = 0;
            FungiState();
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
            Destroy(collision.gameObject);
        }
    }
    private void GoToAction(GameObject _enemy)
    {
        if (_enemy.GetComponent<AcidBehaviour>()&&fungiState==States.state1)
        {
            GameManager.Instance.AddEnemyDefeatCount();
        }
        else if (_enemy.GetComponent<BugBehaviour>() && fungiState == States.state2)
        {
            GameManager.Instance.AddEnemyDefeatCount();
        }
        else if(_enemy.GetComponent<SlimeBehaviour>() && fungiState == States.state3)
        {
            GameManager.Instance.AddEnemyDefeatCount();
        }
        else
        {
            GetDamage();
        }

        _enemy.SetActive(false);
    }
    private void GetDamage()
    {
        currentAnim.SetTrigger("Damage");
        GameManager.Instance.FungiDamage();
        Debug.Log("Damage");
    }
    public enum States
    {
        state1,
        state2,
        state3
    }
}
