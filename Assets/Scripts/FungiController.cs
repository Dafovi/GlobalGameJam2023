using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FungiController : MonoBehaviour
{
    [SerializeField] private States fungiState;
    [SerializeField] private float speed;

    [Header("FungiStatesSprites")]
    [SerializeField] private GameObject fungi1;
    [SerializeField] private GameObject fungi2;
    [SerializeField] private GameObject fungi3;

    private Rigidbody2D rb;
    private float state;
    private GameObject currentFungi;
    private float horizontalMovement;
    private Vector2 limits;
    void Start()
    {
        state = (int)fungiState;
        SetFungiState();
        rb = GetComponent<Rigidbody2D>();
        limits = Camera.main.WorldToViewportPoint(transform.position);
    }
    void Update()
    {
        Movement();
        ChangeState();
    }
    //[ContextMenu("SetFungi")]
    private void SetFungiState()
    { 
        switch (state)
        {
            case 0:
                fungiState = States.state1;
                fungi1.SetActive(true);
                fungi2.SetActive(false);
                fungi3.SetActive(false);
                currentFungi = fungi1;
                break;
            case 1:
                fungiState = States.state2;
                fungi1.SetActive(false);
                fungi2.SetActive(true);
                fungi3.SetActive(false);
                currentFungi = fungi2;
                break;
            case 2:
                fungiState = States.state3;
                fungi1.SetActive(false);
                fungi2.SetActive(false);
                fungi3.SetActive(true);
                currentFungi = fungi3;
                break;
        }
    }
    private void Movement()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        rb.velocity=new Vector2(horizontalMovement * speed,rb.velocity.y);
    }
    private void Animations()
    {
        if (horizontalMovement != 0)
        {
            currentFungi.GetComponent<Animator>().SetBool("Walk",true);
        }
        else
        {
            currentFungi.GetComponent<Animator>().SetBool("Walk", false);
        }
        if (rb.velocity.x < 0)
        {
            currentFungi.GetComponent<SpriteRenderer>().flipX = true;
        }
        if (rb.velocity.x > 0)
        {
            currentFungi.GetComponent<SpriteRenderer>().flipX = false;
        }
    }
    private void ChangeState()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (state > 0)
                state--;
            else
                state = System.Enum.GetNames(typeof(States)).Length-1;
            SetFungiState();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (state < System.Enum.GetNames(typeof(States)).Length-1)
                state++;
            else
                state = 0;
            SetFungiState();
        }
    }
    public enum States
    {
        state1,
        state2,
        state3
    }
}
